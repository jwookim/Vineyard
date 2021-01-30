using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum GravityScale
{
    Min = 0,
    Default = 1,
    Max = 10
}


public class ObjectManager : MonoBehaviour, InputControl.IObjectsActions
{
    public List<Objects> objectList;
    public List<ItemCapsule> dummyItem;

    InputControl inputActions;

    float gravityScale;
    float gravityDir;

    void Awake()
    {
        objectList = new List<Objects>();
        dummyItem = new List<ItemCapsule>();

        inputActions = new InputControl();
        inputActions.Objects.SetCallbacks(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        gravityScale = 1f;
    }

    void OnEnable()
    {
        inputActions.Objects.Enable();
    }

    void OnDisable()
    {
        inputActions.Objects.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        GravityControl();
    }

    public void Rotate(float angle, Vector2 port)
    {
        transform.RotateAround(port, Vector3.forward, angle);
        foreach (var ob in objectList)
        {
            ob.Rotation(angle);
        }

    }

    public void OnRotate(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(context.started)
        {
        }
    }

    public void OnGravity(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        gravityDir = context.ReadValue<float>();
    }

    void GravityControl()
    {
        if (gravityDir != 0f)
        {
            gravityScale += gravityDir * Time.deltaTime / 2f;


            if (gravityScale < (float)GravityScale.Min)
            {
                gravityScale = (float)GravityScale.Min;
            }
            else if (gravityScale > (float)GravityScale.Max)
            {
                gravityScale = (float)GravityScale.Max;
            }

            foreach (var ob in objectList)
            {
                ob.ControlGravity(gravityScale);
            }

        }
    }
    

    public void GenerateItem()
    {
        if (dummyItem.Count <= 0)
        {
            //Instantiate();
        }
    }
}
