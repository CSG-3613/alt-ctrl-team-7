using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameManager gm;

    [SerializeField] private int health;
    [SerializeField] private int maxHealth;

    private int obstacleLayer;
    
    // Start is called before the first frame update
    void Start()
    {
        obstacleLayer = LayerMask.NameToLayer("Obstacle");

        gm = GameManager.instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var otherLayer = collision.gameObject.layer;

        if(otherLayer == obstacleLayer)
        {
            print("Obstacle collision detected");

            health -= 1;
            if (health <= 0)
            {
                gm.gameOver();
            }
            //TODO: die.
        }
    }
}
