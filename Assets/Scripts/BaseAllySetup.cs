using UnityEngine;
using System.Collections;

[System.Serializable]

//Class that is used to set up each hero for battle
public class BaseAllySetup
{
    //The characters name
    public string name;

    //The characters stats, base and current because they could change mid battle
    public float baseHealth;
    public float currhealth;
    
    public float baseDamage;
    public float currDamage;

    public float baseSpeed;
    public float currSpeed;

    public bool canSpecial;

}
