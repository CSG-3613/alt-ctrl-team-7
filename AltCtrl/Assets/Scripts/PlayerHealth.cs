using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public GameManager gm;

    [SerializeField] private int health;
    [SerializeField] private int maxHealth;

    private int obstacleLayer;
    
    // Start is called before the first frame update
    void Start()
    {
        obstacleLayer = LayerMask.NameToLayer("Obstacle");
    }

    private void OnCollisionEnter(Collision collision)
    {
        var otherLayer = collision.gameObject.layer;

        if(otherLayer == obstacleLayer)
        {
            //TODO: die.
        }
    }
}
