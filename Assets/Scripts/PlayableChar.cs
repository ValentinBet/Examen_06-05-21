using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableChar : Character
{

    public override void Kill()
    {
        base.Kill();

        UIManager.Instance.GetPlbCharPanel(this).gameObject.SetActive(false);
    }
    public override void TakeDamages(int damages = 1)
    {
        base.TakeDamages(damages);

        UIManager.Instance.GetPlbCharPanel(this).UpdateCharInfo(this);
    }

    public override void GainArmor(int armor = 1)
    {
        base.GainArmor(armor);

        UIManager.Instance.GetPlbCharPanel(this).UpdateCharInfo(this);
    }
}
