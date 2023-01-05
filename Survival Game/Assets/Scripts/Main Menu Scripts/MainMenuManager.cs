using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject BackgroundMenu;

    private void Awake()
    {
        LoadBackground();
    }

    public void LoadBackground()
    {
        BackgroundMenu.GetComponent<Image>().sprite = sprites[new System.Random().Next(sprites.Count)];
    }

    public void NewGame()
    {

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
