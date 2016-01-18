using System;
using UnityEngine;
using System.Collections;
using TaidouCommon.Model;

public enum TaskType{
    Main=0,//主线任务
    Reward=1,//赏金任务
    Daily=2//日常任务
}
public enum TaskProgress{
    NoStart=0,
    Accept=1,
    Complete=2,
    Reward=3
}
public class Task  {
//    a)Id
//b)任务类型（Main,Reward，Daily）
//c)名称
//d)图标
//e)任务描述
//f)获得的金币奖励
//g)获得的钻石奖励
//h)跟npc交谈的话语
//i)Npc的id
//j)副本id
//k)任务的状态
//i.未开始
//ii.接受任务
//iii.任务完成
//iv.获取奖励（结束）
    private int id;
    private TaskType taskType;
    private string name;
    private string icon;
    private string des;
    private int coin;
    private int diamond;
    private string talkNpc;
    private int idNpc;
    private int idTranscript;
    private TaskProgress taskProgress = TaskProgress.NoStart;

    public TaskDB TaskDB
    {
        get; set;
    }

    public delegate void OnTaskChangeEvent();
    public event OnTaskChangeEvent OnTaskChange;

    //用来同步任务信息的
    public void SyncTask( TaskDB taskDb )
    {
        TaskDB = taskDb;
        taskProgress = (TaskProgress) taskDb.State;
    }

    public void UpdateTask( TaskManager taskManager )
    {
        if (TaskDB == null)
        {
            TaskDB = new TaskDB();
            TaskDB.State = (int) taskProgress;
            TaskDB.TaskID = id;
            TaskDB.LastUpdateTime = new DateTime();
            TaskDB.Type = (int) taskType;
            taskManager.taskDBController.AddTaskDB(TaskDB);
        }
        else {
            this.TaskDB.State = (int)taskProgress;
            taskManager.taskDBController.UpdateTaskDB(this.TaskDB);
        }
    }

    public int Id {
        get { return id; }
        set { id = value; }
    }

    public TaskType TaskType {
        get { return taskType; }
        set { taskType = value; }
    }

    public string Name {
        get { return name; }
        set { name = value; }
    }

    public string Icon {
        get { return icon; }
        set { icon = value; }
    }

    public string Des {
        get { return des; }
        set { des = value; }
    }

    public int Coin {
        get { return coin; }
        set { coin = value; }
    }

    public int Diamond {
        get { return diamond; }
        set { diamond = value; }
    }

    public string TalkNpc {
        get { return talkNpc; }
        set { talkNpc = value; }
    }

    public int IdNpc {
        get { return idNpc; }
        set { idNpc = value; }
    }

    public int IdTranscript {
        get { return idTranscript; }
        set { idTranscript = value; }
    }

    public TaskProgress TaskProgress {
        get { return taskProgress; }
        set {
            if (taskProgress != value) {
                taskProgress = value;
                OnTaskChange();
            }
            
        }
    }

}
