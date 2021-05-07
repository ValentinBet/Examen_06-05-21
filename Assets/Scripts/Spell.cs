using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameData;

[CreateAssetMenu(fileName = "NewSpell", menuName = "Custom/NewSpell")]
public class Spell : ScriptableObject
{
    public string spellName = "spellNul";
    public int time = 1;

    public TargetType targetType = TargetType.One;
    public TeamTarget teamTarget = TeamTarget.Enemy;
    public SpellType spellType = SpellType.Attack;


    public int damagesPerTime = 1;
    public int manaCost = 1;
    public CharacterState state = CharacterState.Clear;


    public int armorGiven = 0;
    public int healGiven = 0;
}


public enum TargetType
{
    All,
    One
}
public enum TeamTarget
{
    Allied,
    Enemy,
    Both
}
public enum SpellType
{
    Attack,
    Armor,
    Heal
}

