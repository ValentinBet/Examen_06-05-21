using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureDivision : MonoBehaviour
{
    public GameObject stepObj;
    public GameObject container;
    public PlayableChar autorizedChar;
    public List<StepSpellInfo> stepSpellInfos = new List<StepSpellInfo>();

    public int measureLenght = 16;

    public List<StepSpellInfo> actualPreview = new List<StepSpellInfo>();
    public void Init(PlayableChar playableChar)
    {
        autorizedChar = playableChar;

        for (int i = 0; i < measureLenght; i++)
        {
            GameObject go = Instantiate(stepObj, container.transform);
            StepSpellInfo _temp = go.GetComponent<StepSpellInfo>();
            _temp.Init(i, this);
            stepSpellInfos.Add(_temp);
        }
    }

    public void CanPutSpell(List<StepSpellInfo> stepSpellInfos)
    {

    }

    public void PreviewSpellPos(Spell spellInfo, int indexPos)
    {
        if (spellInfo.time + indexPos > measureLenght)
        {
            ResetPreview();
            return; // Cant place spell here
        }

        List<StepSpellInfo> _temp = GetAllStepSpell(indexPos, spellInfo.time);
        ResetPreview();

        if (_temp == null)
        {
            return; // Cant place spell here
        }

        actualPreview = _temp;

        foreach (StepSpellInfo spi in actualPreview)
        {
            spi.Highlight();
        }

    }

    internal void SetSpellPos(Spell chosenSpell, int indexPos)
    {
        if (actualPreview != null)
        {
            foreach (StepSpellInfo spi in actualPreview)
            {
                spi.SetStep(chosenSpell);
            }
        }
    }

    internal void ResetStep()
    {
        foreach (StepSpellInfo spi in stepSpellInfos)
        {
            spi.ResetStep();
            
        }
    }

    public void ResetPreview()
    {
        foreach (StepSpellInfo spi in actualPreview)
        {
            if (spi.setStep == false)
            {
                spi.ResetHighlight();
            }
        }
    }

    public List<StepSpellInfo> GetAllStepSpell(int firstIndex, int size)
    {

        if (firstIndex + size > measureLenght)
        {
            return null;
        }

        List<StepSpellInfo> _temp = new List<StepSpellInfo>();
        List<StepSpellInfo> _returnVal = new List<StepSpellInfo>();

        for (int i = firstIndex; i < firstIndex + size; i++)
        {
            _temp.Add(stepSpellInfos[i]);

        }

        foreach (StepSpellInfo spi in _temp)
        {
            if (spi.setStep == true)
            {
                return null;
            } else
            {
                _returnVal.Add(spi);
            }
        }

        return _temp;
    }

}
