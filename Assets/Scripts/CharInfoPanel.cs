using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharInfoPanel : MonoBehaviour
{

    public TextMeshProUGUI charNameText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI armorText;

    public Image lifeImgFill;
    public Image boostedImg;
    public void Init(PlayableChar pc)
    {
        charNameText.text = pc.charInfo.charName;
        charNameText.color = pc.charInfo.mainColor;
        manaText.text = pc.charInfo.manaMax + "";
        lifeText.text = pc.charInfo.lifeMax + "";
        armorText.text = pc.charInfo.baseArmor + "";
        lifeImgFill.fillAmount = 1;
    }

    public void UpdateCharInfo(PlayableChar pc)
    {
        manaText.text = pc.mana + "";
        lifeText.text = pc.life + "";
        armorText.text = pc.armor + "";
        lifeImgFill.fillAmount = (float)pc.life / (float)pc.charInfo.lifeMax;

        if (pc.HasState(GameData.CharacterState.Boosted))
        {
            boostedImg.gameObject.SetActive(true);
        } else
        {
            boostedImg.gameObject.SetActive(false);
        }

    }
}
