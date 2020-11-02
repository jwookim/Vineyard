using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, InputController.IPlayerInputActions
{
    public InputController inputController;
    public float speed = 5f;
    Vector2 direct;
    [SerializeField] private Animator animator;

    float AttackDelay;
    private void Awake()
    {
        inputController = new InputController();
        inputController.PlayerInput.SetCallbacks(this);
        animator = GetComponent<Animator>();
        AttackDelay = 2f;
    }

    void OnEnable()
    {
        inputController.PlayerInput.Enable();
    }
    void OnDisable()
    {
        inputController.PlayerInput.Disable();
    }

    private void FixedUpdate()
    {

        animator.SetFloat("X", direct.x);
        //Y에 vertical
        animator.SetFloat("Y", direct.y);


        //Vector 크기를 float 
        animator.SetFloat("Speed", direct.magnitude);



        //                  (0f, 0f, 1f)    * -1 ~ 1   *    0.02f            *  5f
        transform.Translate(Vector3.forward * direct.y * Time.fixedDeltaTime * speed);


        transform.Rotate(Vector3.up * direct.x, Space.World);

        AttackDelay += Time.deltaTime;
    }

    public void Attack()
    {
        if (AttackDelay >= 2f)
        {
            AttackDelay = 0f;

            animator.SetTrigger("Attack");
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Attack();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        switch(context.phase)
        {
            case InputActionPhase.Started:
                direct = context.ReadValue<Vector2>();
                break;
            case InputActionPhase.Performed:
                direct = context.ReadValue<Vector2>();
                break;
            case InputActionPhase.Canceled:
                direct = Vector2.zero;
                break;
        }



        


        /*float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");


        animator.SetFloat("X", horizontal);
        //Y에 vertical
        animator.SetFloat("Y", vertical);

        //Vector값 : 방향있는 크기값
        Vector2 direction = new Vector2(horizontal, vertical);

        //Vector 크기를 float 
        animator.SetFloat("Speed", direction.magnitude);



        //                  (0f, 0f, 1f)    * -1 ~ 1   *    0.02f            *  5f
        transform.Translate(Vector3.forward * vertical * Time.fixedDeltaTime * speed);


        transform.Rotate(Vector3.up * horizontal, Space.World);*/
    }
}