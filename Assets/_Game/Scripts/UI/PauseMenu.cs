using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Screens")]
    public GameObject PauseScreen;
    public GameObject Instructions;

    [Header("Buttons")]
    public Button ResumeButton;
    public Button QuitButton;
    public Button InstructionsButton;
    public Button InstructionsBackButton;

    private bool paused;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");

        this.ResumeButton.onClick.AddListener(this.TogglePaused);
        this.QuitButton.onClick.AddListener(this.QuitGame);
        this.InstructionsButton.onClick.AddListener(this.ToggleInstructions);
        this.InstructionsBackButton.onClick.AddListener(this.ToggleInstructions);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Cancel"))
            this.TogglePaused();
    }

    void TogglePaused()
    {
        this.paused = !this.paused;

        Debug.Log($"Paused: {this.paused}");

        this.player.SendMessage(this.paused ? "OnPauseGame" : "OnUnpauseGame");

        Cursor.lockState = this.paused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = this.paused;
        this.PauseScreen.SetActive(this.paused);
        this.Instructions.SetActive(false);
    }

    void QuitGame()
    {
        Application.Quit();
    }

    void ToggleInstructions()
    {
        this.Instructions.SetActive(!this.Instructions.activeSelf);
    }
}
