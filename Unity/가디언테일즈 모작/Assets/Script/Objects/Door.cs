using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    const float Speed = 4f;

    Transform door;

    Collider doorCollider;

    [SerializeField]List<ISwitch> switches = new List<ISwitch>();

    bool trigger;

    private void Awake()
    {
        door = transform.GetChild(0);
        doorCollider = GetComponent<Collider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        trigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchCheck();
    }

    private void OnTriggerStay(Collider other)
    {
        trigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        trigger = false;
    }

    void SwitchCheck()
    {
        bool check = true;
        foreach(var sw in switches)
        {
            if(!sw.push)
            {
                check = false;
                break;
            }
        }

        if (check)
            DoorOpen();
        else
            DoorClose();
    }

    void DoorOpen()
    {
        if(!doorCollider.isTrigger)
        {
            doorCollider.isTrigger = true;
            StopAllCoroutines();
            StartCoroutine(OpenDoor());
        }
    }

    void DoorClose()
    {
        if (doorCollider.isTrigger && !trigger)
        {
            doorCollider.isTrigger = false;
            StopAllCoroutines();
            StartCoroutine(CloseDoor());
        }
    }



    IEnumerator OpenDoor()
    {
        while(door.position.y >= transform.position.y - door.localScale.y)
        {
            door.position -= new Vector3(0f, Speed * Time.deltaTime, 0f);
            yield return null;
        }
    }

    IEnumerator CloseDoor()
    {
        while (door.position.y < transform.position.y)
        {
            door.position += new Vector3(0f, Speed * Time.deltaTime, 0f);
            yield return null;
        }

        door.position = transform.position;
    }
}
