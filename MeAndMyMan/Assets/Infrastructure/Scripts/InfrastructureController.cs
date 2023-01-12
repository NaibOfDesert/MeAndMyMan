using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InfrastructureController : MonoBehaviour
{
    [Header("Infrastructure")]
    [SerializeField] LayerMask infrastructureLayersToHit;
    public LayerMask InfrastructureLayersToHit { get { return infrastructureLayersToHit; } }

    Infrastructure infrastructure;
    public Infrastructure Infrastructure { get { return infrastructure; } set { infrastructure = value; } }

    GameController gameController; 

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();    

    }
/*
    bool BuildCheckField(Vector3 worldPositon)
    {
        var fieldCheck = gameController.BoardController.TilesList.Single(n => n.gameObject.transform.position == worldPositon);
        Debug.Log(fieldCheck.gameObject.transform.position);

        return (fieldCheck.Field.IsPlacable);

    }*/
    public void BuildInfrastructure(Vector3 worldPositon){
        Tile tileToBuild = gameController.BoardController.GetBoardTile(worldPositon); 

        if (tileToBuild.Field.IsPlacable)
        {
            tileToBuild.Field.IsPlacable = false;
            infrastructure.IsPlaced = true;
            infrastructure = null;
            gameController.IsBuildActive = false;
        }

    }


}

