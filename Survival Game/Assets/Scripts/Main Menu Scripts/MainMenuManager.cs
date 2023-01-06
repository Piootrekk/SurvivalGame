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

    [SerializeField] List<TMP_InputField> inputValuesSEED;
    [SerializeField] List<TMP_InputField> inputValuesSIZE;

    private List<int> inputIntsSEED = new();
    private List<int> inputIntsSIZE = new();

    private void Awake()
    {
        LoadBackground();
    }

    public List<int> GetValueFromInputs(List<TMP_InputField> list, int defalutValue)
    {
        List<int> intList = new();
        foreach (var input in list)
        {
            if (input.text == string.Empty) intList.Add(defalutValue);
            else
            {
                int value = int.Parse(input.text);
                intList.Add(value);
            }
            
        }
        return intList;
    }


    public void LoadBackground()
    {
        BackgroundMenu.GetComponent<Image>().sprite = sprites[new System.Random().Next(sprites.Count)];
    }

    public void SaveValues(List<int> intList, string name)
    {
        for (int i = 0; i < intList.Count; i++)
        {
            PlayerPrefs.SetInt(name + i, intList[i]);
        }
    }


    public void NewGame()
    {
        inputIntsSEED = GetValueFromInputs(inputValuesSEED, 100);
        SaveValues(inputIntsSEED, "SEED");
        inputIntsSIZE = GetValueFromInputs(inputValuesSIZE, 30);
        SaveValues(inputIntsSIZE, "SIZE");
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
