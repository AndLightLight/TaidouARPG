using System;
using UnityEngine;
using System.Collections;
using System.Net.Configuration;

public class PlayerMove : MonoBehaviour {

    public float velocity = 5;
    public bool isCanControl = true;//表示是否可以使用键盘控制
    private Animator anim;
    private PlayerAttack playerAttack;
    private Vector3 lastPosition = Vector3.zero;
    private Vector3 lastEulerAngles = Vector3.zero;
    private bool isMove = false;
    private DateTime lastUpdateTime  =DateTime.Now;
    private BattleController battleController;
    void Start() {
        anim = this.GetComponent<Animator>();
        playerAttack = this.GetComponent<PlayerAttack>();
        if (GameController.Instance.battleType == BattleType.Team && isCanControl)
        {
            battleController = GameController.Instance.GetComponent<BattleController>();
            InvokeRepeating("SyncPositionAndRotation",0, 1f/30 );
            InvokeRepeating("SyncMoveAnimation",0,1f/30);
        }
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (isCanControl == false) return;
	    if (playerAttack!=null&& playerAttack.hp <= 0) return;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 nowVel = rigidbody.velocity;
        if (Mathf.Abs(h) > 0.05f || Mathf.Abs(v) > 0.05f) {
            anim.SetBool("Move", true);
            if (anim.GetCurrentAnimatorStateInfo(1).IsName("Empty State")) {
                rigidbody.velocity = new Vector3(velocity * h, nowVel.y, v * velocity);
                transform.LookAt(new Vector3(h, 0, v) + transform.position);
            } else {
                rigidbody.velocity = new Vector3(0, nowVel.y, 0);
            }
        } else {
            anim.SetBool("Move", false);
            rigidbody.velocity = new Vector3(0 , nowVel.y,0);
        }
	}
    //同步当前角色的位置和旋转 发起请求的
    void SyncPositionAndRotation()
    {
        Vector3 position = transform.position;
        Vector3 eulerAngles = transform.eulerAngles;
        if (position.x != lastPosition.x || position.y != lastPosition.y || position.z != lastPosition.z ||
            eulerAngles.x != lastEulerAngles.x || eulerAngles.y != lastEulerAngles.y ||
            eulerAngles.z != lastEulerAngles.z )
        {
            //进行同步
            battleController.SyncPositionAndRotation(position,eulerAngles);
            lastPosition = position;
            lastEulerAngles = eulerAngles;
        }
    }

    public void SetPositionAndRotation(Vector3 position, Vector3 eulerAngles)
    {
        transform.position = position;
        transform.eulerAngles = eulerAngles;
    }

    void SyncMoveAnimation()//同步移动的动画
    {
        if (isMove != anim.GetBool("Move"))//当前动画状态发生了改变 需要同步
        {
            //发送同步的请求
            PlayerMoveAnimationModel model = new PlayerMoveAnimationModel() { IsMove = anim.GetBool("Move") };
            model.SetTime(DateTime.Now);
            battleController.SyncMoveAnimation(model);
            isMove = anim.GetBool("Move");
        }
    }

    public void SetAnim(PlayerMoveAnimationModel model )
    {
        //print(model.Time + ":" + model.IsMove);
        DateTime dt = model.GetTime();
        if (dt> lastUpdateTime)
        {
            anim.SetBool("Move",model.IsMove);
            lastUpdateTime = dt;
        }
    }
}
