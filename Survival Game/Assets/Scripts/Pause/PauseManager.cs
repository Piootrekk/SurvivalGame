using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    private InputManager inputManager;
    private void Awake()
    {
        inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
    }


    private void Update()
    {
        CheckESC();
    }

    private void CheckESC()
    {
        pausePanel.SetActive(inputManager.ESC);
        ChangeTimeScale(inputManager.ESC);
    }

    private void ChangeTimeScale(bool input)
    {
       if(input || GameOverScript.Instance.GameOver)
       {
            Time.timeScale = 0;
       }
       else
       {
            Time.timeScale = 1;
       }
    }

    public void OnResume()
    {
        if (inputManager.ESC)
        {
            inputManager.ESC = !inputManager.ESC;
        }
    }


    public void OnMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
