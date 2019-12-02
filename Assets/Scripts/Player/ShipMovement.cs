using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float Speed = 10f;
    public float TopSpeed = 1000f;
    public float RotationSpeed = 5f;

    private float acceleration;
    private Rigidbody rb;

    private Vector3 direction;
    private Vector3 eulerRotation = new Vector3();
   
    void Start()
    {
        this.acceleration = this.Speed;
        this.rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Forward / Backward
        this.direction = this.transform.forward * Input.GetAxis("ForwardMovement");
        // Port / Starboard
        this.direction += this.transform.right * Input.GetAxis("SideMovement");
        // Ascension / Descension
        this.direction += this.transform.up * Input.GetAxis("VerticalMovement");

        // Boost the movement speed up to the Top Speed
        if (Input.GetButton("Boost"))
            this.acceleration = Mathf.Lerp(this.acceleration, this.TopSpeed, Time.deltaTime);

        // Rotation Lerp
        this.eulerRotation.y = Mathf.Lerp(this.eulerRotation.y, Input.GetAxis("Rotation") * this.RotationSpeed * 2f, Time.deltaTime);
    }

    void FixedUpdate()
    {
        // Move Ship
        var force = this.direction * this.acceleration;
        this.rb.AddForce(force * Time.deltaTime * (Input.GetButton("Boost") ? 3f : 1f), ForceMode.Acceleration);

        // Rotate Ship
        var deltaRotation = Quaternion.Euler(this.eulerRotation * Time.deltaTime);
        this.rb.MoveRotation(this.rb.rotation * deltaRotation);
    }
}
