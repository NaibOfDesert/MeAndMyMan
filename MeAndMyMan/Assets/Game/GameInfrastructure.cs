using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfrastructure : MonoBehaviour
{
    [Header("Infrastructure")]
    [SerializeField] LayerMask infrastructureLayersToHit;
    public LayerMask InfrastructureLayersToHit { get { return infrastructureLayersToHit; } }

    Infrastructure infrastructure;
    public Infrastructure Infrastructure { get { return infrastructure; } set { infrastructure = value; } }

    GameController gameController; 

    void Awake()
    {
        gameController = GetComponent<GameController>();   
    }

    public void BuildCheckField()
    {
        BuildInfrastructure();
    }
    public void BuildInfrastructure(){

        infrastructure.IsPlaced = true;
        infrastructure = null;
        gameController.IsBuildActive = false;
    }


}

