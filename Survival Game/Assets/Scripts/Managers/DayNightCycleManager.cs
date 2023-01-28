using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycleManager : MonoBehaviour
{   
    [Range(0f, 1f)]
    [SerializeField] private float dayCycle;
    [SerializeField] private float fullDayLenght;
    [SerializeField] private float startTime = 0.5f;
    [SerializeField] private Vector3 noon;
    [SerializeField] private int dayCount = 1;
    private float timeRate;

    [Header("Sun")]
    [SerializeField] private Light sun;
    [SerializeField] private Gradient sunColor;
    [SerializeField] private AnimationCurve sunCurve;


    [Header("Moon")]
    [SerializeField] private Light moon;
    [SerializeField] private Gradient moonColor;
    [SerializeField] private AnimationCurve moonCurve;

    [Header("Lighting Setting")]
    [SerializeField] private AnimationCurve lightingMulitpler;
    [SerializeField] private AnimationCurve reflectionMultiply;

    private static DayNightCycleManager instance;
    private float damageMultiplyer;
    public static DayNightCycleManager Instance => instance;
    public float DayCycle { get => dayCycle; set => dayCycle = value; }
    public float DayCount => dayCount;
    public float DamageMultiplayer => damageMultiplyer;

    private void Start()
    {
        timeRate = 1.0f / fullDayLenght;
        dayCycle = startTime;
        instance = this;
    }


    private void Update()
    {
        dayCycle += timeRate * Time.deltaTime;
        DayCounter();
        SetLightRotate();
        CheckForDamage();
    }

    private void DayCounter()
    {
        if (dayCycle > 1.0f)
        {
            dayCycle = 0.0f;
            dayCount++;
        }
    }
    private void CheckForDamage()
    {
        if (sun.intensity == 0) damageMultiplyer = 1.5f;
        else damageMultiplyer = 1.0f;
    }
    private void SetLightRotate()
    {
        sun.transform.eulerAngles = (dayCycle - 0.25f) * 4.0f * noon;
        moon.transform.eulerAngles = (dayCycle - 0.75f) * 4.0f * noon;
        SetLightIntensity();
        ChangeColors();
        ActiveAndDisactiveLightSoon();
        ActiveAndDisactiveLightMoon();
        SetRenderSetting();

    }
    private void SetLightIntensity()
    {
        sun.intensity = sunCurve.Evaluate(dayCycle);
        moon.intensity = moonCurve.Evaluate(dayCycle);
    }
    private void ChangeColors()
    {
        sun.color = sunColor.Evaluate(dayCycle);
        moon.color = moonColor.Evaluate(dayCycle);
    }
    private void ActiveAndDisactiveLightSoon()
    {
        if (sun.intensity == 0 && sun.gameObject.activeInHierarchy) sun.gameObject.SetActive(false);
        else if (sun.intensity > 0 && !sun.gameObject.activeInHierarchy) sun.gameObject.SetActive(true);
    }
    private void ActiveAndDisactiveLightMoon()
    {
        if (moon.intensity == 0 && moon.gameObject.activeInHierarchy) moon.gameObject.SetActive(false);
        else if (moon.intensity > 0 && !moon.gameObject.activeInHierarchy) moon.gameObject.SetActive(true);
    }
    private void SetRenderSetting()
    {
        RenderSettings.ambientIntensity = lightingMulitpler.Evaluate(dayCycle);
        RenderSettings.reflectionIntensity = reflectionMultiply.Evaluate(dayCycle);
    }

}
