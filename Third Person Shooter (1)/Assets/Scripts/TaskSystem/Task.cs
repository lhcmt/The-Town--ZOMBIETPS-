/*
 * 2017/11/26
 * 任务类
 * 用于创建单个任务
 */

using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System;  


public class Task {


    public string taskID;//任务ID  
    public string taskName;//任务名字  
    public string caption;//任务描述   
    public bool taskState;//true 任务激活，Fasle取消

    public List<TaskCondition> taskConditions = new List<TaskCondition>(); //任务条件

    public Task(string taskID)
    {
        this.taskID = taskID;
        this.taskState = true;


        //可以添加从文件中文本读入任务信息

    }

    public void AddContiditions(int id, int nowAmount, int targetAmount)
    {
        TaskCondition taskCondition = new TaskCondition(id,nowAmount,targetAmount);
        taskConditions.Add(taskCondition);
    }

    //任务物品发生变化时调用
    //检查任务的每个完成条件
    public bool Check(TaskEventArgs taskEventArgs)
    {
        TaskCondition tc;
        for (int i = 0; i < taskConditions.Count; i++)
        {
            tc = taskConditions[i];
            if (tc.id == taskEventArgs.id)
            {
                tc.nowAmount += taskEventArgs.amount;
                tc.CheckCondition();
            }
        }

        //如果所有条件都完成，返回true
        for (int i = 0; i < taskConditions.Count; i++)
        {
            if (!taskConditions[i].isFinish)
            {
                return true; 
            }
        }
        return false;

    }
    public void Reward()
    {
        //


    }

    //取消任务  
    public void Cancel()
    {
        taskState = false;
    } 

    public void TaskComplete()
    {

    }
}

public class TaskEventArgs
{
    public string taskID;//任务ID
    public int id;//条件ID 
    public int amount;//条件需求数量
}
