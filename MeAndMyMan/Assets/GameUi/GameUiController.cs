using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiController : MonoBehaviour
{
    [SerializeField] Infrastructure infrastructurePrefab; 

    
    public void Build()
    {
        infrastructurePrefab.Initiate(infrastructurePrefab); 
    }
}
