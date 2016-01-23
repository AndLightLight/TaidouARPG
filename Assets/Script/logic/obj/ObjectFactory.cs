using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class ObjectFactory : Singleton<ObjectFactory>
{
	static public LocalPlayerControl CreateLocalPlayer(int id, float x, float y, float z)
	{
		try
		{
			
			Vector3 pos = new Vector3(x, y, z);
			//pos.y = Utility.GetTerrainY(pos);
			Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, 0));


			GameObject logicObj = Resources.Load("") as GameObject;
			logicObj.name = "123456";
			if (null != logicObj)
			{
				GameObject modelObj = logicObj.transform.GetChild(0).gameObject;

				// 添加Dummy Player
				HumanoidAvatar humanAvatar = modelObj.AddComponent<HumanoidAvatar>();
				//humanAvatar.dummyType = GameDefines.DummyType.PlayerSelf;


				// 添加CharacterController组件
				CharacterController charCtrl = logicObj.AddComponent<CharacterController>();
				charCtrl.radius = 0f;
				charCtrl.center = Vector3.up;
				charCtrl.height = 2f;
				charCtrl.slopeLimit = 45;
				charCtrl.stepOffset = 0.3f;

				// 添加AIControl
				logicObj.AddComponent<KeyboardControl>();

				// 添加LocalPlayerControl
				LocalPlayerControl.Instance = logicObj.AddComponent<LocalPlayerControl>();
				//LocalPlayerControl.Instance.trail = trail;

				//设置Layer
				logicObj.layer = LayerMask.NameToLayer("Model");

				// 设置transfom
				LocalPlayerControl.Instance.SetPosition(pos);
				//Utility.AdjustYAxis(LocalPlayerControl.Instance);
				LocalPlayerControl.Instance.SetRotation(rotation);

				// set PlayerObj
				humanAvatar.humanoidControl = LocalPlayerControl.Instance;

				/*LocalPlayerControl.Instance.guid = charInfo.PlayerGuid;
				LocalPlayerControl.Instance.RoleId = charInfo.CharId;
				LocalPlayerControl.Instance.playerId = charInfo.CharId;
				LocalPlayerControl.Instance.speed = 0.01f * charInfo.MoveSpeed;
				LocalPlayerControl.Instance.fightCamp = charInfo.FightCamp;*/

				return LocalPlayerControl.Instance;
			}
			else
			{
				LogManager.Log("Player Load Error: ", LogType.Error);
				return null;
			}
		}
		catch (UnityException uex)
		{
			LogManager.Log(uex.ToString(), LogType.Fatal);
			return null;
		}
		catch (Exception ex)
		{
			LogManager.Log(ex.ToString(), LogType.Fatal);
			return null;
		}
	}
}