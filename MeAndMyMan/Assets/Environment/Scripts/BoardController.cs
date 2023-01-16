using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoardController : MonoBehaviour
{
    [SerializeField] GameObject forestPrefab;

    [SerializeField] Material redMaterial;
    public Material RedMaterial { get { return redMaterial; } } 

    [SerializeField] Material greenMaterial;
    public Material GreenMaterial { get { return greenMaterial; } }

    [SerializeField] Material greyMaterial;
    public Material GreyMaterial { get { return greyMaterial; } } 

    [SerializeField] List<Tile> tilesList;
    public List<Tile> TilesList { get { return tilesList; } }

    GameController gameController;

    List<Tile> lastBoardAreaCheckList;
    public List<Tile> LastBoardAreaCheckList { get { return lastBoardAreaCheckList; ; } }

    List<float> environmentRotationsList;

    void Awake()
    {
        tilesList = FindObjectsOfType<Tile>().ToList();
        lastBoardAreaCheckList = new List<Tile>();
        environmentRotationsList = new List<float>() { 0f, 90f, 180f, 270f };
      
        BoardInitialization(); 
        AbleBoardPlane();



    }

    void Update()
    {
        
    }

    public void BoardInitialization()
    {
        foreach (var tile in tilesList)
        {
            if(tile.Field.FieldType == FieldType.Rocks){
                tile.IsPlacable = false;
                SetMaterialAsDisable(tile);
                // generate rock
            }
            else if (tile.Field.FieldType == FieldType.Forest)
            {
                tile.IsPlacable = false;
                SetMaterialAsDisable(tile);
                GenerateForest(tile.transform.position); 
            }

        }
        // --
    }

    public Tile GetBoardTile(Vector3 worldPositon)
    {
        Tile fieldCheck = tilesList.SingleOrDefault(n => n.gameObject.transform.position == worldPositon);
        Debug.Log(tilesList.Count()); 
        return (fieldCheck);
    }

    public List<Tile> BoardCheck(Vector3 worldPosition, int infrastructureSize)
    {
        List<Tile> boardCheckList = new List<Tile>();
        for(int i = 0; i < infrastructureSize; i++)
        {
            for (int j = 0; j < infrastructureSize; j++)
            {
                Vector3 tilePosition = new Vector3(i, 0, j);
                Tile tileCheck = GetBoardTile(worldPosition + tilePosition);

                if(tileCheck != null)
                {
                    boardCheckList.Add(tileCheck);
                }
            }
        }

        return boardCheckList;
    }

    public void BoardAreaCheck(Vector3 worldPosition, int infrastructureSize, int infrastructureArea)
    {
        List<Tile> boardAreaCheckList = new List<Tile>(); 


        for (int i = -infrastructureArea; i <= (infrastructureArea + infrastructureSize -1); i++)
        {
            for (int j = -infrastructureArea; j <= (infrastructureArea + infrastructureSize - 1); j++)
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

    public void BoardAreaClear()
    {
        foreach (var tile in lastBoardAreaCheckList)
        {
            SetMaterialAsDisable(tile);
        }
        lastBoardAreaCheckList.Clear(); 
    }
    public void SetMaterial(Tile tileToSetMaterial)
    {
        MeshRenderer meshRenderer = tileToSetMaterial.GetComponentInChildren<TilePlane>().TileMesh; 

        if (tileToSetMaterial.IsPlacable)
        {
            meshRenderer.material = greenMaterial;
        }
        else
        {
            meshRenderer.material = redMaterial;
        }
        // to expand
    }

    public void SetMaterialAsDisable(Tile tileToSetMaterial)
    {
        MeshRenderer meshRenderer = tileToSetMaterial.GetComponentInChildren<TilePlane>().TileMesh;
        if (tileToSetMaterial.IsPlacable)
        {
            meshRenderer.material = redMaterial;
        }
        /*else
        {
            meshRenderer.material = greyMaterial;
        }*/

        // to expand
    }

    public void AbleBoardPlane()
    {
        foreach (var tile in tilesList)
        {
            tile.GetComponentInChildren<MeshRenderer>().enabled = !tile.GetComponentInChildren<MeshRenderer>().enabled;
        }
    }

    void GenerateForest(Vector3 position)
    {
        int environmentRotation = Random.Range(0, environmentRotationsList.Count() - 1);

        Quaternion environmentQuaterion = Quaternion.Euler(Quaternion.identity.x, environmentRotation, Quaternion.identity.z);

        Instantiate(forestPrefab, position, environmentQuaterion);
    }
}
