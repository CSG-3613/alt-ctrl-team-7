using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TunnelManager : MonoBehaviour
{

    public static TunnelManager TMinstance { get; private set; }

    private static GameManager gm;

    [SerializeField] private float velocity;
    [SerializeField] private GameObject LastTunnel;

    [Header("Planet Tunnel/Objects")]
    //index of the current planet, used to swap what set of arrays are being used for obsticle and tunnel generation
    [SerializeField] private int _currentPlanetIndex;
    //arrays for all the different planets
    [SerializeField] private GameObject[] _Planet1TunnelPrefabs;
    [SerializeField] private GameObject[] _Planet1ObstaclePrefabs;
    [SerializeField] private GameObject[] _Planet2TunnelPrefabs;
    [SerializeField] private GameObject[] _Planet2ObstaclePrefabs;
    //chance to spawn an obsticle
    [SerializeField] private float chanceObstacle = 0.5f;

    //2D array initialization needs a static int value, This cannot be seen in the inspector
    private static int _numberOfPlanets = 2;

    //2D Arrays Cannot be seen in inspector! Must be assigned in code!
    private GameObject[][] tunnelPrefabs = new GameObject[_numberOfPlanets][];
    private GameObject[][] ObstaclePrefabs = new GameObject[_numberOfPlanets][];

    private void Awake()
    {
        // Ensure this is the only instance. If not, destroy self.
        if (TMinstance != null && TMinstance != this) { Destroy(this); }
        else { TMinstance = this; }
        //Check that Last tunnel has been assigned
        if (LastTunnel == null) { Debug.LogError("Assign Last tunnel in inspector!!!"); }

        //Assigning the values of the 2d Array
        Initialize2DArrays();

    }

    void Start()
    {
        //get reference to GameManager
        gm = GameManager.instance;
    }

    void FixedUpdate()
    {
        velocity = gm.getCurrSpeed();
    }

    public void SpawnTunnel(GameObject _TunnelToDelete)
    {
        GameObject nextTunnel = Instantiate(
            tunnelPrefabs[_currentPlanetIndex][Random.Range(0, tunnelPrefabs[_currentPlanetIndex].Length)], //Gameobject to instantiate
            LastTunnel.transform.position + (5 - Time.deltaTime * velocity) * Vector3.forward, //TunnelPosition
            LastTunnel.transform.rotation); //Tunnel Rotation

        if (Random.value < chanceObstacle)
        {
            Instantiate(
                ObstaclePrefabs[_currentPlanetIndex][Random.Range(0, ObstaclePrefabs[_currentPlanetIndex].Length)], //Gameobject to instantiate
                LastTunnel.transform.position + new Vector3(-2.5f, -4f, 0) //center reference point
                + Random.Range(-5.0f, 5.0f) * Vector3.right + Random.Range(-5.0f, 5.0f) * Vector3.up, //repositioning logic
                LastTunnel.transform.rotation);
        }
        LastTunnel = nextTunnel;
        //Destroy the tunnel that called the spawner
        Destroy(_TunnelToDelete);
    }

    private void Initialize2DArrays()
    {
        //Assigning the values of the 2d Array
        //Must have one for each number of planets!!
        tunnelPrefabs[0] = _Planet1TunnelPrefabs;
        ObstaclePrefabs[0] = _Planet1ObstaclePrefabs;
        tunnelPrefabs[1] = _Planet2TunnelPrefabs;
        ObstaclePrefabs[1] = _Planet2ObstaclePrefabs;
    }
}
