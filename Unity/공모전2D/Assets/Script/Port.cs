using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour, InputControl.IPortActions
{
    ObjectManager objectManager;
    public GameObject target;
    public Vector2? FixedPos;
    InputControl inputActions;

    void Awake()
    {
        objectManager = GetComponentInParent<ObjectManager>();
        FixedPos = null;
        inputActions = new InputControl();
        inputActions.Port.SetCallbacks(this);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    void OnEnable()
    {
        inputActions.Port.Enable();
    }

    void OnDisable()
    {
        inputActions.Port.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        if (FixedPos != null)
            Rotate();
    }

    public void Connect(GameObject tg)
    {
        target = tg;
    }

    public void Disconnect()
    {
        target = null;
        FixedPos = null;
    }

    void Rotate()
    {
        if (target.transform.position != FixedPos)
        {
            objectManager.Rotate(Vector2.SignedAngle(transform.position, (Vector2)FixedPos) - Vector2.SignedAngle(transform.position, target.transform.position), transform.position);
        }
    }

    public void OnFix(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if (target != null)
                FixedPos = target.transform.position;
        }

        if (context.canceled)
            FixedPos = null;
    }
}
