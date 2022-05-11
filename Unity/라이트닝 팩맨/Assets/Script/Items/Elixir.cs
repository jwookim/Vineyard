using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elixir : Item
{
    const int ElixirScore = 50;
    protected override void Start()
    {
        base.Start();
        score = ElixirScore;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        transform.GetChild(0).gameObject.SetActive(true);
    }

    protected override IEnumerator DirectingGetItem()
    {
        audioSource.Play();
        activate = false;
        transform.GetChild(0).gameObject.SetActive(false);

        yield return GameManager.Instance.GetElixir();


        while(audioSource.isPlaying)
            yield return null;

        gameObject.SetActive(false);
    }
}
