using UnityEngine;
using System.Collections;

public class TaskUI : MonoBehaviour {
    public static TaskUI _instance;
    private UIGrid taskListGrid;
    private TweenPosition tween;
    private UIButton closeButton;
    public GameObject taskItemPrefab;
    void Awake() {
        _instance = this;
        taskListGrid = transform.Find("Scroll View/Grid").GetComponent<UIGrid>();
        tween = this.GetComponent<TweenPosition>();
        closeButton = transform.Find("CloseButton").GetComponent<UIButton>();

        EventDelegate ed = new EventDelegate(this, "OnClose");
        closeButton.onClick.Add(ed);
    }
    void Start()
    {
        TaskManager._instance.OnSyncTaskComplete += this.OnSyncTaskComplete;
    }


    public void OnSyncTaskComplete()
    {
        InitTaskList();
    }
    /// <summary>
    /// 初始化任务列表信息
    /// </summary>
    void InitTaskList() {
        ArrayList taskList = TaskManager._instance.GetTaskList();

        foreach (Task task in taskList) {
            GameObject go = NGUITools.AddChild(taskListGrid.gameObject, taskItemPrefab);
            taskListGrid.AddChild(go.transform);
            TaskItemUI ti = go.GetComponent<TaskItemUI>();
            ti.SetTask(task);
        }
    }

    public void Show() {
        tween.PlayForward();
    }
    public void Hide() {
        tween.PlayReverse();
    }
    void OnClose() {
        Hide();
    }
}
