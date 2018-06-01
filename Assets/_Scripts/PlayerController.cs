using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;
    public float fireRate;
    public SimpleTouchPad touchPad;
    public SimpleFireArea fireButton;
    public GameObject shot;
    public Transform shotSpawn;

    private AudioSource audioSource;
    private Rigidbody rb;
    private float nextFire;
    private readonly float zeroAsFloat = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

	void Update()
	{
        if (fireButton.CanFire() && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.Play();
        }
	}

	void FixedUpdate()
    {
        Vector2 direction = touchPad.GetDirection();
        Vector3 movement = new Vector3(direction.x, zeroAsFloat, direction.y);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                zeroAsFloat,
                Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(zeroAsFloat, zeroAsFloat, rb.velocity.x * -tilt);
    }
}