using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public List<EnemyChar> enemiesInCombat = new List<EnemyChar>();
    public List<PlayableChar> allied = new List<PlayableChar>();
    public LayerMask enemyLayer;
    public bool targeting = false;
    public List<PlayableChar> orderedAllied = new List<PlayableChar>();
    public List<EnemyChar> orderedEnemies = new List<EnemyChar>();

    private static CharacterManager _instance;
    public static CharacterManager Instance { get { return _instance; } }



    private GameObject lastEnemyHover;


    // IA 

    List<MeasureDivision> randomizedMeasure = new List<MeasureDivision>();
    int rmInd = 0;
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

    public void Init()
    {
        foreach (PlayableChar pc in allied)
        {
            pc.Init();
        }
        foreach (EnemyChar ec in enemiesInCombat)
        {
            ec.Init();
        }
    }

    private void Update()
    {
        if (!targeting)
            return;


        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, float.MaxValue, enemyLayer))
        {
            if (lastEnemyHover == null)
            {
                lastEnemyHover = hit.collider.gameObject;
                hit.collider.GetComponent<EnemyChar>().OnHover();
            }
            if (lastEnemyHover != hit.collider.gameObject)
            {
                lastEnemyHover.GetComponent<EnemyChar>().OnExit();
                lastEnemyHover = hit.collider.gameObject;
                lastEnemyHover.GetComponent<EnemyChar>().OnHover();
            }

            if (Input.GetMouseButtonDown(0))
            {
                PickTarget();

            }
        }
    }

    private void PickTarget()
    {

        TurnManager.Instance.target = lastEnemyHover.GetComponent<EnemyChar>();
        SetEnemyTargetState(false);
        lastEnemyHover = null;
    }

    public void SetEnemyTargetState(bool state)
    {
        targeting = state;
        UIManager.Instance.SetEnemyTargetState(state);
        if (targeting)
        {

        }
        else
        {
            foreach (EnemyChar ec in enemiesInCombat)
            {
                ec.OnExit();
            }
        }
    }
    public void OrderAllCharByInitiative()
    {
        orderedAllied = allied.OrderBy(c => c.initiative).ToList();

        orderedEnemies = enemiesInCombat.OrderBy(c => c.initiative).ToList();
    }


    // IA 

    public void MakeEnemyTurn()
    {
        print(enemiesInCombat.Count);
        for (int i = 0; i < enemiesInCombat.Count; i++)
        {

            if (enemiesInCombat[i].life <= 0)
            {

                CharacterManager.Instance.enemiesInCombat.RemoveAt(i);
            }
        }

        enemiesInCombat.RemoveAll(item => item == null);

        if (enemiesInCombat.Count <= 0)
        {
            UIManager.Instance.End();
        }

        foreach (EnemyChar ec in enemiesInCombat)
        {
            rmInd = 0;

            if (ec.HasState(GameData.CharacterState.Stunned))
            {
                ec.ResetState();
                continue;
            }

            randomizedMeasure = TurnManager.Instance.measures.OrderBy(a => Guid.NewGuid()).ToList(); // Random list
            MeasureDivision md = PlaceAttack(ec);

            if (md == null)
            {
                continue;
            }
        }
    }

    public MeasureDivision PlaceAttack(EnemyChar ec)
    {
        int maxFreeSize = randomizedMeasure[rmInd].GetFreeSpace();

        List<Spell> _temp = ec.charInfo.spellList.OrderBy(x => x.time).ToList();

        foreach (Spell s in _temp.ToList())
        {
            if (s.time > maxFreeSize)
            {
                _temp.Remove(s);
            }
        }
        _temp.RemoveAll(item => item == null);

        if (_temp.Count == 0)
        {
            rmInd++;
            return PlaceAttack(ec);
        }

        if (CheckIfMeasureIsValid(randomizedMeasure[rmInd], _temp, maxFreeSize))
        {
            Spell sp = _temp[UnityEngine.Random.Range(0, _temp.Count)];

            int index = randomizedMeasure[rmInd].GetMaxSpaceIndex();
            int dif = maxFreeSize - sp.time;

            index += UnityEngine.Random.Range(0, dif);
            List<StepSpellInfo> _te = randomizedMeasure[rmInd].GetAllStepSpellWithoutCondition(index, sp.time);
            foreach (StepSpellInfo ssi in _te)
            {
                ssi.SetStep(sp, ec);
            }
            return null;
        }
        else
        {
            rmInd++;

            if (rmInd > 2)
            {
                return null;
            }
            else
            {
                return PlaceAttack(ec);
            }
        }
    }

    public bool CheckIfMeasureIsValid(MeasureDivision measureDivision, List<Spell> _temp, int maxFreeSize)
    {

        if (maxFreeSize < _temp[0].time)
        {
            return false;
        }
        if (!orderedAllied.Contains(measureDivision.autorizedChar))
        {
            return false;
        }
        return true;
    }
}
