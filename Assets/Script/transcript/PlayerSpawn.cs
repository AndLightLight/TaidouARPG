using UnityEngine;
using System.Collections;
using TaidouCommon.Model;

public class PlayerSpawn : MonoBehaviour
{

    public Transform[] positionTransformArray;

	// Use this for initialization
	void Awake () {
	    SpawnPlayer();
	}

    void SpawnPlayer()
    {
        if (GameController.Instance.battleType==BattleType.Person)
        {
            //个人战斗角色加载
            Role role = PhotonEngine.Instance.role;
            GameObject playerPrefab;
            if (role.IsMan)
            {
                 playerPrefab = Resources.Load("player-battle/Player-boy") as GameObject;
            }
            else
            {
                playerPrefab = Resources.Load("player-battle/Player-girl") as GameObject;
            }
            GameObject go = GameObject.Instantiate(playerPrefab, positionTransformArray[0].position, Quaternion.identity) as GameObject;
            TranscriptManager._instance.player = go;
            go.GetComponent<Player>().roleID = role.ID;
        }else if (GameController.Instance.battleType==BattleType.Team)
        {
            //团队战斗角色加载
            for (int i = 0; i < 3; i++)
            {
                Role role = GameController.Instance.teamRoles[i];
                Vector3 pos = positionTransformArray[i].position;
                GameObject playerPrefab;
                if (role.IsMan) {
                    playerPrefab = Resources.Load("player-battle/Player-boy") as GameObject;
                } else {
                    playerPrefab = Resources.Load("player-battle/Player-girl") as GameObject;
                }
                GameObject go = GameObject.Instantiate(playerPrefab, pos, Quaternion.identity) as GameObject;
                go.GetComponent<Player>().roleID = role.ID;
                GameController.Instance.AddPlayer(role.ID,go);
                if (role.ID == PhotonEngine.Instance.role.ID)
                {
                    //当前创建的角色是当前客户端控制的
                    TranscriptManager._instance.player = go;
                }
                else
                {
                    //这个角色是其他客户端的  修改移动为不可控
                    go.GetComponent<PlayerMove>().isCanControl = false;
                }
            }
        }
    }
}
