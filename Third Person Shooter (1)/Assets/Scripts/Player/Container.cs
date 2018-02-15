using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
 * 物品类
 * ID,
 * 名字
 * 最大数量
 * 当前数量
 */
[System.Serializable]
public class ContainerItem
{
    public int Id;
    public string Name;
    public int Maximum;
    public int currentNum;
    public int Remaining {get{return currentNum;}}
    public int Get(int value)
    {
        if (currentNum - value < 0)
        {
            int toMuch = currentNum;//超出数量
            currentNum = 0;
            return toMuch;
            //list delete
        }
        currentNum -= value;
        return value;
    }

    public void Set(int amount)
    {
        currentNum += amount;
        //限制最大数量
        if (currentNum > Maximum)
            currentNum = Maximum;
    }
}

//包裹
public class Container : MonoBehaviour {
    //物品列表
    public List<ContainerItem> items;

    void Awake()
    {
        items = new List<ContainerItem>();

        //for test
        items.Add(new ContainerItem
        {
            Id = 101,
            Name = "Ammo_Rifle",
            Maximum = 180,
            currentNum = 120
        });
        items.Add(new ContainerItem
        {
            Id = 102,
            Name = "Ammo_Handgun",
            Maximum = 120,
            currentNum = 60
        });
    }

    /*
     * 向items中添加物品
     */
    public int Add(ContainerItem item)
    {
        var containerItem = items.Where(x => x.Id == item.Id).FirstOrDefault();
       if (containerItem !=null)
       {
           Put(item.Id, item.currentNum);
           return 2;
       }
       items.Add(new ContainerItem
       {
            Id = item.Id,
            Name = item.Name,
            Maximum =item.Maximum,
            currentNum = item.currentNum
       });
       return item.Id;
    }

    public void Put(int itemID, int amount)
    {
        var containerItem = items.Where(x => x.Id == itemID).FirstOrDefault();
        if (containerItem == null)
            return;
        containerItem.Set(amount);
    }

    //从容器中拿出拿出value数量物品id
    public int TakeFromContainer(int itemId, int amount)
    {

        var containerItem = GetContainerItem(itemId);
        if (containerItem == null)
            return -1;
        return containerItem.Get(amount);

    }

    public int GetAmountRemaining(int itemId)
    {
        var containerItem = GetContainerItem(itemId);
        if (containerItem == null)
            return -1;
        return containerItem.Remaining;
    }
    //从items中寻找第一个为id的ContainerItem对象
    private ContainerItem GetContainerItem(int  itemId)
    {
        //=>是 .net3.5语法，lambda表达式
        //等于什么呢 
        //foreach(ContainerItem x in items)
        //where （x.Id == id）
        //return x；
        var containerItem = items.Where(x => x.Id == itemId).FirstOrDefault();
        if (containerItem == null)
            return null;
        return containerItem;
    }
}
