using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Fire, ISwitch
{
    public bool push { get; set; }


    //[SerializeField] private float timer;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool StateCheck()
    {
        return push;
    }
}
