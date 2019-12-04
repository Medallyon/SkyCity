using UnityEngine;

public class FloatingScraper : MonoBehaviour
{
    static float MapRangeClamped(float from1, float to1, float from2, float to2, float value)
    {
        if (value <= from2)
            return from1;
        if (value >= to2)
            return to1;
        return (to1 - from1) * ((value - from2) / (to2 - from2)) + from1;
    }

    // animate the game object from -15 to +15 and back
    public float minimum = -30f;
    public float maximum = 30f;
    public float Speed = 1;

    // starting value for the Lerp
    private float t = 0.0f;

    void Start()
    {
        // Determine the base min / max positions for this object
        this.minimum = this.transform.position.y + this.minimum;
        this.maximum = this.transform.position.y + this.maximum;

        // Set a random alpha value (starting point) for this object
        this.t = Random.value;
        this.transform.position += new Vector3(0, this.transform.position.y + MapRangeClamped(0, 1, this.minimum, this.maximum, this.t), 0);
        if (Random.value > .5f)
            this.SwapDirection();
    }

    // Quite performance-heavy if called on hundreds of objects
    void Update()
    {
        // animate the position of the game object...
        transform.position = new Vector3(this.transform.position.x, Mathf.Lerp(minimum, maximum, this.t), this.transform.position.z);

        // .. and increase the t interpolater
        this.t += 0.1f * Time.deltaTime * this.Speed;

        if (this.t > 1.0f)
            this.SwapDirection();
    }

    void SwapDirection()
    {
        // swap maximum and minimum so game object moves
        // in the opposite direction.
        float temp = this.maximum;
        this.maximum = this.minimum;
        this.minimum = temp;
        this.t = 0f;
    }
}
