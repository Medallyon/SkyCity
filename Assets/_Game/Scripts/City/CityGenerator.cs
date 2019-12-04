using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CityGenerator : MonoBehaviour
{
    [Header("City Generation")]
    public GameObject[] BuildingPrefabs;
    public Vector2 CityConstraints = new Vector2(10, 10);
    public float Padding = 20f;

    public GameObject Surface;

    [Header("Floating Scrapers")]
    [Tooltip("The number of groups that should be created. Each group has synchronised floating for performance reasons.")]
    public int FloatingGroups = 5;
    private List<GameObject> groups = new List<GameObject>();

    [Header("Enemy Generation")]
    public int EnemiesToSpawn = 5;
    public GameObject[] EnemyPrefabs;
    private List<GameObject> enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting City Generation");
        for (int i = 0; i < this.FloatingGroups; i++)
        {
            this.groups.Add(new GameObject($"Scraper Group {i}"));
            this.groups[i].AddComponent<FloatingScraper>().Speed = .25f;
        }
        
        this.GenerateScrapers();
        this.GeneratePatrolGrid();

        //this.NavMesh.GetComponent<NavMeshSurface>().BuildNavMesh();
        this.Surface.GetComponent<MeshRenderer>().enabled = false;

        this.SpawnEnemies();
    }

    void GenerateScrapers()
    {
        if (this.BuildingPrefabs.Length == 0)
        {
            Debug.Log("No building prefabs entered. Aborting City Generation");
            return;
        }

        Vector3 startingPosition = this.transform.position + new Vector3((this.Padding / 2) * this.CityConstraints.x, 0, (this.Padding / 2) * this.CityConstraints.y);

        for (int i = 0; i < this.CityConstraints.x; i++)
        {
            for (int j = 0; j < this.CityConstraints.y; j++)
            {
                Vector3 spawnPosition = startingPosition - new Vector3(this.Padding * i + Random.Range(-25, 25), 0, this.Padding * j + Random.Range(-25, 25));

                // Don't spawn a building in the center of the grid (player starting position)
                if (i == this.CityConstraints.x / 2 && j == this.CityConstraints.y / 2)
                    continue;

                GameObject scraperGroup = this.groups[Random.Range(0, this.groups.Count)];
                Debug.Log($"Spawning Scraper at {spawnPosition}");
                GameObject scraper = Instantiate(this.BuildingPrefabs[Random.Range(0, this.BuildingPrefabs.Length)], spawnPosition, new Quaternion());

                scraper.transform.parent = scraperGroup.transform;
            }
        }

        Debug.Log($"Spawned Scrapers");
    }

    void GeneratePatrolGrid()
    {
        int width = (int)this.CityConstraints.x + 1;
        int height = (int)this.CityConstraints.y + 1;
        float halfPad = this.Padding / 2;

        Vector3 startingPosition = this.transform.position + new Vector3(halfPad * width - halfPad / 2, 100, halfPad * height - halfPad / 2);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 patrolPos = startingPosition - new Vector3(this.Padding * i, 0, this.Padding * j);
                AIMovement.PatrolPoints.Add(patrolPos);
            }
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < this.EnemiesToSpawn; i++)
            this.SpawnEnemy(AIMovement.RandomPatrolPoint);
    }

    void SpawnEnemy(Vector3 target)
    {
        GameObject enemy = Instantiate(this.EnemyPrefabs[Random.Range(0, this.EnemyPrefabs.Length)], target, new Quaternion());
        enemy.GetComponent<AIMovement>().NavMesh = this.Surface.GetComponent<NavMeshSurface>();

        NavMeshHit navHit;
        if (NavMesh.SamplePosition(target, out navHit, 500, -1))
        {
            enemy.transform.position = navHit.position;
            enemy.GetComponent<AIMovement>().ImplementNavMeshAgent();
        }

        this.enemies.Add(enemy);
    }
}