using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameData;

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

        UIManager.Instance.spellListContainer.SetActive(false);
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

        foreach (PlayableChar pc in CharacterManager.Instance.allied)
        {
            pc.ResetState();
        }
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
                if (_temp.target == null)
                {
                    print("null");

                    _temp.target = CharacterManager.Instance.orderedAllied[UnityEngine.Random.Range(0, CharacterManager.Instance.orderedAllied.Count)];
                }

                if (_temp.target.HasState(CharacterState.Boosted))
                {
                    _temp.target.TakeDamages(_temp.spellInfo.damagesPerTime * 2);
                }
                else
                {
                    _temp.target.TakeDamages(_temp.spellInfo.damagesPerTime);
                }


                if (_temp.asGivenOneShotEffect == false)
                {
                    _temp.target.GainArmor(_temp.spellInfo.armorGiven);
                    _temp.target.Heal(_temp.spellInfo.healGiven);
                    _temp.target.AddState(_temp.spellInfo.state);
                    md.GiveOneShotSpellEffect(step, _temp.spellInfo.time);
                }

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
                        if (ec.HasState(CharacterState.Boosted))
                        {

                            ec.TakeDamages(_temp.spellInfo.damagesPerTime * 2);
                        }
                        else
                        {
                            ec.TakeDamages(_temp.spellInfo.damagesPerTime);
                        }
                        if (_temp.asGivenOneShotEffect == false)
                        {
                            ec.GainArmor(_temp.spellInfo.armorGiven);
                            ec.Heal(_temp.spellInfo.healGiven);
                            ec.AddState(_temp.spellInfo.state);
                        }

                    }
                }
                else
                {
                    foreach (PlayableChar ec in CharacterManager.Instance.allied)
                    {
                        if (ec.HasState(CharacterState.Boosted))
                        {
                     
                            ec.TakeDamages(_temp.spellInfo.damagesPerTime * 2);
                        } else
                        {
                            ec.TakeDamages(_temp.spellInfo.damagesPerTime);
                        }


                        if (_temp.asGivenOneShotEffect == false)
                        {
                            ec.GainArmor(_temp.spellInfo.armorGiven);
                            ec.Heal(_temp.spellInfo.healGiven);
                            ec.AddState(_temp.spellInfo.state);
                        }

                    }
                }
                if (_temp.asGivenOneShotEffect == false)
                {
                    md.GiveOneShotSpellEffect(step, _temp.spellInfo.time);
                }

            }



        }
    }

    public void NewTurn()
    {

        foreach (PlayableChar pc in CharacterManager.Instance.allied)
        {
            pc.ResetArmor();
            UIManager.Instance.GetPlbCharPanel(pc).UpdateCharInfo(pc);
        }
        foreach (EnemyChar ec in CharacterManager.Instance.enemiesInCombat)
        {
            ec.ResetArmor();
            UIManager.Instance.GetEnemyCharPanel(ec).UpdateCharInfo(ec);
        }

        foreach (MeasureDivision md in measures)
        {
            md.ResetStep();
        }

        UIManager.Instance.StopAndResetPin();

        CharacterManager.Instance.OrderAllCharByInitiative();

        isAllyTurn = false;
        CharacterManager.Instance.MakeEnemyTurn();
        isAllyTurn = true;

        charTurn = CharacterManager.Instance.orderedAllied[charStep];
    }

}
