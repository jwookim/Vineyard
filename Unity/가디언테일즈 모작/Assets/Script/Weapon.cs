using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WEAPON
{
    WEAPON_SWORD,
    WEAPON_AXE,
    WEAPON_STAFF,
    WEAPON_BOW
}

public abstract class Weapon : MonoBehaviour
{
    public Sprite sprite { get; protected set; }
    public Character user { get; protected set; }
    public WEAPON type { get; protected set; }

    protected float Atk;
    protected float Def;
    protected int Cooldown;
    protected int SkillChargeBoost;


    public abstract void WeaponSkill();
}
