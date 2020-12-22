using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Objects
{
    protected float Direct;
    protected float Velocity;
    protected float Acceleration;
    protected float curVelocity;
    protected float Jump;

    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Direct = 0f;
        Velocity = 10f;
        Acceleration = 5f;
        curVelocity = 0;
        Jump = 300f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Accelerate();
        //Move();
    }

    protected void Accelerate()
    {
        /*if(Direct == 0f)
        {
            Braking();
            return;
        }*/



        if (Mathf.Abs(rigid.velocity.x) < Velocity)
            rigid.velocity += new Vector2(Direct * Acceleration * Time.deltaTime, 0);

    }

    protected void Braking()
    {
        curVelocity -= curVelocity * 0.8f * Time.deltaTime;
    }
    protected void Move()
    {
        transform.Translate(new Vector2(Time.deltaTime * curVelocity, 0));
    }


    

}
