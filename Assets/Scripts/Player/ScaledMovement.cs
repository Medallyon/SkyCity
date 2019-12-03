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
    public Text AccelerationText;
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
            this.AccelerationText.text = $"{Mathf.Round(MapRangeClamped(-.5f, 1, -this.TopSpeed / 2, this.TopSpeed, this.Acceleration) * 100)}%";
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
        if (!Input.GetButton("Brake"))
            // Increment acceleration steadily
            this.Acceleration += Input.GetAxis("ForwardMovement") * (this.Speed * .01f);
        else
            // Lerp acceleration to 0 while holding brake
            this.Acceleration = Mathf.Lerp(this.Acceleration, 0f, Time.deltaTime);

        // Check before adding force to save calls to physics engine
        if (Input.GetAxis("SideMovement") != 0f)
            this.rb.AddForce(Input.GetAxis("SideMovement") * this.transform.right * this.Speed);
        if (Input.GetAxis("VerticalMovement") != 0f)
            this.rb.AddForce(Input.GetAxis("VerticalMovement") * this.transform.up * this.Speed);

        // Lerp Velocity, making the object catch up with the speed of 'Acceleration'
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
