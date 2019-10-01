using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float Speed = 1;

    private float acceleration;
    private Vector3 direction;

    private Rigidbody rb;
   
    // Start is called before the first frame update
    void Start()
    {
        this.acceleration = this.Speed * 10;
        this.rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        this.direction = this.transform.forward * Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space))
            this.direction.y = 1;
        else if (Input.GetKey(KeyCode.LeftShift))
            this.direction.y = -1;
        else
            this.direction.y = 0;

        // todo: insert rotation logic
    }

    void FixedUpdate()
    {
        var force = this.direction * this.acceleration;
        this.rb.AddForce(force * Time.deltaTime);
        //this.rb.MoveRotation();

        Debug.Log(this.rb.velocity);
    }
}
