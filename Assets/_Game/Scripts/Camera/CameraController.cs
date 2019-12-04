using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class CameraController : MonoBehaviour
{
    public GameObject FollowTarget;
    public float distance = 8f;
    public float xOffset = 0f;
    public float yOffset = 0f;

    public float xSpeed = 60f;
    public float ySpeed = 60f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = 2f;
    public float distanceMax = 15f;

    private new Rigidbody rigidbody;

    public CursorLockMode cursorMode = CursorLockMode.Locked;

    float x = 0f;
    float y = 0f;

    void Start()
    {
        Cursor.lockState = this.cursorMode;
        // Hide cursor when locked
        Cursor.visible = (CursorLockMode.Locked != this.cursorMode);

        Vector3 angles = this.transform.eulerAngles;
        this.x = angles.y;
        this.y = angles.x;

        this.rigidbody = this.GetComponent<Rigidbody>();

        if (this.rigidbody != null)
            this.rigidbody.freezeRotation = true;
    }

    void LateUpdate()
    {
        if (!this.FollowTarget)
            return;

        var movement = this.FollowTarget.GetComponent<ScaledMovement>();
        if (movement != null && movement.inputLocked)
            return;

        this.x += Input.GetAxis("Mouse X") * this.xSpeed * this.distance * 0.02f;
        this.y -= Input.GetAxis("Mouse Y") * this.ySpeed * 0.02f;

        this.y = CameraController.ClampAngle(this.y, this.yMinLimit, this.yMaxLimit);

        Quaternion rot = Quaternion.Euler(this.y, this.x, 0);
        this.distance = Mathf.Clamp(this.distance - Input.GetAxis("Mouse ScrollWheel") * 5, this.distanceMin, this.distanceMax);

        RaycastHit hit;
        if (Physics.Linecast(this.FollowTarget.transform.position, this.transform.position, out hit))
            this.distance -= hit.distance;

        Vector3 negDist = new Vector3(0f, 0f, -this.distance);
        Vector3 pos = rot * negDist + this.FollowTarget.transform.position + new Vector3(this.xOffset, this.yOffset);

        this.transform.rotation = rot;
        this.transform.position = pos;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
