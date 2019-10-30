using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float Speed = 1f;
    public float RotationSpeed = 1f;

    private float acceleration;
    private Vector3 direction;
    private Vector3 eulerRotation = new Vector3();

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
        // Forward / Backward
        this.direction = this.transform.forward * Input.GetAxis("ForwardMovement");
        // Port / Starboard
        this.direction += this.transform.right * Input.GetAxis("SideMovement");
        // Up / Down
        this.direction += this.transform.up * Input.GetAxis("VerticalMovement");

        // Rotation
        this.eulerRotation.y = Input.GetAxis("Rotation") * this.RotationSpeed * 2f;
    }

    void FixedUpdate()
    {
        // Move Ship
        var force = this.direction * this.acceleration;
        this.rb.AddForce(force * Time.deltaTime);

        // Rotate Ship
        var deltaRotation = Quaternion.Euler(this.eulerRotation * Time.deltaTime);
        this.rb.MoveRotation(this.rb.rotation * deltaRotation);
    }
}
