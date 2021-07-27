using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objects : MonoBehaviour
{
    protected bool Interactable;

    protected AudioSource audioSource;

    [SerializeField] protected AudioClip Audio_Destroy;

    protected virtual void Awake()
    {
        tag = "Object";
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {
        StopAllCoroutines();
        CancelInvoke();

    }

    /*private void OnCollisionEnter(Collision collision)
    {
        Collision();
    }*/

    public virtual bool Collision()
    {
        return false;
    }

    public virtual void Contact(Character ch)
    {

    }

    public virtual void Interaction()
    {

    }

    public void Destroy()
    {
        GameObject effect = ObjectPoolManger.Instance.GenerateDestroyEffect();
        effect.transform.position = transform.position;
        effect.GetComponent<DestroyEffect>().Activate(Audio_Destroy);

        gameObject.SetActive(false);
    }

    protected float GetHalfSize()
    {
        return transform.lossyScale.x * 0.5f;
    }
}
