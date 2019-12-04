using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
        SceneManager.UnloadSceneAsync("Menu_Main");
    }
}
