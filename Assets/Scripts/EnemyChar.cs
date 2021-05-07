using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChar : Character
{

    public Material hoverMat;
    public Material baseMat;

    public MeshRenderer meshRenderer;
    public void OnHover()
    {
        meshRenderer.material = hoverMat;
    }

    public void OnExit()
    {
        meshRenderer.material = baseMat;
    }

    public override void Kill()
    {
        base.Kill();



        UIManager.Instance.GetEnemyCharPanel(this).gameObject.SetActive(false);
    }
    public override void TakeDamages(int damages = 1)
    {
        base.TakeDamages(damages);

        UIManager.Instance.GetEnemyCharPanel(this).UpdateCharInfo(this);
    }

    public override void GainArmor(int armor = 1)
    {
        base.GainArmor(armor);

        UIManager.Instance.GetEnemyCharPanel(this).UpdateCharInfo(this);
    }



}
