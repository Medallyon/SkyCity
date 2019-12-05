using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject ExplosionPrefab;

    public float MaxHealth = 10f;

    private float health = 10f;
    public float Health
    {
        get
        {
            return this.health;
        }
        set
        {
            this.health = Mathf.Max(0f, Mathf.Min(value, this.MaxHealth));
            if (this.Health == 0f)
                this.Destroy();
        }
    }

    void Destroy()
    {
        GameObject.Find("Game").GetComponent<ObjectiveHandler>().defeatEnemy(this.gameObject);

        GameObject explosion = Instantiate(this.ExplosionPrefab);
        explosion.transform.position = this.transform.position;

        Destroy(this.gameObject);
    }
}
