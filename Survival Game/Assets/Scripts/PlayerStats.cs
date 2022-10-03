using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxPoints = 100f;
    [SerializeField] private Image frontBar;
    [SerializeField] private Image backBar;

    private float currentPoints;
    private float lerpSpeed = 2f * Time.deltaTime;

    public float CurrentPoints { get { return currentPoints; } set { currentPoints = value; } }
    public float MaxPoints { get { return currentPoints; } set { currentPoints = value; } }


    private void Start()
    {
        currentPoints = maxPoints;
    }

    private void Update()
    {
        currentPoints = Mathf.Clamp(currentPoints, 0, maxPoints);

    }

    private void UpdateBar()
    {

        float PointFraction = currentPoints / maxPoints;
        if (backBar.fillAmount > PointFraction)
        {
            frontBar.fillAmount = PointFraction;
            backBar.fillAmount = Mathf.Lerp(backBar.fillAmount, PointFraction, lerpSpeed);
        }
        else if (frontBar.fillAmount < PointFraction)
        {
            backBar.fillAmount = PointFraction;
            frontBar.fillAmount = Mathf.Lerp(frontBar.fillAmount, PointFraction, lerpSpeed);
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


}
