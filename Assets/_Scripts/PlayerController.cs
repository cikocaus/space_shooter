using UnityEngine;
using System.Collections;

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

    public GameObject shot;
    public Transform shotSpawn;

    Rigidbody rb;
    float nextFire;
    readonly float zeroAsFloat = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

	void Update()
	{
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
	}

	void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, zeroAsFloat, moveVertical);
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