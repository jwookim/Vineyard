using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Sword2H : Weapon
{
    Sword2H()
    {
        user = null;
        type = WEAPON.WEAPON_SWORD;
        sprite = Resources.Load("Sprites/Equipment/MeleeWeapon1H/Basic/Sword/Sword2H.png") as Sprite;
    }
}
