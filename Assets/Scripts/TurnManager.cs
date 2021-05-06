using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private static TurnManager _instance;
    public static TurnManager Instance { get { return _instance; } }


    public List<MeasureDivision> measures = new List<MeasureDivision>();

    public float timeStep = 0.4f;
    public int step = 0;

    public Spell chosenSpell;
    public PlayableChar charTurn;
    public Character target;
    public int charStep = 0;

    public bool isAllyTurn = true;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

    }



    public void PlayItem()
    {

    }

    public void Flee()
    {

    }


    public void NextChar()
    {
        chosenSpell = null;

        charStep++;
        if (charStep >= CharacterManager.Instance.orderedAllied.Count)
        {
            EndTurn();
            return;
        }
        charTurn = CharacterManager.Instance.orderedAllied[charStep];
    }

    public void EndTurn()
    {
        charStep = 0;
        step = 0;
        UIManager.Instance.StartMovePin(timeStep * measures[0].measureLenght);
        StartCoroutine(ProgressOnMeasure());
    }

    IEnumerator ProgressOnMeasure()
    {
        ResolveStep();
        yield return new WaitForSeconds(timeStep);
        step++;

        if (step >= measures[0].measureLenght)
        {
            NewTurn();
        }
        else
        {
            StartCoroutine(ProgressOnMeasure());
        }


    }

    public void ResolveStep()
    {
        foreach (MeasureDivision md in measures)
        {
            StepSpellInfo _temp = md.stepSpellInfos[step];

            if (_temp.spellInfo == null)
            {
                continue;
            }

            if (_temp.spellInfo.targetType == TargetType.One)
            {
                _temp.target.TakeDamages(_temp.spellInfo.damagesPerTime);
                _temp.target.GainArmor(_temp.spellInfo.armorGiven);
            }
            else
            {
                TeamTarget _tempTarget = TeamTarget.Allied;

                if (_temp.originTeam == TeamTarget.Allied)
                {
                    if (_temp.spellInfo.teamTarget == TeamTarget.Enemy)
                    {
                        _tempTarget = TeamTarget.Enemy;
                    }
                }
                else
                {
                    if (_temp.spellInfo.teamTarget == TeamTarget.Allied)
                    {
                        _tempTarget = TeamTarget.Enemy;
                    }
                }

                if (_tempTarget == TeamTarget.Enemy)
                {
                    foreach (EnemyChar ec in CharacterManager.Instance.enemiesInCombat)
                    {
                        ec.TakeDamages(_temp.spellInfo.damagesPerTime);
                        ec.GainArmor(_temp.spellInfo.armorGiven);
                    }
                }
                else
                {
                    foreach (PlayableChar ec in CharacterManager.Instance.allied)
                    {
                        ec.TakeDamages(_temp.spellInfo.damagesPerTime);
                        ec.GainArmor(_temp.spellInfo.armorGiven);
                    }
                }
            }



        }
    }

    public void NewTurn()
    {
        UIManager.Instance.StopAndResetPin();

        foreach (MeasureDivision md in measures)
        {
            md.ResetStep();
        }

        CharacterManager.Instance.OrderAllCharByInitiative();


        charTurn = CharacterManager.Instance.orderedAllied[charStep];
    }

}
