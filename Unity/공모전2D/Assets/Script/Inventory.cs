using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    List<Item> items;

    public Inventory()
    {
        items = new List<Item>();
    }



    public void AddItem(Item item)
    {
        items.Add(item);
    }


    public float GetSpeed()
    {
        float sum = 0f;
        foreach(var item in items)
        {
            sum += item.Speed;
        }
        return sum;
    }
    public float GetAccel()
    {
        float sum = 0f;
        foreach (var item in items)
        {
            sum += item.Accel;
        }
        return sum;
    }
    public float GetJump()
    {
        float sum = 0f;
        foreach (var item in items)
        {
            sum += item.Jump;
        }
        return sum;
    }
}
