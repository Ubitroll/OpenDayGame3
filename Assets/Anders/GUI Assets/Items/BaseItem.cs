﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class BaseItem : ScriptableObject
{
    public string Name;
    public string Description;
    public string ID;

    public Sprite ItemIcon;

    public int Damage;
    public int ArmourPierce;
    public int ShieldPierce;
    public int AttackSpeed;
    public int LifeSteal;
    public int CriticalDamage;
    public int CriticalChance;
    public int MagicDamage;
    public int Shield;
    public int Armour;
    public int MovementSpeed;
    public int Health;
    public float ManaRegen;
    public int ExtraMana;
    public int HealthRegen;
    public float AbilityCooldown;
    public int Cost;
}
