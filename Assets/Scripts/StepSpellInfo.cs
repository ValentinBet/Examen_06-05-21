using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepSpellInfo : MonoBehaviour
{
    public Character target;
    public Spell spellInfo;
    public TeamTarget originTeam = TeamTarget.Allied;




    public int indexPos = 0;
    public Image img;
    public MeasureDivision parentMeasure;
    public bool setStep = false;
    public void Init(int index, MeasureDivision parent)
    {
        indexPos = index;
        parentMeasure = parent;
    }

    public void InitSpell(Spell spell)
    {
        spellInfo = spell;
    }

    public void Hover()
    {
        if (TurnManager.Instance.chosenSpell != null && TurnManager.Instance.charTurn == parentMeasure.autorizedChar && CharacterManager.Instance.targeting == false)
        {
            parentMeasure.PreviewSpellPos(TurnManager.Instance.chosenSpell, indexPos);
        }
    }

    public void Highlight()
    {
        img.color = TurnManager.Instance.charTurn.charInfo.mainColor;
    }

    public void ResetHighlight()
    {
        img.color = Color.white;
    }

    public void OnClick()
    {
        if (TurnManager.Instance.chosenSpell != null && TurnManager.Instance.charTurn == parentMeasure.autorizedChar && CharacterManager.Instance.targeting == false)
        {
            parentMeasure.SetSpellPos(TurnManager.Instance.chosenSpell, indexPos);
            TurnManager.Instance.NextChar();
        }
    }
    public void ExitHover()
    {
        if (TurnManager.Instance.chosenSpell != null && TurnManager.Instance.charTurn == parentMeasure.autorizedChar && CharacterManager.Instance.targeting == false)
        {
            parentMeasure.ResetPreview();
        }

    }
    public void ResetStep()
    {
        spellInfo = null;
        target = null;
        originTeam = TeamTarget.Allied;
        setStep = false;
        ResetHighlight();
    }

    internal void SetStep(Spell chosenSpell)
    {
        spellInfo = chosenSpell;
        setStep = true;
        img.color = TurnManager.Instance.charTurn.charInfo.mainColor;
        target = TurnManager.Instance.target;

        if (TurnManager.Instance.isAllyTurn)
        {
            originTeam = TeamTarget.Allied;
        } else
        {
            originTeam = TeamTarget.Enemy;
        }
    }
}


