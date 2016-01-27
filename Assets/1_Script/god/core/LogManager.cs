using UnityEngine;
using System.Collections;

public enum LogType
{
	Normal = 0,
	Assert = 1,
	Warning = 2,
	Error = 3,
	Fatal = 4,
}

public static class LogManager
{
    public static void Init(string path, string name, bool test)
    {
        //LogFileManager.Init(path, name, test);
    }

    public static void Close()
    {
        //LogFileManager.Close();
    }

    public static void Log(string message, LogType type = LogType.Normal)
    {
        //LogFileManager.Log(message, type);
#if UNITY_EDITOR
        switch (type)
        {
            case LogType.Normal:
                Debug.Log(message);
                break;
            case LogType.Assert:
                Debug.LogWarning(message);
                break;
            case LogType.Error:
            case LogType.Fatal:
                Debug.LogError(message);
                break;
            default:
                Debug.Log(message);
                break;
        }
#endif
    }
}
