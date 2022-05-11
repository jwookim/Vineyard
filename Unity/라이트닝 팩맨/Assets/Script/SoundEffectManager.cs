using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    [SerializeField] AudioClip Ready;
    [SerializeField] AudioClip Start;
    [SerializeField] AudioClip Clear;
    [SerializeField] AudioClip Fever;
    [SerializeField] AudioClip Camera;
    [SerializeField] AudioClip Curtain;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void ReadySign()
    {
        audioSource.clip = Ready;
        audioSource.Play();
    }
    public void StartSign()
    {
        audioSource.clip = Start;
        audioSource.Play();
    }

    public void ClearSign()
    {
        audioSource.clip = Clear;
        audioSource.Play();
    }
    public void FeverMusic()
    {
        audioSource.clip = Fever;
        audioSource.Play();
    }
    public void SwapCamera()
    {
        audioSource.clip = Camera;
        audioSource.Play();
    }
    public void DrawCurtain()
    {
        audioSource.clip = Curtain;
        audioSource.Play();
    }
}
