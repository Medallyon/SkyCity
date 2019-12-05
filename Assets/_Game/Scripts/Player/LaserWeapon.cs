using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : MonoBehaviour
{
    public List<GameObject> LaserPrefabs = new List<GameObject>();
    public List<AudioClip> ShootingSounds = new List<AudioClip>();

    [Header("Behaviour")]
    [Tooltip("How often the laser shoots, in seconds")]
    public float Cooldown = 1f;
    [Tooltip("The Y offset for the laser's spawn position")]
    public float YOffset = 0f;
    [Tooltip("Should two lasers be used?")]
    public bool DoubleLasers = true;
    [Tooltip("The offset for how far apart each laser is from each other")]
    public float DoubleLaserOffset = 2f;

    private GameObject DefaultAnchor;
    private GameObject LeftAnchor;
    private GameObject RightAnchor;

    private float cd = 0f;
    private bool onCooldown = false;
    private bool shotLeft = false;

    private AudioSource laserSource;

    // Start is called before the first frame update
    void Start()
    {
        this.laserSource = this.GetComponents<AudioSource>()[1];

        if (this.DoubleLasers)
        {
            this.LeftAnchor = Instantiate(new GameObject("Left Laser"), this.transform.position + new Vector3(2.84f, 0.58f, 1.382f), new Quaternion(), this.transform);
            this.RightAnchor = Instantiate(new GameObject("Right Laser"), this.transform.position + new Vector3(-2.84f, 0.58f, 1.382f), new Quaternion(), this.transform);
        }

        else
        {
            this.DefaultAnchor = Instantiate(new GameObject("Laser"), this.transform.position + new Vector3(0f, .6f, 3), new Quaternion(), this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.cd += Time.deltaTime;
        if (this.cd >= this.Cooldown)
        {
            this.cd = 0;
            this.onCooldown = false;
        }

        if (!Input.GetButton("Shoot") || this.onCooldown)
            return;

        var movement = GameObject.FindGameObjectWithTag("Player").GetComponent<ScaledMovement>();
        if (movement != null && movement.inputLocked)
            return;

        this.onCooldown = true;
        if (this.DoubleLasers)
            this.shotLeft = !this.shotLeft;

        this.Shoot();
    }

    GameObject Shoot()
    {
        this.laserSource.clip = this.ShootingSounds[Random.Range(0, this.ShootingSounds.Count)];
        this.laserSource.Play();

        GameObject spawnAnchor = this.DefaultAnchor;
        if (this.DoubleLasers)
            spawnAnchor = (this.shotLeft) ? this.RightAnchor : this.LeftAnchor;

        return Instantiate(this.LaserPrefabs[Random.Range(0, this.LaserPrefabs.Count)], spawnAnchor.transform.position, Quaternion.LookRotation(this.transform.up, this.transform.forward));
    }
}
