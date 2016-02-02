

using UnityEngine;
using System.Collections;


namespace com.jdxk.core
{
	public class SingletonObject<T> : MonoBehaviour where T : SingletonObject<T>
	{
		private static T _instance = null;

		public static T Instance
		{
			get
			{
				if (_instance == null)
				{
					GameObject obj = new GameObject(typeof(T).Name, typeof(T));
					_instance = obj.GetComponent<T>();
					GameObject.DontDestroyOnLoad(obj);

                    _instance.Spawn();
				}
				return _instance;
			}
		}

        protected virtual void Spawn()
        {

        }
	}
}
