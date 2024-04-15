using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelMovement : MonoBehaviour
{
    public float velocity = 2f;
    public float endTunnelZ = -40;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = gameObject.transform.position + (Vector3.back)*velocity*Time.deltaTime;
        Debug.Log(gameObject.transform.position.z);
        if (this.gameObject.transform.position.z<=endTunnelZ)
        {
            Destroy(gameObject);
        }
    }
}
