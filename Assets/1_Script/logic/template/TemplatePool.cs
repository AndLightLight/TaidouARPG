

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


//using Data = ;
//using StrData = Dictionary<string, BaseConfig>;

public class ConfigPool : Singleton<ConfigPool>
{
	private const int jsonStartIndex = 4;
	Dictionary<Type, Dictionary<int, BaseConfig>> DataPoolDic = new Dictionary<Type, Dictionary<int, BaseConfig>>();
	Dictionary<Type, Dictionary<string, BaseConfig>> StrDataPoolDic = new Dictionary<Type, Dictionary<string, BaseConfig>>();
	public bool HasBuilt = false;

	WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

	private string zipPath = "";
	private string tablePath = "";

	private bool InitTableBundle()
	{
		zipPath = Application.streamingAssetsPath + "/Table/Table.assetbundle";
		tablePath = zipPath.Replace(Application.streamingAssetsPath, Application.persistentDataPath);

		if (!File.Exists(tablePath))
		{
			//UpdateMe.MakeFileDir(tablePath);
			if (File.Exists(zipPath))
			{
				//解压文件
				/* if (LzmaCompression.Decode(zipPath, tablePath))
				 {
				 }
				 else
				 {
					 return false;
				 }*/
			}
			else
			{
				//提示，然后退出
				return false;
			}
		}
		else
		{
			//存在，说明不是第一次了.
		}

		return true;
	}

	public IEnumerator Build()
	{
		if (HasBuilt)
		{//已经build过了,直接返回
			LogManager.Log("Skip ConfigPool.Build() , _isBuilt == true", LogType.Warning);
			yield break;
		}

		//是否从bundle加载表格数据
		bool bLoadFromBundle = false;   //这里以后默认是true
		//#if UNITY_EDITOR
		//bLoadFromBundle = DebugInfo.UseBundleInEditor;
		//#endif

		//测试用
		//   bLoadFromBundle = true;

		if (bLoadFromBundle)
		{//从bundle加载

			if (!InitTableBundle())
			{
				yield break;
			}

			AssetBundle AB = AssetBundle.CreateFromFile(tablePath);
			if (null == AB)
			{
				throw new Exception("Load json.assetbundle fail!");
			}

			//LoadFormBundle<PreLoad_Tbl>(AB, "PreLoad.json");


			//LoadFormBundle<ItemGet_Tbl>(AB, "ItemGet.json");//获得物品超链接
			//释放资源
			AB.Unload(true);
		}
		else
		{//从原始资源加载
			//LoadRes<PreLoad_Tbl>("PreLoad.json");
			yield return waitForEndOfFrame;
			//LoadRes<ItemGet_Tbl>("ItemGet.json");//获得物品超链接
		}


		/*DataManager.Instance.Init();
		yield return waitForEndOfFrame;
		VipManager.Instance.Init();*/

		//标识build过了
		HasBuilt = true;
	}

	public override void Release()
	{
		foreach (var node in DataPoolDic)
		{
			node.Value.Clear();
		}
		DataPoolDic.Clear();

		foreach (var node in StrDataPoolDic)
		{
			node.Value.Clear();
		}
		StrDataPoolDic.Clear();

		base.Release();
	}

	public void LoadRes<T>(string path, bool reload = false) where T : BaseConfig, new()
	{
		if (string.IsNullOrEmpty(path))
		{
			return;
		}

		path = "Table/" + path;
		if (DataPoolDic.ContainsKey(typeof(T)) || StrDataPoolDic.ContainsKey(typeof(T)))
		{
			if (reload)
			{
				if (DataPoolDic.ContainsKey(typeof(T)))
					DataPoolDic.Remove(typeof(T));
				if (StrDataPoolDic.ContainsKey(typeof(T)))
					StrDataPoolDic.Remove(typeof(T));
			}
			else
			{
				return;
			}
		}

		try
		{
			string RawJson = null;
#if UNITY_EDITOR
			if (reload)
			{
				RawJson = System.IO.File.ReadAllText(path);
			}
			else
			{
				var ta = Resources.Load(path) as TextAsset;
				RawJson = ta.text;
			}
#else
                    var ta = Resources.Load(path) as TextAsset;
                    RawJson = ta.text;
#endif

			Dictionary<int, BaseConfig> dic = new Dictionary<int, BaseConfig>();
			Dictionary<string, BaseConfig> strDic = new Dictionary<string, BaseConfig>();
			DataPoolDic.Add(typeof(T), dic);
			StrDataPoolDic.Add(typeof(T), strDic);

			JArray ja = (JArray)JsonConvert.DeserializeObject(RawJson);
			int JsonNodeCount = ja.Count;
			for (int i = jsonStartIndex; i < JsonNodeCount; i++)
			{
				try
				{
					T t = new T();
					JObject obj = (JObject)ja[i];
					t.init(obj);
					if (0 != t.id)
					{
						if (!dic.ContainsKey(t.id))
						{
							dic.Add(t.id, t);
						}
						else
						{
							LogManager.Log("Config Data Ready Exist, TableName: " + t.GetType().Name + " ID:" + t.id, LogType.Error);
						}
					}
					else
					{
						if (!strDic.ContainsKey(t.strId))
						{
							strDic.Add(t.strId, t);
						}
						else
						{
							LogManager.Log("Config Data Ready Exist, TableName: " + t.GetType().Name + " ID:" + t.strId, LogType.Error);
						}
					}
				}
				catch (Exception)
				{
					LogManager.Log(typeof(T).ToString() + " ERROR!!! line " + (i + 2).ToString(), LogType.Error);
				}
			}

		}
		catch (UnityException uex)
		{
			LogManager.Log(uex.ToString(), LogType.Error);
		}
		catch (Exception e)
		{
			LogManager.Log(e.ToString(), LogType.Error);
		}
	}

	//从bundle中读取表格
	public bool LoadFormBundle<T>(AssetBundle AB, string JsonName) where T : BaseConfig, new()
	{
		bool bRet = false;
		do
		{
			if (string.IsNullOrEmpty(JsonName))
				break;
			TextAsset ta = AB.LoadAsset(JsonName) as TextAsset;
			if (null == ta)
				break;
			try
			{
				Dictionary<int, BaseConfig> dic = new Dictionary<int, BaseConfig>();
				Dictionary<string, BaseConfig> strDic = new Dictionary<string, BaseConfig>();
				DataPoolDic.Add(typeof(T), dic);
				StrDataPoolDic.Add(typeof(T), strDic);
				JArray ja = (JArray)JsonConvert.DeserializeObject(ta.text);
				int JsonNodeCount = ja.Count;
				for (int i = jsonStartIndex; i < JsonNodeCount; i++)
				{
					try
					{
						T t = new T();
						JObject obj = (JObject)ja[i];
						t.init(obj);
						if (0 != t.id)
						{
							if (!dic.ContainsKey(t.id))
							{
								dic.Add(t.id, t);
							}
							else
							{
								LogManager.Log("Config Data Ready Exist, TableName: " + t.GetType().Name + " ID:" + t.id, LogType.Error);
							}
						}
						else
						{
							if (!strDic.ContainsKey(t.strId))
							{
								strDic.Add(t.strId, t);
							}
							else
							{
								LogManager.Log("Config Data Ready Exist, TableName: " + t.GetType().Name + " ID:" + t.strId, LogType.Error);
							}
						}
					}
					catch (Exception)
					{
						LogManager.Log(typeof(T).ToString() + " ERROR!!! line " + (i + 2).ToString(), LogType.Error);
					}
				}
				bRet = true;
			}
			catch (Exception e)
			{
				LogManager.Log(e.ToString(), LogType.Error);
			}
		} while (false);
		return bRet;
	}

	public BaseConfig GetDataByKey(Type type, int key)
	{
		if (DataPoolDic.ContainsKey(type) && GetDataPool(type).ContainsKey(key))
		{
			return GetDataPool(type)[key];
		}
		if (key > 0)
		{
			LogManager.Log("Config Data Is Null : " + type.Name + " key: " + key, LogType.Error);
		}
		return null;
	}

	public BaseConfig GetDataByKey(Type type, string strKey)
	{
		if (StrDataPoolDic.ContainsKey(type) && GetStrDataPool(type).ContainsKey(strKey))
		{
			return GetStrDataPool(type)[strKey];
		}

		LogManager.Log("Config Data Is Null : " + type.Name + " key: " + strKey, LogType.Error);
		return null;
	}

	public T GetDataByKey<T>(int key) where T : BaseConfig
	{
		return GetDataByKey(typeof(T), key) as T;
	}

	public T GetDataByKey<T>(string strKey) where T : BaseConfig
	{
		return GetDataByKey(typeof(T), strKey) as T;
	}

	public Dictionary<int, BaseConfig> GetDataPool<T>()
	{
		return GetDataPool(typeof(T));
	}

	public Dictionary<string, BaseConfig> GetStrDataPool<T>()
	{
		return GetStrDataPool(typeof(T));
	}

	public Dictionary<int, BaseConfig> GetDataPool(Type type)
	{
		if (DataPoolDic.ContainsKey(type))
		{
			return DataPoolDic[type];
		}

		return null;
	}

	public Dictionary<string, BaseConfig> GetStrDataPool(Type type)
	{
		if (StrDataPoolDic.ContainsKey(type))
		{
			return StrDataPoolDic[type];
		}

		return null;
	}

	public int GetPoolSize(Type type)
	{
		if (DataPoolDic.ContainsKey(type))
		{
			return DataPoolDic[type].Count;
		}

		return 0;
	}

	public IEnumerator GetElement(Type type)
	{
		return null;
	}
}

