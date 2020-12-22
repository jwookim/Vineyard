using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character, InputControl.IPlayerActions
{

    public InputControl inputActions;

    protected override void Awake()
    {
        base.Awake();
        inputActions = new InputControl();
        inputActions.Player.SetCallbacks(this);
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }


    void OnEnable()
    {
        inputActions.Player.Enable();
    }

    void OnDisable()
    {

        inputActions.Player.Disable();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    public void OnControl(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
/*
        Vector2 value = context.ReadValue<Vector2>();

        Move(value);*/
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Direct = context.ReadValue<float>();
    }

    public void OnSight(InputAction.CallbackContext context)
    {
        //if cont

        float value = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            //rigid.AddForce(Vector2.up * Jump);
            Rotation(90f);
        }
    }


    public override void Rotation(float angle)
    {
        base.Rotation(angle);

        transform.rotation = Quaternion.identity;
    }
}
