using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;

    private GameController gameController;

    readonly string boundary = "Boundary";
    readonly string player = "Player";
    readonly string enemy = "Enemy";

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
        if (other.CompareTag(boundary) || other.CompareTag(enemy))
        {
            return;
        }

        if(explosion != null)
        {
            InitExplosionAnimation();
        }
       
        if (other.CompareTag(player))
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
