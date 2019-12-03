using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaledMovement : MonoBehaviour
{
    public float Speed = 10f;
    public float TopSpeed = 80f;

    private float acceleration = 0f;
    private float Acceleration
    {
        get
        {
            return this.acceleration;
        }
        set
        {
            // Limit acceleration with min and max bounds based on public TopSpeed
            this.acceleration = Mathf.Max(-this.TopSpeed / 2, Mathf.Min(value, this.TopSpeed));
        }
    }

    private Rigidbody rb;
    private Vector3 eulerRotation;

    void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        this.Acceleration += Input.GetAxis("ForwardMovement") * (this.Speed * .01f);
        if (Input.GetButton("Brake"))
            this.Acceleration = 0f;

        this.rb.AddForce(Input.GetAxis("SideMovement") * this.transform.right * this.Speed);
        this.rb.AddForce(Input.GetAxis("VerticalMovement") * this.transform.up * this.Speed);

        this.rb.velocity = Vector3.Lerp(this.rb.velocity, this.transform.forward * this.Acceleration, Time.deltaTime / 2);

        Debug.Log($"{this.Acceleration}, {this.rb.velocity}");

        // Rotation Lerp
        this.eulerRotation.y = Mathf.Lerp(this.eulerRotation.y, Input.GetAxis("Rotation") * 100f, Time.deltaTime);

        // Rotate Ship
        var deltaRotation = Quaternion.Euler(this.eulerRotation * Time.deltaTime);
        this.rb.MoveRotation(this.rb.rotation * deltaRotation);
    }
}
