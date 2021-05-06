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
}
