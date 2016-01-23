using UnityEngine;
using System.Collections;

public class KeyboardControl : MonoBehaviour
{

	void Start()
	{

	}

	void Update()
	{

	}

	void TouchControl()
	{
		try
		{
			// 点击事件处理
			if (UICamera.hoveredObject == null)
			{
				if (Input.GetMouseButtonDown(0))
				{
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

					RaycastHit hit;
					int layerMask = 0;/*(1 << LayerMask.NameToLayer(GameDefines.LayerTerrain) | 1 << LayerMask.NameToLayer(GameDefines.LayerNPC) |*/

					if (Physics.Raycast(ray, out hit, 50, layerMask))
					{
						/*Vector3 point;
						if (LayerMask.NameToLayer(GameDefines.LayerNPC) == hit.collider.gameObject.layer)
						{	BroadCastControl bc = hit.collider.GetComponent<BroadCastControl>();
							point = hit.point;
						}*/
					}
				}
				else if (Input.GetMouseButtonUp(0))
				{
				}

			}

			// keyboard
			int x = 0;
			int z = 0;

			if (Input.GetKeyUp(KeyCode.W) && !(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
				|| Input.GetKeyUp(KeyCode.S) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
				|| Input.GetKeyUp(KeyCode.A) && !(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D))
				|| Input.GetKeyUp(KeyCode.D) && !(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W))
				)
			{
				return;
			}
			if (Input.GetKey(KeyCode.W))
			{
				z += 1;
			}
			if (Input.GetKey(KeyCode.S))
			{
				z -= 1;
			}
			if (Input.GetKey(KeyCode.A))
			{
				x -= 1;
			}
			if (Input.GetKey(KeyCode.D))
			{
				x += 1;
			}

			if (x != 0 || z != 0)
			{
				//Vector3 speed = new Vector3(x, 0, z).normalized * PersonControl.Instance.speed;
				//PersonControl.Instance.Run(speed, GameDefines.RunType.JoyStick, 0.01f);
			}

			// skill
			if (Input.GetKey(KeyCode.Space))
			{

			}
			if (Input.GetKeyDown(KeyCode.J))
			{

			}
			if (Input.GetKeyDown(KeyCode.K))
			{

			}
			if (Input.GetKeyDown(KeyCode.L))
			{

			}
		}
		catch (System.Exception e)
		{

		}
	}

}
