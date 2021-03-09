using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwap : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] RightHand;
    [SerializeField] SpriteRenderer[] LeftHand;
    [SerializeField] SpriteRenderer[] Shield;

    // Start is called before the first frame update
    void Awake()
    {
        RightHand = new SpriteRenderer[4];
        LeftHand = new SpriteRenderer[4];
        Shield = new SpriteRenderer[4];

        for (int i=0; i<4; i++)
        {
            RightHand[i] = transform.GetChild(i).Find("UpperBody").Find("ArmR").Find("HandR").Find("MeleeWeapon").GetComponent<SpriteRenderer>();
            LeftHand[i] = transform.GetChild(i).Find("UpperBody").Find("ArmL").Find("HandL").Find("MeleeWeapon").GetComponent<SpriteRenderer>();
            Shield[i] = transform.GetChild(i).Find("UpperBody").Find("ArmL").Find("HandL").Find("Shield").GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void EquipWeapon(Sprite weapon)
    {
        foreach (var wp in RightHand)
            wp.sprite = weapon;
    }

    public void EquipBow(Sprite weapon)
    {
        foreach (var wp in LeftHand)
            wp.sprite = weapon;
    }

    public void EquipShield(Sprite shield)
    {
        foreach (var sh in Shield)
            sh.sprite = shield;
    }


    public void ReleaseWeapon()
    {
        foreach (var wp in RightHand)
            wp.sprite = null;

        foreach (var wp in LeftHand)
            wp.sprite = null;
    }

    public void ReleaseShield()
    {
        foreach (var sh in Shield)
            sh.sprite = null;
    }
}
