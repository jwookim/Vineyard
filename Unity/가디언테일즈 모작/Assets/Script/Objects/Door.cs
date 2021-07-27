using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DOORTYPE
{
    During,
    Timer,
    Permanent
}

public class Door : MonoBehaviour
{
    [SerializeField] AudioClip Audio_Drag;
    AudioSource audioSource;

    const float Speed = 4f;

    Transform door;

    Collider doorCollider;

    SwitchHub switchHub;

    [SerializeField] DOORTYPE doorType;

    [SerializeField] float openTime;

    float currentTime;

    Coroutine DoorCoroutine;
    Coroutine TimerCoroutine;

    bool collision;

    bool isOpen;

    private void Awake()
    {
        door = transform.GetChild(0);
        doorCollider = GetComponent<Collider>();
        switchHub = GetComponent<SwitchHub>();
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        collision = false;
        currentTime = 0f;
        DoorCoroutine = null;
        TimerCoroutine = null;
        audioSource.clip = Audio_Drag;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchCheck();
    }

    private void OnTriggerStay(Collider other)
    {
        collision = true;
    }

    private void OnTriggerExit(Collider other)
    {
        collision = false;
    }

    void SwitchCheck()
    {
        if (switchHub.Check())
            DoorOpen();
        else
            TypeCheck();
    }

    void DoorOpen()
    {
        currentTime = openTime;
        if(!isOpen)
        {
            if (doorType == DOORTYPE.Timer && TimerCoroutine == null)
                TimerCoroutine = StartCoroutine(CloseTimer());
            isOpen = true;
            if (DoorCoroutine != null)
            {
                StopCoroutine(DoorCoroutine);
                DoorCoroutine = null;
                audioSource.Stop();
            }
            DoorCoroutine = StartCoroutine(OpenDoor());
        }
    }

    void DoorClose()
    {
        if (!collision)
        {
            isOpen = false;
            if (DoorCoroutine != null)
            {
                StopCoroutine(DoorCoroutine);
                DoorCoroutine = null;
                audioSource.Stop();
            }
            DoorCoroutine = StartCoroutine(CloseDoor());
        }
    }

    void TypeCheck()
    {
        if (isOpen)
        {
            if (doorType == DOORTYPE.During)
                DoorClose();
        }
    }

    IEnumerator OpenDoor()
    {
        audioSource.Play();
        while(door.position.y >= transform.position.y - door.localScale.y)
        {
            door.position -= new Vector3(0f, Speed * Time.deltaTime * GameManager.Instance.TimeScale, 0f);
            yield return null;
        }

        doorCollider.isTrigger = true;

        if (doorType == DOORTYPE.Permanent)
        {
            switchHub.enabled = false;
            this.enabled = false;
        }
    }

    IEnumerator CloseDoor()
    {
        audioSource.Play();
        while (door.position.y < transform.position.y)
        {
            door.position += new Vector3(0f, Speed * Time.deltaTime * GameManager.Instance.TimeScale, 0f);
            yield return null;
        }

        door.position = transform.position;
        doorCollider.isTrigger = false;
    }

    IEnumerator CloseTimer()
    {
        while(currentTime > 0f)
        {
            currentTime -= Time.deltaTime * GameManager.Instance.TimeScale;
            Debug.Log(currentTime);
            yield return null;
        }

        while (isOpen)
        {
            DoorClose();
            yield return null;
        }
        TimerCoroutine = null;
    }
}
