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
    public bool asGivenOneShotEffect = false;

    public GameObject armorImg;
    public GameObject healImg;
    public GameObject allAttackImg;
    public GameObject attackImg;

    public void Init(int index, MeasureDivision parent)
    {
        indexPos = index;
        parentMeasure = parent;
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
            if (parentMeasure.SetSpellPos(TurnManager.Instance.chosenSpell, indexPos))
            {
                TurnManager.Instance.NextChar();
            } 

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
        asGivenOneShotEffect = false;
        allAttackImg.SetActive(false);
        attackImg.SetActive(false);
        armorImg.SetActive(false);
        healImg.SetActive(false);
        ResetHighlight();
    }

    internal void SetStep(Spell chosenSpell, EnemyChar? inEnemyTurm = null)
    {
        spellInfo = chosenSpell;
        setStep = true;

        if (inEnemyTurm != null)
        {
            img.color = inEnemyTurm.charInfo.mainColor;
            target = parentMeasure.autorizedChar;
        } else
        {
            img.color = TurnManager.Instance.charTurn.charInfo.mainColor;
            target = TurnManager.Instance.target;
        }

        switch (chosenSpell.spellType)
        {
            case SpellType.Attack:
                switch (chosenSpell.targetType)
                {
                    case TargetType.All:
                        allAttackImg.SetActive(true);
                        break;
                    case TargetType.One:
                        attackImg.SetActive(true);
                        break;
                    default:
                        break;
                }
                break;
            case SpellType.Armor:
                armorImg.SetActive(true);
                break;
            case SpellType.Heal:
                healImg.SetActive(true);
                break;
            default:
                break;
        }


        if (TurnManager.Instance.isAllyTurn)
        {
            originTeam = TeamTarget.Allied;
        } else
        {
            originTeam = TeamTarget.Enemy;
        }
    }



}


