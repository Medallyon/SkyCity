using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float Speed = 100f;
    public float Damage = 1f;
    public float DestroyAfter = 10f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
        this.rb.velocity = this.Speed * this.transform.up;
        
        if (this.DestroyAfter > 0f)
            Invoke("DestroyTimer_Elapsed", this.DestroyAfter);
    }

    public void DestroyTimer_Elapsed()
    {
        Destroy(this.gameObject);
        CancelInvoke("DestroyTimer_Elapsed");
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collided with {collision.gameObject}");
        foreach (ContactPoint contact in collision.contacts)
            Debug.DrawRay(contact.point, contact.normal, Color.white);

        // TODO: Play Laser Sound

        GameObject other = collision.gameObject;
        if (other.tag == "Player" || other.tag == "NavMesh")
            return;

        if (other.tag == "Enemy")
            other.GetComponent<Enemy>().Health -= this.Damage;

        this.DestroyTimer_Elapsed();
    }
}
