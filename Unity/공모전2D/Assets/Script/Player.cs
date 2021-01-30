using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character, InputControl.IPlayerActions
{

    public InputControl inputActions;


    public Inventory inventory;

    const float RopeLen = 30f;

    protected override void Awake()
    {
        base.Awake();
        inventory = new Inventory();
        inputActions = new InputControl();
        inputActions.Player.SetCallbacks(this);
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }


    protected override void OnEnable()
    {
        base.OnEnable();
        inputActions.Player.Enable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        inputActions.Player.Disable();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Rotation(float angle)
    {
        base.Rotation(angle);

        transform.rotation = Quaternion.identity;
    }

    private void RopeConnect()
    {
        Collider2D Port = Physics2D.OverlapCircle(transform.position, RopeLen, LayerMask.GetMask("Port"));
        if (Port != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Port.transform.position - transform.position, Vector2.Distance(Port.transform.position, transform.position), LayerMask.GetMask("Obstacle"));


            if (hit.collider != null)
            {
                Debug.Log(hit);
                return;
            }

            gravityController = Port.gameObject;

            gravityController.GetComponent<Port>().Connect(gameObject);

            var joint = GetComponent<DistanceJoint2D>();
            joint.enabled = true;
            joint.connectedBody = gravityController.GetComponent<Rigidbody2D>();
            joint.distance = Vector2.Distance(gravityController.transform.position, transform.position);
        }
                
    }

    private void RopeDisconnect()
    {
        if (gravityController != null)
        {
            gravityController.GetComponent<Port>().Disconnect();
            gravityController = null;
            GetComponent<DistanceJoint2D>().enabled = false;
        }
    }

    private void StatusCulc()
    {
        totalAccel = Acceleration + inventory.GetAccel();
        totalSpeed = Speed + inventory.GetSpeed();
        totalJump = Jump + inventory.GetJump();
    }

    protected void GetItem(Item item)
    {
        inventory.AddItem(item);
        StatusCulc();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            GetItem(collision.gameObject.GetComponent<ItemCapsule>().TakeItem());
            Debug.Log(collision);
        }
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
            rigid.velocity = new Vector2(rigid.velocity.x, totalJump);
        }
    }


    public void OnRope(InputAction.CallbackContext context)
    {
        if (context.started)
            RopeConnect();

        else if (context.canceled)
            RopeDisconnect();
    }
}
