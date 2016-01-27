
using UnityEngine;
using System.Collections;

public abstract class IObject : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
        SelfStart();
	}
	
	// Update is called once per frame
	void Update () 
	{
        SelfUpdate();
	}

    protected abstract void SelfStart();

    protected abstract void SelfUpdate();

    // transform
    public virtual void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetRotation(int dir)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, dir, 0));
    }

    public void SetRotation(Quaternion rot)
    {
        transform.rotation = rot;
    }

    public Quaternion GetRotation()
    {
        return transform.rotation;
    }

    public float GetDirection()
    {
        return transform.rotation.eulerAngles.y;
    }

    public void SetScale(float scale)
    {
        transform.localScale = scale * Vector3.one;
    }

    public void SetParent(Transform parent)
    {
        transform.parent = parent;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
