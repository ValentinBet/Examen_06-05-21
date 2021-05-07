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
            actualPreview = null;
            return; // Cant place spell here
        }

        List<StepSpellInfo> _temp = GetAllStepSpell(indexPos, spellInfo.time);
        ResetPreview();

        if (_temp == null)
        {
            actualPreview = null;
            return; // Cant place spell here
        }

        actualPreview = _temp;

        foreach (StepSpellInfo spi in actualPreview)
        {
            spi.Highlight();
        }

    }

    internal bool SetSpellPos(Spell chosenSpell, int indexPos)
    {
        if (actualPreview != null)
        {
            foreach (StepSpellInfo spi in actualPreview)
            {
                spi.SetStep(chosenSpell);
            }
            return true;
        } else
        {
            return false;
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
        if (actualPreview != null)
        {
            foreach (StepSpellInfo spi in actualPreview)
            {
                if (spi.setStep == false)
                {
                    spi.ResetHighlight();
                }
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
            }
            else
            {
                _returnVal.Add(spi);
            }
        }

        return _temp;
    }
    public List<StepSpellInfo> GetAllStepSpellWithoutCondition(int firstIndex, int size)
    {

        if (firstIndex + size > measureLenght)
        {
            return null;
        }
        List<StepSpellInfo> _temp = new List<StepSpellInfo>();


        for (int i = firstIndex; i < firstIndex + size; i++)
        {
            _temp.Add(stepSpellInfos[i]);
        }

        return _temp;
    }



    public void GiveOneShotSpellEffect(int firstIndex, int size)
    {
        List<StepSpellInfo> _temp = GetAllStepSpellWithoutCondition(firstIndex, size);

        foreach (StepSpellInfo item in _temp)
        {
            item.asGivenOneShotEffect = true;
        }

    }


    public int GetFreeSpace()
    {

        int sizeFree = 0;
        int maxsizeFree = 0;

        foreach (StepSpellInfo spi in stepSpellInfos)
        {

            if (spi.setStep == false)
            {
                sizeFree++;
            } else
            {
                sizeFree = 0;
            }

            if (sizeFree > maxsizeFree)
            {
                maxsizeFree = sizeFree;
            }
        }


        return maxsizeFree;
    }


    public int GetMaxSpaceIndex()
    {
        int index = 0;
        int sizeFree = 0;
        int maxsizeFree = 0;

        for (int i = 0; i < stepSpellInfos.Count; i++)
        {
            if (stepSpellInfos[i].setStep == false)
            {
                sizeFree++;
            }
            else
            {
                sizeFree = 0;
            }

            if (sizeFree > maxsizeFree)
            {
                maxsizeFree = sizeFree;
                index = (i - (maxsizeFree - 1)) ;
            }
        }



        return index;
    }

}
