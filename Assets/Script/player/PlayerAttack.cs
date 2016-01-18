using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour {

    private Dictionary<string, PlayerEffect> effectDict = new Dictionary<string, PlayerEffect>();
    public PlayerEffect[] effectArray;
    public float distanceAttackForward = 2;
    public float distanceAttackAround = 3;
    public int[] damageArray = new int[] { 20, 30, 30, 30 };
    public enum AttackRange {
        Forward,
        Around
    }
    public int hp = 1000;
    private Animator anim;

    private GameObject hudTextGameObject;
    private HUDText hudText;
    private Transform damageShowPoint;
    private Player player;
    private BattleController battleController;
    private bool isSyncPlayerAnimation = false;//表示是否需要同步动画
    void Start()
    {
        player = GetComponent<Player>();
        if (GameController.Instance.battleType==BattleType.Team&&player.roleID==PhotonEngine.Instance.role.ID)//当前角色属于当前客户端
        {
            battleController = GameController.Instance.GetComponent<BattleController>();
            isSyncPlayerAnimation = true;
        }
        hp = PlayerInfo._instance.HP;
        PlayerEffect[] peArray = this.GetComponentsInChildren<PlayerEffect>();
        foreach (PlayerEffect pe in peArray) {
            effectDict.Add(pe.gameObject.name, pe); 
        }
        foreach (PlayerEffect pe in effectArray) {
            effectDict.Add(pe.gameObject.name, pe); 
        }
        anim = this.GetComponent<Animator>();
        damageShowPoint = transform.Find("DamageShowPoint");

        hudTextGameObject = HpBarManager._instance.GetHudText(damageShowPoint.gameObject);
        hudText = hudTextGameObject.GetComponent<HUDText>();
    }

    
    //0 normal skill1 skill2 skill3
    //1 effect name
    //2 sound name
    //3 move forward
    //4 jump height
    void Attack(string args) {
        string[] proArray = args.Split(',');
        //1 show effect
        string effectName = proArray[1];
        ShowPlayerEffect(effectName);
        //2 play sound
        string soundName = proArray[2];
        SoundManager._instance.Play(soundName);
        //3 move forward  控制前冲的效果
        float moveForward = float.Parse(proArray[3]);
        if (moveForward > 0.1f) {
            iTween.MoveBy(this.gameObject, Vector3.forward * moveForward, 0.3f);
        }
        string posType = proArray[0];
        if(posType=="normal"){
            ArrayList array = GetEnemyInAttackRange(AttackRange.Forward);
            foreach (GameObject go in array) {
                go.SendMessage("TakeDamage",damageArray[0]+","+proArray[3]+","+proArray[4] );
            }
        } 

    }
    void PlaySound(string soundName) {
        SoundManager._instance.Play(soundName);
    }
    //0 normal skill1 skill2 skill3
    //1 move forward
    //2 jump height
    void SkillAttack(string args) {
        string[] proArray = args.Split(',');
        string posType = proArray[0];
        if (posType == "skill1") {
            ArrayList array = GetEnemyInAttackRange(AttackRange.Around);
            foreach (GameObject go in array) {
                go.SendMessage("TakeDamage", damageArray[1] + "," + proArray[1] + "," + proArray[2]);
            }
        } else if (posType == "skill2") {
            ArrayList array = GetEnemyInAttackRange(AttackRange.Around);
            foreach (GameObject go in array) {
                go.SendMessage("TakeDamage", damageArray[2] + "," + proArray[1] + "," + proArray[2]);
            }
        } else if (posType == "skill3") {
            ArrayList array = GetEnemyInAttackRange(AttackRange.Forward);
            foreach (GameObject go in array) {
                go.SendMessage("TakeDamage", damageArray[3] + "," + proArray[1] + "," + proArray[2]);
            }
        }
    }

    void ShowPlayerEffect(string effectName) {
        PlayerEffect pe;
        if (effectDict.TryGetValue(effectName, out pe)) {
            pe.Show();
        }
    }

    void ShowEffectDevilHand() {
        string effectName = "DevilHandMobile";
        PlayerEffect pe;
        effectDict.TryGetValue(effectName, out pe);
        ArrayList array = GetEnemyInAttackRange(AttackRange.Forward);
        foreach (GameObject go in array) {
            RaycastHit hit;
            bool collider =  Physics.Raycast(go.transform.position+Vector3.up,Vector3.down,out hit,10f, LayerMask.GetMask("Ground") );
            if (collider) {
                GameObject.Instantiate(pe, hit.point, Quaternion.identity);
            }
        }
    }
    void ShowEffectSelfToTarget( string effectName ) {
        //print("showeffect self to target");
        PlayerEffect pe;
        effectDict.TryGetValue(effectName, out pe);
        ArrayList array = GetEnemyInAttackRange(AttackRange.Around);
        foreach (GameObject go in array) {
                GameObject goEffect = (GameObject.Instantiate(pe) as PlayerEffect).gameObject;
                goEffect.transform.position = transform.position + Vector3.up;
                goEffect.GetComponent<EffectSettings>().Target = go;
        }
    }
    void ShowEffectToTarget( string effectName ) {
        //print("showeffect self to target");
        PlayerEffect pe;
        effectDict.TryGetValue(effectName, out pe);
        ArrayList array = GetEnemyInAttackRange(AttackRange.Around);
        foreach (GameObject go in array) {
            RaycastHit hit;
            bool collider = Physics.Raycast(go.transform.position + Vector3.up, Vector3.down, out hit, 10f, LayerMask.GetMask("Ground"));
            if (collider) {
                GameObject goEffect = (GameObject.Instantiate(pe) as PlayerEffect).gameObject;
                goEffect.transform.position = hit.point;
            }
        }
    }

    //得到在攻击范围之内的敌人
    ArrayList GetEnemyInAttackRange( AttackRange attackRange  ) {
        ArrayList arrayList = new ArrayList();
        if (attackRange == AttackRange.Forward) {
            foreach (GameObject go in TranscriptManager._instance.GetEnemyList()) {
                Vector3 pos = transform.InverseTransformPoint(go.transform.position);
                if (pos.z > -0.5f) {
                    float distance = Vector3.Distance(Vector3.zero, pos);
                    if (distance < distanceAttackForward) {
                        arrayList.Add(go);
                    }
                }
            }
        } else {
            foreach (GameObject go in TranscriptManager._instance.GetEnemyList()) {
                float distance = Vector3.Distance(transform.position, go.transform.position);
                    if (distance < distanceAttackAround) {
                        arrayList.Add(go);
                    }
            }
        }

        return arrayList;
    }

    void TakeDamage(int damage)
    {
        if (this.hp <= 0) return;
        this.hp -= damage;
        if(OnPlayerHpChange!=null)
        OnPlayerHpChange(hp);
        //播放受到攻击的动画
        int random = Random.Range(0, 100);
        if (random < damage)
        {
            anim.SetTrigger("TakeDamage");
            if (isSyncPlayerAnimation)
            {
                PlayerAnimationModel model = new PlayerAnimationModel() {takeDamage = true};
                battleController.SyncPlayerAnimation(model);
            }
        }
        //显示血量的减少
        hudText.Add("-" + damage, Color.red, 0.3f);
        //屏幕上血红特效的显示
        BloodScreen.Instance.Show();
        if (hp <= 0)
        {
            hp = 0;
            anim.SetTrigger("Die");
            if (isSyncPlayerAnimation) {
                PlayerAnimationModel model = new PlayerAnimationModel() { die = true };
                battleController.SyncPlayerAnimation(model);
            }
            GameController.Instance.OnPlayerDie(GetComponent<Player>().roleID);
        }
    }

    public event OnPlayerHpChangeEvent OnPlayerHpChange;

}
