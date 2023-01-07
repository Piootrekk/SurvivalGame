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
    

    [SerializeField] Transform lvlPanel;
    [SerializeField] Transform maxLvlPanel;
    [SerializeField] Transform xpCost;
    [SerializeField] Transform button;

    private float skillFactor = 1f;
    private int currentPriceXPCost;

    void Awake()
    {
        startXPCost = currentPriceXPCost;
        ChangeStartData();
    }

    private void ChangeStartData()
    {
        lvlPanel.GetComponent<TextMeshProUGUI>().text = skillLvl.ToString();
        maxLvlPanel.GetComponent<TextMeshProUGUI>().text = maxSkillLvl.ToString();
        xpCost.GetComponent<TextMeshProUGUI>().text = currentPriceXPCost.ToString();
    }

}
