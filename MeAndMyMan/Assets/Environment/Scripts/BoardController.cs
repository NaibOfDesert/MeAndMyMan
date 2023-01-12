using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoardController : MonoBehaviour
{
    [SerializeField] Tile[] tilesList;
    public Tile[] TilesList { get { return tilesList; } }

    GameController gameController;

    void Awake()
    {
        tilesList = FindObjectsOfType<Tile>();
        BoardInitialization();
    }

    void Update()
    {
        
    }

    public void BoardInitialization()
    {
        
    }

    public Tile GetBoardTile(Vector3 worldPositon)
    {
        Tile fieldCheck = TilesList.Single(n => n.gameObject.transform.position == worldPositon);
        return (fieldCheck);
    }
}
