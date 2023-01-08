using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    
    [SerializeField] private CurrentSkill factorSkillJump;
    [SerializeField] private CurrentSkill factorSkillRun;
    [SerializeField] private CurrentSkill factorSkillForce;
    [SerializeField] private CurrentSkill factorSkillHP;
    [SerializeField] private CurrentSkill factorSkillHungry;
    [SerializeField] private CurrentSkill factorSkillThirsty;
    [Space]
    [SerializeField] private Transform XpText;

    private static SkillManager instance;
    [SerializeField] private int playerExp;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        XpTextUpdate();
    }

    public static SkillManager Instance => instance;
    public int PlayerExp { get => playerExp; set => playerExp = value; }

    public float FactorSkillJump => factorSkillJump.CurrentskillFactor;
    public float FactorSkillRun => factorSkillRun.CurrentskillFactor;
    public float FactorSkillForce => factorSkillForce.CurrentskillFactor;
    public float FactorSkillHP => factorSkillHP.CurrentskillFactor;
    public float FactorSkillHungry => factorSkillHungry.CurrentskillFactor;
    public float FactorSkillThirsty => factorSkillThirsty.CurrentskillFactor;

    private void XpTextUpdate()
    {
        XpText.GetComponent<TextMeshProUGUI>().text = playerExp.ToString();
    }

    public void HpUpdate()
    {
        StatsManager.Instance.Health.ChangeCurrentMaxPoints(FactorSkillHP);
    }

    public void ThirstyUpdate()
    {
        StatsManager.Instance.Thirst.ChangeCurrentMaxPoints(FactorSkillThirsty);
    }
    public void HungryUpdate()
    {
        StatsManager.Instance.Hunger.ChangeCurrentMaxPoints(FactorSkillHungry);
    }

    public void SpeedUpdate()
    {
        PlayerController.Instance.ChangeSpeed(FactorSkillRun);
    }
}

