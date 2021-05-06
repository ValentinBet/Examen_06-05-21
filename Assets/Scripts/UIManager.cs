using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject spellListContainer;

    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }


    public List<CharInfoPanel> charInfoPanel = new List<CharInfoPanel>();
    public List<EnemyCharInfoPanel> enemyCharInfoPanel = new List<EnemyCharInfoPanel>();
    public List<MeasureDivision> measureDivisions = new List<MeasureDivision>();

    Dictionary<Character, CharInfoPanel> charPanelDict = new Dictionary<Character, CharInfoPanel>();

    Dictionary<Character, EnemyCharInfoPanel> enemyCharPanelDict = new Dictionary<Character, EnemyCharInfoPanel>();

    public Transform progressPinLeft;
    public Transform progressPinRight;
    public GameObject progressPin;


    float t;
    float timeToReachTarget;
    public bool measureStarted = false;
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

    private void Update()
    {
        if (measureStarted)
        {
            t += Time.deltaTime / timeToReachTarget;
            progressPin.transform.position = Vector3.Lerp(progressPinLeft.position, progressPinRight.position, t);
        }
    }



    public void Init()
    {
        for (int i = 0; i < CharacterManager.Instance.allied.Count; i++)
        {
            charInfoPanel[i].Init(CharacterManager.Instance.allied[i]);
            charPanelDict.Add(CharacterManager.Instance.allied[i], charInfoPanel[i]);
        }
        for (int i = 0; i < CharacterManager.Instance.enemiesInCombat.Count; i++)
        {
            enemyCharInfoPanel[i].Init(CharacterManager.Instance.enemiesInCombat[i]);
            enemyCharPanelDict.Add(CharacterManager.Instance.enemiesInCombat[i], enemyCharInfoPanel[i]);
        }

        for (int i = 0; i < measureDivisions.Count; i++)
        {
            measureDivisions[i].Init(CharacterManager.Instance.allied[i]);
        }

    }

    public CharInfoPanel GetPlbCharPanel(PlayableChar pc)
    {
       return charPanelDict[pc];
    }

    public EnemyCharInfoPanel GetEnemyCharPanel(EnemyChar ec)
    {
        return enemyCharPanelDict[ec];
    }

    public void InitSpellList()
    {

        PlayableChar playableChar = TurnManager.Instance.charTurn;

        for (int i = 0; i < playableChar.charInfo.spellList.Count; i++)
        {
            UISpellPanel _temp = spellListContainer.transform.GetChild(i).gameObject.GetComponent<UISpellPanel>();
            _temp.Init(playableChar.charInfo.spellList[i]);
            _temp.gameObject.SetActive(true);

        }
        spellListContainer.SetActive(true);
    }

    public void CloseSpellList()
    {
        spellListContainer.SetActive(false);

        for (int i = 0; i < 4; i++)
        {
            spellListContainer.transform.GetChild(i).gameObject.SetActive(false);
        }
    }


    public void StartMovePin(float timeToReach = 1)
    {
        timeToReachTarget = timeToReach;
           measureStarted = true;
    }

    public void StopAndResetPin()
    {
        progressPin.transform.position = progressPinLeft.position;
        measureStarted = false;
    }
}
