using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Switch : MonoBehaviour
{
    public bool onOff { get; protected set; }

    protected virtual void Start()
    {
        onOff = false;
    }
}
