using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Buttons")]
    public Button PlayButton;
    public Button QuitButton;
    public Button InstructionsButton;
    public Button InstructionsBackButton;
    [Header("Screens")]
    public GameObject LoadingScreen;
    public GameObject InstructionsScreen;
    
    void Start()
    {
        this.PlayButton.onClick.AddListener(this.StartGame);
        this.QuitButton.onClick.AddListener(this.Quit);
        this.InstructionsButton.onClick.AddListener(this.ToggleInstructions);
        this.InstructionsBackButton.onClick.AddListener(this.ToggleInstructions);
    }

    void StartGame()
    {
        this.LoadingScreen.SetActive(true);
        SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
    }

    void Quit()
    {
        Application.Quit();
    }

    void ToggleInstructions()
    {
        this.InstructionsScreen.SetActive(!this.InstructionsScreen.activeSelf);
    }
}
