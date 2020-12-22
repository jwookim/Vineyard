using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour, InputControl.IObjectsActions
{
    public List<Objects> objectList;

    InputControl inputActions;

    float gravityScale;

    void Awake()
    {
        objectList = new List<Objects>();

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
        
    }

    public void OnRotate(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(context.started)
        {
            transform.RotateAround(Vector2.zero, Vector3.forward, context.ReadValue<float>() * 30f);

            foreach(var ob in objectList)
            {
                ob.Rotation(/*context.ReadValue<float>() * */30f);
            }
        }
    }

    public void OnGravity(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

    }
}
