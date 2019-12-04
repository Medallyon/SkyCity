using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    public AudioSource explosionSound;

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
        GameObject.Find("ObjectiveHandler").GetComponent<ObjectiveHandler>().defeatEnemy(this.gameObject);

        // todo: explosion doesn't play / get instantiated
        Instantiate(this.ExplosionPrefab, this.transform);
        //this.explosionSound.Play();

        Destroy(this.gameObject);
    }
}
