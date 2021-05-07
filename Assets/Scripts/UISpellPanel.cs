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
        manaCostText.text = spell.manaCost + " mana"; 
        measureTimeText.text = "Sur " +  spell.time + " temps" ;
        stateDmgText.text = "Etat appliqués = " + (spell.state).ToString() + " <color=red> Dégats = " + spell.damagesPerTime + "</color>";

    }

    public void ChooseThisSpell()
    {
        UIManager.Instance.CloseSpellList();
        CharacterManager.Instance.SetEnemyTargetState(false);

        if (spell.targetType == TargetType.One)
        {
            CharacterManager.Instance.SetEnemyTargetState(true);
        }

        TurnManager.Instance.chosenSpell = spell;


    }
}
