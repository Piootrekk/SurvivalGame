using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxPoints = 100f;
    [SerializeField] private Image frontBar;
    [SerializeField] private Image backBar;
    [SerializeField] private float currentPoints;
    [SerializeField] private float decayRate;
    private float lerp = 2f;

    public float CurrentPoints { get { return currentPoints; } set { currentPoints = value; } }
    public float MaxPoints { get { return currentPoints; } set { currentPoints = value; } }
    public float DecayRate { get { return decayRate; } set { decayRate = value; } }


    private void Start()
    {
        currentPoints = maxPoints;
    }

    private void Update()
    {
        currentPoints = Mathf.Clamp(currentPoints, 0, maxPoints);
        UpdateBar();

    }

    private void UpdateBar()
    {
        
        float fillBack = backBar.fillAmount;
        float fillFront = frontBar.fillAmount;
        float PointFraction = currentPoints / maxPoints;

        if (fillBack > PointFraction)
        {
            frontBar.fillAmount = PointFraction;
            backBar.fillAmount = Mathf.Lerp(fillBack, PointFraction, lerp * Time.deltaTime);
        }
        else if (fillFront < PointFraction)
        {
            backBar.fillAmount = PointFraction;
            frontBar.fillAmount = Mathf.Lerp(fillFront, backBar.fillAmount, lerp * Time.deltaTime);
        }
    }

    public void AddPoints(float points)
    {
        currentPoints += points;
    }

    public void TakePoints(float points)
    {
        currentPoints -= points;
    }

    public void RenegeratePoints(float points)
    {
        currentPoints = Mathf.Lerp(currentPoints, currentPoints + points, lerp * Time.deltaTime);
    }

}
