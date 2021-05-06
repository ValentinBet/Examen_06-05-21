using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCharInfoPanel : MonoBehaviour
{

    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI armorText;

    public Image lifeImgFill;
    public void Init(EnemyChar pc)
    {
        lifeText.text = pc.charInfo.lifeMax + "";
        armorText.text = pc.charInfo.baseArmor + "";
        lifeImgFill.fillAmount = 1;
    }

    public void UpdateCharInfo(EnemyChar pc)
    {
        lifeText.text = pc.life + "";
        armorText.text = pc.armor + "";
        lifeImgFill.fillAmount = pc.life / pc.charInfo.lifeMax;
    }
}
