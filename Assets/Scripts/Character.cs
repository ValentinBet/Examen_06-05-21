using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameData;

public class Character : MonoBehaviour
{
    public CharInfo charInfo;
    public int life = 100;
    public int mana = 1;
    public int initiative = 3;
    public int armor = 0;

    public CharacterState _state = CharacterState.Clear;

    // Start is called before the first frame update

    public void Init()
    {
        SetupCharInfo();
    }
    private void SetupCharInfo()
    {
        life = charInfo.lifeMax;
        mana = charInfo.manaMax;
        initiative = charInfo.baseInitiative;
        armor = charInfo.baseArmor;
    }


    public virtual void TakeDamages(int damages = 0)
    {
        if (armor < 0)
        {
            damages += armor;
        } else
        {
            damages -= armor;
        }

        int finalDmg = Mathf.Clamp((damages),0,  int.MaxValue);

        life -= finalDmg;

        if (life <= 0)
        {
            Kill();
        }
    }

    public virtual void GainArmor(int armorGain = 0)
    {
        armor += armorGain;
    }

    public virtual void ResetArmor()
    {
        armor = charInfo.baseArmor;
    }
    public virtual void Heal(int Heal = 1)
    {
        life += Heal;
    }
    public virtual void Kill()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void AddState(CharacterState _stateToadd)
    {
        _state |= _stateToadd;
    }

    public virtual bool HasState(CharacterState _stateToadd)
    {
        if (_state.HasFlag(_stateToadd))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public virtual void RemoveState(CharacterState _stateToRemove)
    {
        _state &= ~_stateToRemove;
    }

    public virtual void ResetState()
    {
        _state = CharacterState.Clear;
    }
}
