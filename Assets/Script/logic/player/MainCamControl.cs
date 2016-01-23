
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainCamControl : MonoBehaviour
{
	public static MainCamControl Instance { get; set; }

	public Transform target;
	public bool bStayPlayerHead = true;

	// camera positon
	public float Distance = 3;
	public float Height = 1;
	public float Offset = 1;

	//用于控制摄像机旋转的参数
	public float XRotate = 3f;
	public float YRotate = 1f;

	// camera lerp parameter
	public float CameraCatchLerp = 25;

	//Add by dr 2015-03-02 camera fov parameter
	public float ModifyHeight;
	public float ModifyDistance;
	public static bool isAllowScroll = true;
	public static bool isThroughAgentToLookAtTarget = true;


	void Awake()
	{
		Instance = this;
	}



	void Start()
	{
		if (ModifyHeight == 0)
		{
			ModifyHeight = Height;
		}
		if (ModifyDistance == 0)
		{
			ModifyDistance = Distance;
		}

		//camera fov
#if (UNITY_IPHONE || UNITY_ANDROID) && !UNITY_EDITOR
        gameObject.AddComponent<CameraSmoothControl>();
#endif

		// for editor
#if MAP_EDITOR && UNITY_EDITOR
        gameObject.AddComponent(typeof(SceneEditor));
#endif

	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void LateUpdate()
	{
		AdjustCamera();
	}

	bool force = true;
	private void AdjustCamera()
	{
		//针对摄像机平滑写的计算公式 Height最多改变50%; Distance最多改变30%
		if (isAllowScroll)
		{
			Height = (0.5f * CameraSmoothControl.ScrollFactor + 0.5f) * ModifyHeight;
			Distance = 0.6f * ModifyDistance * Height / ModifyHeight + 0.4f * ModifyDistance;
		}

		//摄像机计算公式
		Vector3 dir = new Vector3(Offset, Height, -Distance);
		dir = Vector3.Normalize(dir) * Distance;

		Vector3 pos = LocalPlayerControl.Instance.GetPosition() + dir;
		float angel = XRotate * Distance + YRotate;

		if (force)
		{
			transform.position = pos;
			transform.rotation = Quaternion.Euler(angel, 0, 0);
			force = false;
			return;
		}

#if UNITY_IPHONE
		CameraCatchLerp = 5f;
#endif
#if UNITY_ANDROID
        CameraCatchLerp = 25f;
#endif
		float detlax = transform.position.x >= pos.x ? (transform.position.x - pos.x) / CameraCatchLerp : (pos.x - transform.position.x) / CameraCatchLerp;
		float detlay = transform.position.y >= pos.y ? (transform.position.y - pos.y) / CameraCatchLerp : (pos.y - transform.position.y) / CameraCatchLerp;
		float detlaz = transform.position.z >= pos.z ? (transform.position.z - pos.z) / CameraCatchLerp : (pos.z - transform.position.z) / CameraCatchLerp;

		transform.position =
			new Vector3(
				Mathf.Lerp(transform.position.x, pos.x, detlax),
				Mathf.Lerp(transform.position.y, pos.y, detlay),
				Mathf.Lerp(transform.position.z, pos.z, detlaz)
				);

		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(angel, 0, 0), Time.smoothDeltaTime);
	}

}
