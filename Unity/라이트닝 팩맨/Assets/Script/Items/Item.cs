using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : SpriteObj
{
    const float size = 0.5f;

    protected int score;
    protected bool activate;


    protected AudioSource audioSource;

    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (activate)
            CollisionCheck();
    }

    private void OnEnable()
    {
        activate = true;
    }
    private void CollisionCheck()
    {
        if (Physics.CheckBox(transform.position, transform.lossyScale / 2f * size, Quaternion.identity, LayerMask.GetMask("Player")))
        {
            intoPocket();
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        audioSource.Stop();
    }


    protected virtual void intoPocket()
    {
        activate = false;
        GameManager.Instance.increaseScore(score);
        StartCoroutine(DirectingGetItem());
    }

    protected abstract IEnumerator DirectingGetItem();
}
