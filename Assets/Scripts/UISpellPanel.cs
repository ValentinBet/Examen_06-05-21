using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISpellPanel : MonoBehaviour
{
    public TextMeshProUGUI spellNameText;
    public TextMeshProUGUI manaCostText;
    public TextMeshProUGUI measureTimeText;
    public TextMeshProUGUI stateDmgText;

    Spell spell;
    public void Init(Spell pickSpell)
    {
        spell = pickSpell;
        spellNameText.text = spell.spellName;
        manaCostText.text = spell.manaCost + ""; 
        measureTimeText.text = spell.time + "";
        stateDmgText.text = (spell.state).ToString() + " - <color=red> Dmg : " + spell.damagesPerTime + "</color>";

    }

    public void ChooseThisSpell()
    {
        UIManager.Instance.CloseSpellList();

        if (spell.targetType == TargetType.One)
        {
            CharacterManager.Instance.SetEnemyTargetState(true);
        }

        TurnManager.Instance.chosenSpell = spell;


    }
}
