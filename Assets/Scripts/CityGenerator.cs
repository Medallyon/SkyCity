using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    public GameObject[] BuildingPrefabs;
    public Vector2 CityConstraints = new Vector2(10, 10);
    public float Padding = 20f;

    // Start is called before the first frame update
    void Start()
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
                Vector3 spawnPosition = startingPosition - new Vector3(this.Padding * i + (Random.Range(-(this.Padding), this.Padding) / 8), 0, this.Padding * j + (Random.Range(-(this.Padding), this.Padding) / 8));

                // Don't spawn a building in the center of the grid (player starting position)
                if (i == this.CityConstraints.x / 2 && j == this.CityConstraints.y / 2)
                    continue;

                Instantiate(this.BuildingPrefabs[Random.Range(0, this.BuildingPrefabs.Length)], spawnPosition, new Quaternion(0, Random.Range(0, 3) * 90, 0, 0));
            }
        }
    }
}
