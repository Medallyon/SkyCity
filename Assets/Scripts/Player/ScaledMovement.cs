using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaledMovement : MonoBehaviour
{
    static float MapRangeClamped(float from1, float to1, float from2, float to2, float value)
    {
        if (value <= from2)
            return from1;
        if (value >= to2)
            return to1;
        return (to1 - from1) * ((value - from2) / (to2 - from2)) + from1;
    }

    [Header("Movement")]
    public float Speed = 10f;
    public float TopSpeed = 80f;
    [Tooltip("The amount you can boost before running out of charge")]
    public float BoostCharge = 100f;
    [Tooltip("The time it takes before being able to boost again after a boost (in seconds)")]
    public float BoostCooldown = 1f;

    [Header("UI")]
    public Text AccelerationText;
    public Slider Accelerometer;
    public Slider BoostMeter;

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

    private bool canRecharge = true;
    private float boost = 0f;
    public float Boost
    {
        get
        {
            return this.boost;
        }
        set
        {
            this.boost = Mathf.Max(0, Mathf.Min(value, this.BoostCharge));
            this.BoostMeter.value = MapRangeClamped(0, 1, 0, this.BoostCharge, this.Boost);
        }
    }

    private Rigidbody rb;
    private Vector3 eulerRotation;

    void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
        this.Boost = this.BoostCharge;
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Boost"))
        {
            this.canRecharge = false;
            CancelInvoke("ResetBoost");

            if (--this.Boost > 0)
                this.rb.AddForce(this.transform.forward * this.Speed * 3);
        }
        
        else
        {
            if (this.canRecharge)
                this.Boost++;
            else
                Invoke("ResetBoost", this.BoostCooldown);
        }

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

    void ResetBoost()
    {
        this.canRecharge = true;
    }
}
