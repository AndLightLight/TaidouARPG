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

			GameObject logicObj = GameObject.Instantiate(Resources.Load("Prefab/Player/Alice/Alice")) as GameObject;
			logicObj.name = "123456";
			if (null != logicObj)
			{
                GameObject modelObj = logicObj.transform.GetChild(0).gameObject;

                LocalPlayerControl.Instance = logicObj.AddComponent<LocalPlayerControl>();

                NavMeshAgent navMeshAgent = logicObj.AddComponent<NavMeshAgent>();
                navMeshAgent.radius = 0.5f;
                navMeshAgent.height = 1.6f;
                navMeshAgent.baseOffset = -0.1f;
                navMeshAgent.speed = 1;
                navMeshAgent.angularSpeed = 360;
                navMeshAgent.acceleration = 10;
                navMeshAgent.stoppingDistance = 0.05f;
                LocalPlayerControl.Instance.m_navMeshAgent = navMeshAgent;

				HumanoidAvatar humanAvatar = modelObj.AddComponent<HumanoidAvatar>();
                humanAvatar.humanoidControl = LocalPlayerControl.Instance;
                LocalPlayerControl.Instance.m_humanoidAvatar = humanAvatar;

                KeyboardControl control = logicObj.AddComponent<KeyboardControl>();

				//…Ë÷√Layer
				//logicObj.layer = LayerMask.NameToLayer("Model");

				LocalPlayerControl.Instance.SetPosition(pos);
				LocalPlayerControl.Instance.SetRotation(rotation);
                //Utility.AdjustYAxis(LocalPlayerControl.Instance);

                LocalPlayerControl.Instance.Initialize();

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