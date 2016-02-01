using UnityEngine;
using System.Collections;

public class KeyboardControl : MonoBehaviour
{

	void Start()
	{

	}

	void Update()
	{
		TouchControl();
	}

	void TouchControl()
	{
		try
		{
			Debug.LogError("layer" + UICamera.hoveredObject.layer);
			Debug.LogError("name" + UICamera.hoveredObject.name);
			Debug.LogError("object" + UICamera.hoveredObject); 

			// 点击事件处理
			if (UICamera.hoveredObject == null)
			{
				Debug.LogError("1  hover");
				if (Input.GetMouseButtonDown(0))
				{
					Debug.LogError("2  mouse");
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

					RaycastHit hit;
					int layerMask = 1 << LayerMask.NameToLayer(GameDefines.LayerTerrain)
						| 1 << LayerMask.NameToLayer(GameDefines.LayerNPC);

					if (Physics.Raycast(ray, out hit, 50, layerMask))
					{
						Debug.LogError("3  ray");
						Vector3 hitPoint = hit.point;
						//if (LayerMask.NameToLayer(GameDefines.LayerNPC) == hit.collider.gameObject.layer)
						//	BroadCastControl bc = hit.collider.GetComponent<BroadCastControl>();

						LocalPlayerControl.Instance.MoveTo(hitPoint);
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
				Vector3 speed = new Vector3(x, 0, z).normalized * LocalPlayerControl.Instance.Speed;
				LocalPlayerControl.Instance.Move(speed);
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
