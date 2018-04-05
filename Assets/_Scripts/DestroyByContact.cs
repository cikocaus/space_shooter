using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;

    readonly string boundary = "Boundary";
    readonly string player = "Player";

	void OnTriggerEnter(Collider other)
	{
        if(other.tag.Equals(boundary))
        {
            return;
        }
        InitExplosionAnimation();

        if (other.tag.Equals(player))
        {
            InitPlayerExplosionAnimation(other);
        }
        DestroyTriggeredObject(other);
	}

    void InitExplosionAnimation()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }

    void InitPlayerExplosionAnimation(Collider other)
    {
        Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
    }

    void DestroyTriggeredObject (Collider other)
    {
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
