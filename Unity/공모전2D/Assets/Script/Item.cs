using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public Sprite sprite;
    public float Accel { get; protected set; }
    public float Speed { get; protected set; }
    public float Jump { get; protected set; }


}


public class Shoes:Item
{
    public Shoes()
    {
        Accel = 10f;
        Speed = 10f;
        Jump = 10f;
    }
}