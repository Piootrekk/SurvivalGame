using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    private InputManager inputManager;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
    }


    private void Update()
    {
        CheckESC();
    }

    private void CheckESC()
    {
        pausePanel.SetActive(inputManager.ESC);
        Debug.Log(inputManager.ESC);
        
    }
}
