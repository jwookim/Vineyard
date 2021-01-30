using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Objects
{
    const float AccelerationCycle = 0.2f;

    public GameObject gravityController;

    protected float Direct;
    protected float Speed;
    protected float Acceleration;
    protected float Jump;
    [SerializeField]
    protected float totalSpeed;
    [SerializeField]
    protected float totalAccel;
    protected float totalJump;

    protected override void Awake()
    {
        base.Awake();
        Direct = 0f;
        Speed = 20f;
        Acceleration = 10f;
        Jump = 10f;
        totalSpeed = Speed;
        totalAccel = Acceleration;
        totalJump = Jump;
        gravityController = null;
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnEnable()
    {
        base.OnEnable();


        StartCoroutine(Accelerate());
    }


    IEnumerator Accelerate()
    {
        float speed;
        while (true)
        {
            if (gravityController == null)
                speed = totalSpeed;
            else
                speed = totalSpeed * 2f;

            if (Mathf.Abs(rigid.velocity.x) < speed)
                rigid.velocity += new Vector2(Direct * totalAccel * AccelerationCycle, 0);
            yield return new WaitForSeconds(AccelerationCycle);
        }
    }

    protected virtual void Attack()
    {

    }
}
