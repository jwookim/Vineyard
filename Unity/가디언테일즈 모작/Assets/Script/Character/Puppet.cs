using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puppet : Character
{
    float healTimer;
    // Start is called before the first frame update

    // Update is called once per frame

    protected override void Awake()
    {
    }
    protected override void Start()
    {
        healTimer = 5f;
        MaxHp = 100f;
        curHp = MaxHp;
        HpBar.GetComponent<StateBar>().parent = gameObject;
    }

    protected override void Update()
    {
        if (healTimer > 0f)
            healTimer -= Time.deltaTime * GameManager.Instance.TimeScale;
        else
        {
            if (curHp < MaxHp)
            {
                curHp += 50f * Time.deltaTime * GameManager.Instance.TimeScale;
                HpBar.GetComponent<StateBar>().HpUpdate(curHp / MaxHp);
            }

            if (curHp > MaxHp)
            {
                curHp = MaxHp;
                HpBar.SetActive(false);
            }
        }
    }

    protected override void OnEnable()
    {
        
    }

    protected override void OnDisable()
    {
        HpBar.SetActive(false);
    }

    public override float Damage(float damage, AttackType type)
    {
        healTimer = 5f;
        if (!HpBar.activeSelf)
            HpBar.GetComponent<StateBar>().Activate(curHp / MaxHp);

        return base.Damage(damage, type);
        
    }
}
