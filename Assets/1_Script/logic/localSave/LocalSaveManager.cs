
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LocalSave
{
	public enum DataType
	{
		Player,
		Map,
	}

	public class LocalSaveManager : Singleton<LocalSaveManager>
	{
		private static string m_dirPath = Application.persistentDataPath + "/Save/";
		private static string m_filePath = m_dirPath + "GameData.sav";

		public object Load(DataType type)
		{
			object retObj = null;

			if (!IOHelper.IsDirectoryExists(m_dirPath) || !IOHelper.IsFileExists(m_filePath))
			{
				retObj = null;
			}
			else
			{
				switch (type)
				{
					case DataType.Player:
					retObj = LoadPlayerData();
					break;
				case DataType.Map:
					retObj = LoadMapData();
					break;
				}

			}

			return retObj;
		}

		public void Save(DataType type)
		{
			CheckCreateFile();

			switch (type)
			{
				case DataType.Player:
					SavePlayerData();
					break;
				case DataType.Map:
					SaveMapData();
					break;
			}

		}

		private void CheckCreateFile()
		{
			if (!IOHelper.IsDirectoryExists(m_dirPath))
				IOHelper.CreateDirectory(m_dirPath);

			if (!IOHelper.IsFileExists(m_filePath))
				IOHelper.CreateFile(m_filePath, string.Empty);
		}

		private object LoadPlayerData()
		{
			ObjectAttribute obj = (ObjectAttribute)IOHelper.GetData(m_filePath, typeof(ObjectAttribute));
			
			return null;
		}

		private object LoadMapData()
		{
			return null;
		}

		private void SavePlayerData()
		{
			//TestClass t = new TestClass();
			//IOHelper.SetData(filePath, t);
		}

		private void SaveMapData()
		{

		}

	}

}