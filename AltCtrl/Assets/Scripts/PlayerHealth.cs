using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private GameManager gm;

    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private GameObject explosionFX;
    [SerializeField] private GameObject[] HealthUIHearts;
    [SerializeField] private FMODUnity.EventReference crashEvent;

    private ParticleSystem explosionParticle;
    private bool invincible;
    private int obstacleLayer;
    private int countdown = 120;

    // Start is called before the first frame update
    void Start()
    {
        //retrieve explosionFX main module and set callback for use when player dies
        explosionParticle = explosionFX.GetComponent<ParticleSystem>();
        var main = explosionParticle.main;
        main.stopAction = ParticleSystemStopAction.Callback;

        //set layer to check collisions on
        obstacleLayer = LayerMask.NameToLayer("Obstacle");

        //fetch gamemanager and initialize health
        gm = GameManager.instance;
        invincible = false;

        health = maxHealth;
        Time.timeScale = 1;
    }

    private void FixedUpdate()
    {
        if (countdown > 0)
        {
            countdown--;
            if (countdown <= 0)
            {
                invincible = false;
                print("damage cooldown ended");
            }
            else
            {
                invincible = true;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        var otherLayer = other.gameObject.layer;

        Debug.Log(other.gameObject.layer);

        if (otherLayer == obstacleLayer && !invincible)
        {
            print("Obstacle collision detected");
            health -= 1;
            HealthUIHearts[health].SetActive(false);
            FMODUnity.RuntimeManager.PlayOneShot(crashEvent, transform.position);
            countdown = 180;
            if (health <= 0)
            {
                explosionParticle.Play();
                GetComponent<MeshRenderer>().enabled = false;
                gm.gameOver();
            }
            //TODO: die.
        }
    }

    private void OnParticleSystemStopped()
    {
        gameObject.SetActive(false);
    }

    /*
    IEnumerator DamageCooldown()
    {
        new WaitForSeconds(2);


        yield return null;
    }
    */
}
