using UnityEngine;
using System;
using System.Collections.Generic;

public class Utility
{
    public static Transform FindBone(Transform trans, string name)
    {
        if (trans.name == name)
            return trans;

        for (int i = 0; i < trans.childCount; ++i)
        {
            Transform bone = FindBone(trans.GetChild(i), name);
            if (null != bone)
                return bone;
        }

        return null;
    }

    /*public static Transform FindBone(Transform trans, GameUtility.enBone enB)
    {
        return FindBone(trans, GameUtility.GetBoneName(enB));
    }

    public static GameObject EquipObj(int ResID, Transform Tr)
    {
        GameObject Obj = BundleResource.GetMe().GetResourceInstance(ResID);
        return _EquipObj(Obj, Tr);
    }*/

    private static GameObject _EquipObj(GameObject ObjInstance, Transform Tr)
    {
        if (null == Tr || null == ObjInstance)
            return null;
        ObjInstance.gameObject.transform.parent = Tr;
        ObjInstance.transform.localPosition = Vector3.zero;
        ObjInstance.transform.localRotation = Quaternion.identity;
        ObjInstance.transform.localScale = Vector3.one;
        return ObjInstance;
    }

    public static void DeleteObjsChildren(Transform node, float time = 0)
    {
		if (null == node)
			return;
        foreach (Transform trans in node)
        {
            GameObject.Destroy(trans.gameObject, time);
        }
    }

    public static void DeleteObjsChildrenImmediate(Transform node)
    {
        if (null == node)
            return;

        foreach (Transform trans in node)
        {
            GameObject.DestroyImmediate(trans.gameObject);
        }
    }

    public static void DeleteObjsChildren(Transform node, string name, float time = 0)
    {
        for (int i = 0; i < node.childCount; ++i)
        {
            if (node.GetChild(i).name.Equals(name))
            {
                GameObject obj = node.GetChild(i).gameObject;
                DeleteObjsChildren(obj.transform, time);
            }
        }
    }

    #region 换装公共接口

    public static bool GetRenderState(Transform trans)
    {
        if (null == trans)
            return true;
        SkinnedMeshRenderer renderer = trans.GetComponent<SkinnedMeshRenderer>();
        if (null == renderer)
            return true;
        return renderer.enabled;
    }
    public static void SetRenderState(Transform trans, bool active)
    {
        if (null == trans)
            return;
        Renderer renderer = trans.GetComponentInChildren<Renderer>();
        if (null == renderer)
            return;
        renderer.enabled = active;
    }

    //获取SkinnedMeshRender组件所在的物体名称
    public static string GetSkinnedMeshRenderName(Transform node)
    {
        string strName = "";
        SkinnedMeshRenderer skinnedMeshRenderer = node.GetComponentInChildren<SkinnedMeshRenderer>();
        if (null != skinnedMeshRenderer && null != skinnedMeshRenderer.gameObject)
            strName = skinnedMeshRenderer.gameObject.name;
        return strName;
    }

	//换装逻辑（换模型的子物体，不动模型身上挂载的脚本）
	public static void ChangeMeshAndMat(GameObject srcObj, GameObject desObj, string srcName, string desName, string boneName)
	{
        if (null == srcObj)
			return;

		//换骨骼点
        Transform trans_srcBone = srcObj.transform.FindChild(boneName);
		if(null != trans_srcBone)
		{
			GameObject.DestroyImmediate(trans_srcBone.gameObject);
		}

        Transform trans_desBone = desObj.transform.FindChild(boneName);
		if(null != trans_desBone)
		{
			trans_desBone.name = boneName;
            trans_desBone.parent = srcObj.transform;
			trans_desBone.localPosition = Vector3.one;
		}
		
		//换mat
        Transform trans_srcMat = srcObj.transform.FindChild(srcName);
		if(null != trans_srcMat)
		{
			GameObject.DestroyImmediate(trans_srcMat.gameObject);
		}

        Transform trans_desMat = desObj.transform.FindChild(desName);
		if(null != trans_desMat)
		{
			trans_desMat.name = desName;
            trans_desMat.parent = srcObj.transform;
			trans_desMat.localPosition = Vector3.one;
		}
	}

    #endregion

    public static void LookAtTargetImmediately(IObject src, IObject des)
    {
        Vector3 dir = des.GetPosition() - src.GetPosition();
        dir.y = 0f;
        if (dir.sqrMagnitude > 0.01f)
        {
            src.SetRotation(Quaternion.LookRotation(dir));
        }
    }

    public static void LookAtTargetImmediately(IObject src, Vector3 dir)
    {
        //Vector3 dir = pos - src.GetPosition();
        dir.y = 0f;
        if (dir.sqrMagnitude > 0.01f)
        {
            src.SetRotation(Quaternion.LookRotation(dir));
        }
    }

    public static void LookAtTargetSlerp(IObject src, IObject des, float speed)
    {
        Vector3 dir = des.GetPosition() - src.GetPosition();
        dir.y = 0;
        if (dir.sqrMagnitude > 0.01f)
        {
            src.SetRotation(Quaternion.Slerp(
                src.GetRotation(),
                Quaternion.LookRotation(dir),
                Mathf.Clamp01(speed * Time.deltaTime)));
        }
    }

    public static void LookAtTargetSlerp(IObject src, Vector3 dir, float speed)
    {
        //Vector3 dir = pos - src.GetPosition();
        dir.y = 0;
        if (dir.sqrMagnitude > 0.01f)
        {
            src.SetRotation(Quaternion.Slerp(
                src.GetRotation(),
                Quaternion.LookRotation(dir),
                Mathf.Clamp01(speed * Time.deltaTime)));
        }
    }

    public static void DestroyChildrenObjs(Transform trans)
    {
        foreach (Transform child in trans)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public static void DestroyChildrenObjsImmediate(Transform trans)
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in trans)
        {
            children.Add(child);
        }

        for (int i = 0; i < children.Count; ++i)
            GameObject.DestroyImmediate(children[i].gameObject);
    }

    public static void SwitchEffect(Transform node, bool active)
    {
        if (node == null)
        {
            return;
        }
        for (int i = 0; i < node.childCount; ++i)
        {
            Transform trans = node.GetChild(i);
            ParticleSystem particleSystem = trans.GetComponent(typeof(ParticleSystem)) as ParticleSystem;
            if (particleSystem != null)
            {
                particleSystem.enableEmission = active;
            }

            SwitchEffect(trans, active);
        }
    }

    public static void SetLayer(Transform trans, int layer)
    {
        for (int i = 0; i < trans.childCount; ++i)
        {
            trans.GetChild(i).gameObject.layer = layer;
            SetLayer(trans.GetChild(i), layer);
        }
        trans.gameObject.layer = layer;
    }

    public static void LogCallStack()
    {
        System.Text.StringBuilder SB = new System.Text.StringBuilder();
        var CS = new System.Diagnostics.StackTrace();
        var SF = CS.GetFrames();
        for (int x = 0; x < SF.Length; ++x)
        {
            SB.Append(SF[x].GetMethod().Name).Append("\n");
        }
        LogManager.Log(SB.ToString() , LogType.Normal);
    }

    // 调整角色的Y坐标
    static public void AdjustYAxis(Transform target)
    {
        Ray ray = new Ray(new Vector3(target.position.x, 1000f, target.position.z), Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, 1 << LayerMask.NameToLayer(GameDefines.LayerTerrain)))
        {
            target.position = hit.point + Vector3.up * 0.1f;
        }
    }

    public static void AdjustYAxis(IObject player)
    {
        // navmesh
        NavMeshHit NMH;
        if (NavMesh.SamplePosition(player.GetPosition() + Vector3.up * 1, out NMH, 2f, -1))
        {
            player.SetPosition(NMH.position);
        }
        else
        {
            AdjustYAxis(player.transform);
         //   LogManager.Log("NavMesh.SamplePosition() fail! pos:" + player.GetPosition());
        }
    }

    // 调整角色的Y坐标
    static public float GetTerrainY(Transform target)
    {
        // 模型
        float posY = 0f;
        Ray ray = new Ray(new Vector3(target.position.x, 1000f, target.position.z), Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, 1 << LayerMask.NameToLayer(GameDefines.LayerTerrain)))
        {
            posY = hit.point.y + 0.1f;
        }
        return posY;
    }

    //计算某个点的高度值
    static public float GetTerrainY(Vector3 target)
    {
        // 模型
        float posY = 0f;
        Ray ray = new Ray(new Vector3(target.x, 1000f, target.z), Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, 1 << LayerMask.NameToLayer(GameDefines.LayerTerrain)))
        {
            posY = hit.point.y + 0.1f;
        }
        return posY;
    }

    //通过射线获得某一xz坐标上,地标高y的值
    static public bool GetTerrainY(Vector3 Vin, out Vector3 Vout)
    {
        RaycastHit hit;
        Vector3 vTop = Vin;
        vTop.y = 1000f;
        if (Physics.Raycast(vTop, Vector3.down, out hit, 1 << LayerMask.NameToLayer(GameDefines.LayerTerrain)))
        {
            Vout = hit.point + Vector3.up * 0.1f;
            return true;
        }
        Vout = Vin;
        return false;
    }

    static public bool GetTerrainY(float posX, float posZ, out float posY)
    {
        RaycastHit hit;
        Vector3 vTop = new Vector3(posX, 1000f, posZ);
        if (Physics.Raycast(vTop, Vector3.down, out hit, 1 << LayerMask.NameToLayer(GameDefines.LayerTerrain)))
        {
            posY = hit.point.y + Vector3.up.y * 0.1f;
            return true;
        }
        posY = 0;
        return false;
    }

    public static float GetNavMeshHeight(Vector3 pos)
    {
        // navmesh
        NavMeshHit NMH;
        if (NavMesh.SamplePosition(pos + Vector3.up * 1, out NMH, 2f, -1))
        {
            return NMH.position.y;
        }
        else
        {
            return pos.y;
        }
    }
    //
    static public string Bytes2HEX(byte[] raw)
    {
        var SB = new System.Text.StringBuilder();
        byte[] B2 = new byte[2];
        for (int x = 0; x < raw.Length; ++x)
        {            
            B2[0] = (byte)(raw[x] >> 4);
            B2[1] = (byte)(raw[x] & 0x0F);
            for (int y = 0; y < B2.Length; ++y)
            {
                byte B = B2[y];
                if (B < 10)
                {
                    SB.Append((char)('0' + B));
                }
                else
                {
                    SB.Append((char)('A' + (B - 10)));
                }
            }
        }
        return SB.ToString();
    }

    static public string GetShortenNum(long num)
    {
        System.Text.StringBuilder strNum = new System.Text.StringBuilder();
        if (num > 100000000)
        {
            num /= 100000000;
            strNum.Append(num.ToString() + "亿");
        }

        if (num > 1000000)
        {
            num /= 10000;
            strNum.Append(num.ToString() + "万");
            return strNum.ToString();
        }
        
        if (num >= 0)
        {
            strNum.Append(num.ToString());
        }

        return strNum.ToString();
    }
    static public string GetShortNum(long num)
    {
        System.Text.StringBuilder strNum = new System.Text.StringBuilder();
        if (num > 10000)
        {
            float n = num / (10000f);
            strNum.Append(String.Format("{0:F1}", n) + "万");
            return strNum.ToString();
        }

        if (num >= 0)
        {
            strNum.Append(num.ToString());
        }

        return strNum.ToString();
    }
    static public string GetShortNumOne(long num)
    {
        System.Text.StringBuilder strNum = new System.Text.StringBuilder();
        if (num > 10000)
        {
            int n = Mathf.CeilToInt(num /(10000f));
            strNum.Append(String.Format("{0}", n) + "万");
            return strNum.ToString();
        }

        if (num >= 0)
        {
            strNum.Append(num.ToString());
        }

        return strNum.ToString();
    }



    public static bool IsSamePosition3D(Vector3 pos1, Vector3 pos2)
    {
        Vector3 dis = pos2 - pos1;
        if (dis.sqrMagnitude > 0.01f)
            return false;
        else
            return true;
    }

    public static bool IsSamePosition2D(Vector3 pos1, Vector3 pos2)
    {
        Vector2 dis = new Vector2(pos2.x - pos1.x, pos2.z - pos1.z);
        if (dis.sqrMagnitude > 0.01f)
            return false;
        else
            return true;
    }
}
