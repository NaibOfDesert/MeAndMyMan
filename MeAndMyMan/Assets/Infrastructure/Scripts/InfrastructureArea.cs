using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InfrastructureArea : MonoBehaviour
{
    void Awake()
    {

    }
    public void EnableArea()
    {
        gameObject.SetActive(true); 

    }

    public void DisableArea()
    {
        gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        // change color of tiles aftes collision

        if (other.gameObject.GetComponentInParent<GameController>())
        {

        }
    }
}

