using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ObjectiveHandler : MonoBehaviour
{
    public GameObject CityGenerator;
    public GameObject ScoreScreen;

    [Header("UI")]
    public Text TimerText;
    public Text EnemiesLeftText;
    public Button MenuButton;
    public Button QuitButton;

    private int enemiesToDefeat;

    void Start()
    {
        this.enemiesToDefeat = this.CityGenerator.GetComponent<CityGenerator>().EnemiesToSpawn;
        this.EnemiesLeftText.text = this.enemiesToDefeat.ToString();

        this.MenuButton.onClick.AddListener(this.GoToMenu);
        this.QuitButton.onClick.AddListener(this.QuitGame);
    }

    void Update()
    {
        this.TimerText.text = $"{Mathf.Floor(Time.time / 60).ToString("00")}:{Mathf.RoundToInt(Time.time % 60).ToString("00")}";
    }

    private List<GameObject> defeated = new List<GameObject>();
    public void defeatEnemy(GameObject enemy)
    {
        this.defeated.Add(enemy);
        this.UpdateEnemyText();

        // Game Over
        if (this.enemiesToDefeat - this.defeated.Count == 0)
        {
            GameObject.FindGameObjectWithTag("Player").SendMessage("OnPauseGame");
            this.ScoreScreen.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void UpdateEnemyText()
    {
        this.EnemiesLeftText.text = (this.enemiesToDefeat - this.defeated.Count).ToString();
    }

    void GoToMenu()
    {
        SceneManager.LoadSceneAsync("Menu_Main");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menu_Main"));
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Game"));
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
