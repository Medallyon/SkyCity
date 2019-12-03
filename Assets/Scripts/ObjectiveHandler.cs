using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveHandler : MonoBehaviour
{
    public GameObject CityGenerator;

    [Header("UI")]
    public Text EnemiesLeftText;

    private int enemiesToDefeat;

    // Start is called before the first frame update
    void Start()
    {
        this.enemiesToDefeat = this.CityGenerator.GetComponent<CityGenerator>().EnemiesToSpawn;
        this.EnemiesLeftText.text = this.enemiesToDefeat.ToString();
    }

    private List<GameObject> defeated;
    public void defeatEnemy(GameObject enemy)
    {
        this.defeated.Add(enemy);
        this.UpdateEnemyText();
    }

    void UpdateEnemyText()
    {
        this.EnemiesLeftText.text = (this.enemiesToDefeat - this.defeated.Count).ToString();
    }
}
