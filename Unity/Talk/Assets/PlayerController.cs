using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, Controls.IPlayerActions
{
    public Controls inputs;
    [SerializeField] private JsonManager jsonManager;
    Vector2 dir;
    Vector2 rotate;

    float sensitive;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        jsonManager = GetComponent<JsonManager>();
        sensitive = 0.3f;
        inputs = new Controls();
        inputs.Player.SetCallbacks(this);
    }

    void OnEnable()
    {
        dir = Vector2.zero;
        rotate = Vector2.zero;

        inputs.Player.Enable();
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        inputs.Player.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.forward * dir.y + Vector3.right * dir.x) * Time.deltaTime * 3f);
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        dir = context.ReadValue<Vector2>();
    }

    public void OnTalk(InputAction.CallbackContext context)
    {
        if(context.started)
        {

        }
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        Vector2 pos = context.ReadValue<Vector2>();
        transform.Rotate((pos.x - rotate.x) * Vector3.up * sensitive);
        //Debug.Log(transform.GetChild(0).transform.rotation.x);
        if (transform.GetChild(0).transform.rotation.x + (pos.y - rotate.y) < 90f && transform.GetChild(0).transform.rotation.x + (pos.y - rotate.y) > -80f)
            transform.GetChild(0).Rotate((pos.y - rotate.y) * Vector3.left * sensitive);
        rotate = pos;
    }
}
