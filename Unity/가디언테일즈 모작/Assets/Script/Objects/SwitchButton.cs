using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : Switch
{
    AudioSource audioSource;
    [SerializeField] AudioClip Audio_On;
    [SerializeField] AudioClip Audio_Off;
    // Start is called before the first frame update
    protected void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    protected override void Start()
    {
        base.Start();
    }


    private void OnTriggerStay(Collider collision)
    {
        if (!onOff)
        {
            onOff = true;
            audioSource.clip = Audio_On;
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (onOff)
        {
            onOff = false;
            audioSource.clip = Audio_Off;
            audioSource.Play();
        }
    }

}
