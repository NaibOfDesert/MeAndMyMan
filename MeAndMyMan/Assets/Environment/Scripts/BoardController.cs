using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoardController : MonoBehaviour
{
    [SerializeField] Material redMaterial;
    public Material RedMaterial { get { return redMaterial; } } //--

    [SerializeField] Material greenMaterial;
    public Material GreenMaterial { get { return greenMaterial; } } //--

    [SerializeField] Material greyMaterial;
    public Material GreyMaterial { get { return greyMaterial; } } //--

    [SerializeField] List<Tile> tilesList;
    public List<Tile> TilesList { get { return tilesList; } }

    GameController gameController;

    List<Tile> lastBoardAreaCheckList;

    void Awake()
    {
        
        tilesList = FindObjectsOfType<Tile>().ToList(); // change to List
        BoardInitialization();

        lastBoardAreaCheckList = new List<Tile>();
    }

    void Update()
    {
        
    }

    public void BoardInitialization()
    {
        
        // --
    }

    public Tile GetBoardTile(Vector3 worldPositon)
    {
        Tile fieldCheck = TilesList.SingleOrDefault(n => n.gameObject.transform.position == worldPositon);
        return (fieldCheck);
    }

    bool BoardCheck()
    {
        return true;
    }

    public void BoardAreaCheck(Vector3 worldPosition, int infrastructureSize, int infrastructureArea)
    {
        List<Tile> boardAreaCheckList = new List<Tile>(); 


        //++ add dependece od infrastructure size
        for (int i = -infrastructureArea; i <= infrastructureArea; i++)
        {
            for (int j = -infrastructureArea; j <= infrastructureArea; j++)
            // tilePositon += new Vector3(i, 0, i);
            {
                Vector3 tilePositon = worldPosition;
                tilePositon += new Vector3(i, 0, j);
                Tile tileToCheck = GetBoardTile(tilePositon);
                
                if (tileToCheck != null)
                {
                    boardAreaCheckList.Add(tileToCheck);
                    SetMaterial(tileToCheck);
                }
            }
        }

        foreach(var tile in lastBoardAreaCheckList)
        {
            if (!boardAreaCheckList.Contains(tile))
            {
                SetMaterialAsDisable(tile); 
            }
        }

        lastBoardAreaCheckList = boardAreaCheckList;
    }


    void SetMaterial(Tile tileToSetMaterial)
    {
        MeshRenderer meshRenderer = tileToSetMaterial.GetComponentInChildren<TilePlane>().TileMesh; // >=> 

        if (tileToSetMaterial.Field.IsPlacable)
        {
            meshRenderer.material = greenMaterial;
        }
        else
        {
            meshRenderer.material = redMaterial;
        }
        // to expand
     }

    void SetMaterialAsDisable(Tile tileToSetMaterial)
    {
        MeshRenderer meshRenderer = tileToSetMaterial.GetComponentInChildren<TilePlane>().TileMesh;


        meshRenderer.material = redMaterial;

        // to expand
    }
}
