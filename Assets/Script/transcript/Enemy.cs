using UnityEngine;

using System.Collections;
using System.Text;

public class Enemy : MonoBehaviour {

    public GameObject damageEffectPrefab;
    public int hp = 200;
    public float speed = 2;
    public int attackRate = 2;//攻击速率 表示多少秒攻击一次
    public float attackDistance = 2;
    public float damage = 20;//这个是攻击力
    public float downSpeed = 1f;
    public string guid = "";//这个是GUID 是每一个敌人的唯一标示
    public int targetRoleId = -1;//表示这个敌人要攻击的目标
    private GameObject targetGo;//表示要攻击的目标的游戏物体
    private int hpTotal = 0;
    private float downDistance = 0;
    private float distance = 0;
    private float attackTimer = 0;
    private Transform bloodPoint;
    private CharacterController cc;
    private GameObject hpBarGameObject;
    private UISlider hpBarSlider;
    private GameObject hudTextGameObject;
    private HUDText hudText;
    private Vector3 lastPosition = Vector3.zero;
    private Vector3 lastEulerAngles = Vector3.zero;

    private bool lastIsIdle = true;
    private bool lastIsWalk = false;
    private bool lastIsAttack = false;
    private bool lastIsTakeDamage = false;
    private bool lastIsDie = false;



    void Start()
    {
        targetGo = GameController.Instance.GetPlayerByRoleID(targetRoleId);
        TranscriptManager._instance.AddEnemy(this.gameObject);
        hpTotal = hp;
        bloodPoint = transform.Find("BloodPoint");
        cc = this.GetComponent<CharacterController>();
        InvokeRepeating("CalcDistance", 0,0.1f);
        Transform hpBarPoint = transform.Find("HpBarPoint");
        hpBarGameObject =  HpBarManager._instance.GetHpBar(hpBarPoint.gameObject);
        hpBarSlider = hpBarGameObject.transform.Find("Bg").GetComponent<UISlider>();

        hudTextGameObject = HpBarManager._instance.GetHudText(hpBarPoint.gameObject);
        hudText = hudTextGameObject.GetComponent<HUDText>();

        if (GameController.Instance.battleType == BattleType.Team && GameController.Instance.isMaster)
        {
            InvokeRepeating("CheckPositionAndRotation",0,1/30f);
            InvokeRepeating("CheckAnimation",0,1/30f);
        }
    }

    void Update() {
        if (hp <= 0) {
            //移到地下
            downDistance += downSpeed * Time.deltaTime;
            transform.Translate(-transform.up * downSpeed*Time.deltaTime );
            if (downDistance > 4) {
                Destroy(this.gameObject);
            }
            return;
        }
        if (GameController.Instance.battleType==BattleType.Person || (GameController.Instance.battleType==BattleType.Team&&GameController.Instance.isMaster ))
        {
            if (distance < attackDistance) {
                attackTimer += Time.deltaTime;
                if (attackTimer > attackRate)
                {
                    Transform player = targetGo.transform;
                    Vector3 targetPos = player.position;
                    targetPos.y = transform.position.y;
                    transform.LookAt(targetPos);
                    animation.Play("attack01");
                    attackTimer = 0;
                }
                if (!animation.IsPlaying("attack01")) {
                    animation.CrossFade("idle");
                }
            } else {
                animation.Play("walk");
                Move();
            }
        }
      
    }

    void Move()
    {
        Transform player = targetGo.transform;
        Vector3 targetPos = player.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
        cc.SimpleMove(transform.forward * speed);
    }
    void CalcDistance() {
        Transform player = targetGo.transform;
        distance = Vector3.Distance(player.position, transform.position);
    }
	
    //收到攻击调用这个方法
    // 0,收到多少伤害
    // 1,后退的距离
    // 2,浮空的高度
    void TakeDamage(string args) {
        if (hp <= 0) return;
        Combo._instance.ComboPlus();
        string[] proArray = args.Split(',');
        //减去伤害值
        int damage = int.Parse(proArray[0]);
        hp -= damage;
        hpBarSlider.value = (float)hp / hpTotal;
        hudText.Add("-" + damage, Color.red, 0.3f);
//        受到攻击的动画
        animation.Play("takedamage");//浮空和后退
        float backDistance = float.Parse(proArray[1]);
        float jumpHeight = float.Parse(proArray[2]);
        iTween.MoveBy(this.gameObject,            transform.InverseTransformDirection(targetGo.transform.forward) * backDistance             + Vector3.up * jumpHeight,            0.3f);//出血特效实例化
        GameObject.Instantiate(damageEffectPrefab, bloodPoint.transform.position , Quaternion.identity);
        if (hp <= 0) {
            Dead();
        }
    }
    void Attack()
    {
        Transform player = targetGo.transform;
        distance = Vector3.Distance(player.position, transform.position);
        if (distance < attackDistance)
        {
            player.SendMessage("TakeDamage", damage,SendMessageOptions.DontRequireReceiver);
        }
    }
    //当死亡的时候调用这个方法
    void Dead()
    {
        TranscriptManager._instance.RemoveEnemy(this.gameObject);
        this.GetComponent<CharacterController>().enabled = false;
        Destroy(hpBarGameObject);
        Destroy(hudTextGameObject);
        //第一种死亡方式是播放死亡动画
        //第二种死亡方式是使用破碎效果
        int random = Random.Range(0, 10);
        if (random <= 7)
        {
            animation.Play("die");
        }
        else
        {
            this.GetComponentInChildren<MeshExploder>().Explode();
            this.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        }
    }

    void CheckPositionAndRotation()
    {
        Vector3 position = transform.position;
        Vector3 eulerAngles = transform.eulerAngles;
        if (position.x != lastPosition.x || position.y != lastPosition.y || position.z != lastPosition.z ||
            eulerAngles.x != lastEulerAngles.x || eulerAngles.y != lastEulerAngles.y ||
            eulerAngles.z != lastEulerAngles.z)
        {
            TranscriptManager._instance.AddEnemyToSync(this);
            lastPosition = position;
            lastEulerAngles = eulerAngles;
        }
    }

    void CheckAnimation()
    {
        if (lastIsAttack != animation.IsPlaying("attack01") || lastIsDie!=animation.IsPlaying("die")||lastIsIdle!=animation.IsPlaying("idle")||lastIsTakeDamage!=animation.IsPlaying("takedamage")||lastIsWalk!=animation.IsPlaying("walk") )
        {
            TranscriptManager._instance.AddEnemyToSyncAnimation(this);// 把自身传递到管理器里面 统一进行动画的更新
            lastIsAttack = animation.IsPlaying("attack01");
            lastIsDie = animation.IsPlaying("die");
            lastIsIdle = animation.IsPlaying("idle");
            lastIsTakeDamage = animation.IsPlaying("takedamage"); 
            lastIsWalk = animation.IsPlaying("walk");
        }
    }
}
