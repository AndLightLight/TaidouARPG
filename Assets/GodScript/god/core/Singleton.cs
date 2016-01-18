

using System;


public class Singleton<T> where T : new()
{
	private static T _instance = null;

	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new T();
			}
			return _instance;
		}
	}

}
