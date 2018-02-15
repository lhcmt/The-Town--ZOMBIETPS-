using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System.Xml.Linq;
using System; 
using UnityEngine.UI;

public class TaskManager : MonoSingletion<TaskManager> {

    public Text TaskUIText;

    public List<Task> taskList = new List<Task>();



    void Start()
    {
        TaskUIText = FindObjectOfType<PlayerUI>().TaskUI;
        Level1();

    }

    void Update()
    {
        UpdateTaskOnUI();
    }

    //new a Task
    //可以做成从文件中读入
    public Task CreateTask(string taskID,string taskName,string caption)
    {
        Task newTask = new Task(taskID);
        newTask.taskName = taskName;
        newTask.caption = caption;
        taskList.Add(newTask);
        return newTask;
    }

    //在UI上显示所有任务
    public void UpdateTaskOnUI()
    {
        if (TaskUIText != null)
        {
            TaskUIText.text = "";
            foreach (Task task in taskList)
            {
                TaskUIText.text +=
                    task.taskName + ":"+ 
                    "\n" +
                    task.caption;
                foreach (TaskCondition taskCondition in task.taskConditions)
                {
                    TaskUIText.text += "\n" + taskCondition.nowAmount + "//" + taskCondition.targetAmount;
                }
                TaskUIText.text += "\n\n";
            }
        }
    }



    //任务完成后删除某个任务
    public void DeleteTask(string taskIDtoDelete)
    {
        foreach(Task task in taskList)
        {
            if(task.taskID ==taskIDtoDelete)
            {
                taskList.Remove(task);
                return;
            }
        }
    }

    //游戏流程暂时写在这里
    void Level1()
    {
        Task newTask = CreateTask(
        "task001",
        "逃出小镇",
        "你所在的小镇爆发了丧尸病毒，你需要生存下去，找到逃出去的办法");

        newTask = CreateTask(
            "task002",
            "击杀丧尸！",
            "击杀10个丧尸");
        newTask.AddContiditions(901, 0, 10);
    }

}
