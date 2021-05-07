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
    public Image stunnedIcon;
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
        lifeImgFill.fillAmount = (float)pc.life / (float)pc.charInfo.lifeMax;

        if (pc.HasState(GameData.CharacterState.Stunned))
        {
            stunnedIcon.gameObject.SetActive(true);
        } else
        {
            stunnedIcon.gameObject.SetActive(false);
        }
    }
}
