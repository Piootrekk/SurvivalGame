using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CurrentSkill : MonoBehaviour
{

    [SerializeField] private int skillLvl = 1;
    [SerializeField] private int maxSkillLvl = 10;
    [SerializeField] private int startXPCost = 100;
    [SerializeField] private float currentskillFactor = 1f;
    [SerializeField] private float skillFactorLvlUp = 0.1f;

    [SerializeField] Transform lvlPanel;
    [SerializeField] Transform maxLvlPanel;
    [SerializeField] Transform xpCost;
    [SerializeField] Transform button;

    private int currentPriceXPCost;

    public float CurrentskillFactor { get => currentskillFactor; set => currentskillFactor = value; }

    void Awake()
    {
        currentPriceXPCost = startXPCost;
        ChangeStartData();
    }

    private void Update()
    {
        DistableButton();
    }

    private void DistableButton()
    {
        if (skillLvl == maxSkillLvl || SkillManager.Instance.PlayerExp < currentPriceXPCost)
        {
            button.GetComponent<Button>().interactable = false;
        }
        else button.GetComponent<Button>().interactable = true;
    }

    private void ChangeStartData()
    {
        lvlPanel.GetComponent<TextMeshProUGUI>().text = skillLvl.ToString();
        maxLvlPanel.GetComponent<TextMeshProUGUI>().text = maxSkillLvl.ToString();
        xpCost.GetComponent<TextMeshProUGUI>().text = currentPriceXPCost.ToString();
    }

    public void OnClick()
    {
        skillLvl++;
        currentskillFactor += skillFactorLvlUp;
        SkillManager.Instance.PlayerExp -= currentPriceXPCost;
        currentPriceXPCost += currentPriceXPCost;
        ChangeStartData();
    }
}
