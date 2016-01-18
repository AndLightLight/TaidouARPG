using UnityEngine;
using System.Collections;

public class PlayerAutoMove : MonoBehaviour {

    private NavMeshAgent agent;
    public float minDistance = 3;


	// Use this for initialization
	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (agent.enabled) {
            if (agent.remainingDistance < minDistance &&agent.remainingDistance!=0 ) {
                agent.Stop();
                agent.enabled = false;
                TaskManager._instance.OnArriveDestination();
            }
        }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (Mathf.Abs(h) > 0.05f || Mathf.Abs(v) > 0.05f) {
            StopAuto();//如果在寻路过程中 有按下移动控制键，那么就停止寻路
        }
	}

    public void SetDestination( Vector3 targetPos ) {
        agent.enabled = true;
        agent.SetDestination(targetPos);
    }

    public void StopAuto() {
        if (agent.enabled) {
            agent.Stop();
            agent.enabled = false;
        }
    }
}
