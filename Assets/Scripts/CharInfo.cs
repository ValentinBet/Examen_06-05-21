using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharInfo", menuName = "Custom/Char Info")]
public class CharInfo : ScriptableObject
{
    public string charName = "";
    public Color mainColor = Color.white;
    public int lifeMax = 100;
    public int manaMax = 1;
    public int baseInitiative = 3;
    public int baseArmor= 0;


    public List<Spell> spellList = new List<Spell>();
}
