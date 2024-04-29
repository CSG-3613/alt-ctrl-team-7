using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameManager gm;

    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    private bool invincible;

    private int obstacleLayer;
    
    // Start is called before the first frame update
    void Start()
    {
        obstacleLayer = LayerMask.NameToLayer("Obstacle");

        gm = GameManager.instance;
        invincible = false;
    }

    //private void FixedUpdate()
    //{
    //    StartCoroutine(DamageCooldown());
    //}

    private void OnTriggerEnter(Collider other)
    {
        var otherLayer = other.gameObject.layer;

        if(otherLayer == obstacleLayer && !invincible)
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

    IEnumerator DamageCooldown()
    {
        new WaitForSeconds(2);


        yield return null;
    }
}
