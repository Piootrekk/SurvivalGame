using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject BackgroundMenu;

    [SerializeField] List<TMP_InputField> inputValues;

    private List<int> inputInts = new();

    private void Awake()
    {
        LoadBackground();
    }

    public void GetValueFromInputs()
    {
        foreach (var input in inputValues)
        {
            if (input.text == string.Empty) inputInts.Add(1);
            else
            {
                int value = int.Parse(input.text);
                inputInts.Add(value);
            }
            
        }
    }


    public void LoadBackground()
    {
        BackgroundMenu.GetComponent<Image>().sprite = sprites[new System.Random().Next(sprites.Count)];
    }

    public void SaveValues()
    {
        for (int i = 0; i < inputInts.Count; i++)
        {
            PlayerPrefs.SetInt("SEED" + i, inputInts[i]);
        }
    }


    public void NewGame()
    {
        GetValueFromInputs();
        SaveValues();
        SceneManager.LoadScene("NewGame");

    }
    public void LoadGame()
    {

    }
    public void Settings()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
