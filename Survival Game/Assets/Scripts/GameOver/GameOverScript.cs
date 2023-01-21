using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] GameObject gameOver;
    [SerializeField] TextMeshProUGUI text;

    public bool GameOver { get; private set; } = false;
    public static GameOverScript Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(!gameOver.activeSelf && StatsManager.Instance.Health.CurrentPoints <= 0)
        {
            gameOver.SetActive(true);
            GameOver = true;
            Time.timeScale = 0;
            AddText();
        }
    }

    private void AddText()
    {
        text.text = text.text.Replace("X", DayNightCycleManager.Instance.DayCount.ToString());
    }

}
