using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem; // »ç¿ë

public class PlayerController : MonoBehaviour, InputControl.IPlayerControlActions
{
    [SerializeField] private NavMeshAgent meshAgent;

    InputControl newInput;

    private void Awake()
    {
        newInput = new InputControl();
        newInput.PlayerControl.SetCallbacks(this);

        meshAgent = GetComponent<NavMeshAgent>();
    }
    void OnEnable()
    {
        newInput.PlayerControl.Enable();
    }
    void OnDisable()
    {
        newInput.PlayerControl.Disable();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 clickPoint = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(clickPoint);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log(hit.point);

            meshAgent.SetDestination(hit.point);
        }
    }
}

