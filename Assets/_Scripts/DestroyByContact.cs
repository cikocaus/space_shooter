using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;

    private GameController gameController;

    readonly string boundary = "Boundary";
    readonly string player = "Player";

    private void Start()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameObject != null)
        {
            gameController = gameObject.GetComponent<GameController>();
        } else
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

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
            gameController.GameOver();
        }
        gameController.AddScore(scoreValue);
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
