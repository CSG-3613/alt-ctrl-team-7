using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class DeleteCollider : MonoBehaviour
{
    [ExecuteInEditMode]
    void RemoveColliders()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
                var collider = this.gameObject.transform.GetChild(i).gameObject.GetComponent<Collider>();
                if (collider != null)
                {
                    Destroy(collider);
                }
            }
        }
    }
}
