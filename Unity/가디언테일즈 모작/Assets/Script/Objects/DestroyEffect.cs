using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    AudioSource source;
    // Start is called before the first frame update
    private void Awake()
    {
        source = GetComponent<AudioSource>();
        
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        source.Stop();
        source.clip = null;
    }


    public void Activate(AudioClip clip)
    {
        gameObject.SetActive(true);
        StartCoroutine(PlayBack(clip));
    }


    IEnumerator PlayBack(AudioClip clip)
    {
        source.clip = clip;
        source.Play();

        yield return new WaitForSeconds(clip.length);

        ObjectPoolManger.Instance.StorageDestroyEffect(gameObject);
        gameObject.SetActive(false);
    }
}
