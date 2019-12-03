using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float Speed = 100f;
    public float DestroyAfter = 10f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
        if (this.DestroyAfter > 0f)
            Invoke("DestroyTimer_Elapsed", this.DestroyAfter);
    }

    public void DestroyTimer_Elapsed()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        this.rb.velocity = this.Speed * this.transform.up;
    }
}
