using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaledMovement : MonoBehaviour
{
    public float Speed = 10f;
    public float TopSpeed = 80f;

    [Header("UI")]
    public Slider Accelerometer;
    public Image VelocityFill;

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
            this.Accelerometer.value = MapRangeClamped(0, 1, -this.TopSpeed / 2, this.TopSpeed, this.Acceleration);
        }
    }

    private Rigidbody rb;
    private Vector3 eulerRotation;

    void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // TODO: Reference slider fill image and sync it with the actual current velocity
        //this.
    }

    void FixedUpdate()
    {
        this.Acceleration += Input.GetAxis("ForwardMovement") * (this.Speed * .01f);
        if (Input.GetButton("Brake"))
            this.Acceleration = 0f;

        this.rb.AddForce(Input.GetAxis("SideMovement") * this.transform.right * this.Speed);
        this.rb.AddForce(Input.GetAxis("VerticalMovement") * this.transform.up * this.Speed);

        this.rb.velocity = Vector3.Lerp(this.rb.velocity, this.transform.forward * this.Acceleration, Time.deltaTime / 2);

        // Rotation Lerp
        this.eulerRotation.y = Mathf.Lerp(this.eulerRotation.y, Input.GetAxis("Rotation") * 100f, Time.deltaTime);

        // Rotate Ship
        var deltaRotation = Quaternion.Euler(this.eulerRotation * Time.deltaTime);
        this.rb.MoveRotation(this.rb.rotation * deltaRotation);
    }

    static float MapRangeClamped(float from1, float to1, float from2, float to2, float value)
    {
        if (value <= from2)
            return from1;
        if (value >= to2)
            return to1;
        return (to1 - from1) * ((value - from2) / (to2 - from2)) + from1;
    }
}
