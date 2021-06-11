using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    const float spreadRange = 1.5f;
    const float spreadTime = 0.8f;
    const float collapseTime = 3f;

    ParticleSystem Effect;
    GameObject Light;
    [SerializeField] private bool onOff;


    [SerializeField] bool collabsible;
    public bool OnOff { get { return onOff; }}

    [SerializeField] float timeLimit;

    float currentTime;

    Coroutine SpreadCoroutine;

    private void Awake()
    {
        Effect = GetComponent<ParticleSystem>();
        Light = transform.GetChild(0).gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpreadCoroutine = null;
        currentTime = 0f;
        if (onOff)
            Ignite();
        else
            Extinguish();
    }

    // Update is called once per frame
    void Update()
    {
        if (onOff)
            Burn();
    }


    public void Ignite() //점화
    {
        onOff = true;
        Light.SetActive(true);
        currentTime = timeLimit;
        if (!Effect.isPlaying)
            Effect.Play();

        if (SpreadCoroutine == null)
            SpreadCoroutine = StartCoroutine(Spread());

        if(collabsible)
            transform.parent.GetComponent<Objects>().Invoke("Destroy", collapseTime);
    }

    void Extinguish() // 소화
    {
        onOff = false;
        Light.SetActive(false);
        if (!Effect.isStopped)
            Effect.Stop();
    }

    void Burn()
    {
        if (timeLimit > 0f)
        {
            currentTime -= Time.deltaTime * GameManager.Instance.TimeScale;
            if (currentTime <= 0f)
                Extinguish();
        }
    }

    IEnumerator Spread() // 번짐
    {
        yield return new WaitForSeconds(spreadTime);
        while (onOff)
        {
            foreach (var tmp in Physics.OverlapSphere(transform.position, spreadRange, LayerMask.GetMask("Fire")))
            {
                if (tmp == GetComponent<Collider>())
                    continue;

                tmp.GetComponent<Fire>().Ignite();
            }

            yield return null;
        }

        SpreadCoroutine = null;
    }
}
