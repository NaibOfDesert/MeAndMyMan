using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoardController : MonoBehaviour
{
    [SerializeField] GameObject forestPrefab;
    [SerializeField] GameObject rockPrefab;


    [SerializeField] Material redMaterial;
    public Material RedMaterial { get { return redMaterial; } } 

    [SerializeField] Material greenMaterial;
    public Material GreenMaterial { get { return greenMaterial; } }

    [SerializeField] Material greyMaterial;
    public Material GreyMaterial { get { return greyMaterial; } }

    [SerializeField] Material yellowMaterial;
    public Material YellowMaterial { get { return yellowMaterial; } }

    [SerializeField] List<Tile> tilesList;
    public List<Tile> TilesList { get { return tilesList; } }

    GameController gameController;

    List<Tile> lastBoardAreaCheckList;
    public List<Tile> LastBoardAreaCheckList { get { return lastBoardAreaCheckList; ; } }

    List<float> environmentRotationsList; 
    List<float> environmentScaleList; // ?? is it needed? 



    void Awake()
    {
        tilesList = FindObjectsOfType<Tile>().ToList();
        lastBoardAreaCheckList = new List<Tile>();
        environmentRotationsList = new List<float>() { 0f, 90f, 180f, 270f };
      
        BoardInitialization(); 
        AbleBoardPlane();
        AbleBoardCoordinates();



    }

    void Update()
    {
        
    }

    public void BoardInitialization()
    {
        foreach (var tile in tilesList)
        {
            if(tile.Field.FieldType == FieldType.Rocks){
                tile.IsPlaceable = false;
                SetMaterialAsDisplaceable(tile);
                GenerateEnvironmentObject(tile.transform.position, rockPrefab);
            }
            else if (tile.Field.FieldType == FieldType.Forest)
            {
                tile.IsPlaceable = false;
                SetMaterialAsDisplaceable(tile);
                GenerateEnvironmentObject(tile.transform.position, forestPrefab); 
            }

        }
        // --
    }



    void GenerateEnvironmentObject(Vector3 position, GameObject environmentPrefab)
    {
        int environmentRotation = Random.Range(0, environmentRotationsList.Count() - 1);

        Quaternion environmentQuaterion = Quaternion.Euler(Quaternion.identity.x, environmentRotation, Quaternion.identity.z);

        Instantiate(environmentPrefab, position, environmentQuaterion);
    }

    public Tile GetBoardTile(Vector3 worldPositon)
    {
        Tile fieldCheck = tilesList.SingleOrDefault(n => n.gameObject.transform.position == worldPositon);
        Debug.Log(tilesList.Count()); 
        return (fieldCheck);
    }

    public void AbleBoardPlane()
    {
        foreach (var tile in tilesList)
        {
            tile.AbleMeshRenderer();
        }
    }

    public void AbleBoardCoordinates()
    {
        foreach (var tile in tilesList)
        {
            tile.GetComponentInChildren<TileCoordinates>().AbleCoordinates(); 
        }
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

    public List<Tile> BoardAreaCheck(Vector3 worldPosition, int infrastructureSize, int infrastructureArea)
    {
        List<Tile> boardAreaCheckList = new List<Tile>(); 


        for (int i = -infrastructureArea; i <= (infrastructureArea + infrastructureSize -1); i++)
        {
            for (int j = -infrastructureArea; j <= (infrastructureArea + infrastructureSize - 1); j++)
            {
                Vector3 tilePositon = worldPosition;
                tilePositon += new Vector3(i, 0, j);
                Tile tileToCheck = GetBoardTile(tilePositon);
                
                if (tileToCheck != null && tileToCheck.IsPlaceable && !tileToCheck.UsedByInfrastructureArea)
                {
                    boardAreaCheckList.Add(tileToCheck);
                    SetMaterialAsPlaceable(tileToCheck);
                }
            }
        }


        // to add cheeck if area is palceable but used by field for infrastrcutre=> yellow color
        // is area is used by field fora area => gery 

        BoardAreaClear(boardAreaCheckList);

        lastBoardAreaCheckList = boardAreaCheckList;
        return boardAreaCheckList; 
    }

    public void BoardAreaClear(List<Tile> boardArea)
    {
        foreach (var tile in lastBoardAreaCheckList)
        {
            if (!boardArea.Contains(tile))
            {
                SetMaterialAsDisplaceable(tile);
            }
        } 
    }

    public void BoardAreaSetAsUsedByInfrastructure(List<Tile> boardAreaUsedByInfrastructure, Infrastructure infrastructure)
    {
        foreach(var tile in boardAreaUsedByInfrastructure)
        {
            tile.IsPlaceable = false;
            tile.UsedByInfrastructure = infrastructure;


        }
    }

    public void BoardAreaSetAsUsedByInfrastructureArea(List<Tile> boardAreaUsedByInfrastructureArea, Infrastructure infrastructure)
    {
        foreach (var tile in boardAreaUsedByInfrastructureArea)
        {
            tile.UsedByInfrastructure = infrastructure; 
            tile.UsedByInfrastructureArea = true; 
        }
    }

    public void SetMaterialAsPlaceable(Tile tileToSetMaterial)
    {
        MeshRenderer meshRenderer = tileToSetMaterial.GetComponentInChildren<TilePlane>().TileMesh; 

        if (tileToSetMaterial.IsPlaceable)
        {
            meshRenderer.material = greenMaterial;
        }
    }

    public void SetMaterialAsDisplaceable(Tile tileToSetMaterial)
    {
        MeshRenderer meshRenderer = tileToSetMaterial.GetComponentInChildren<TilePlane>().TileMesh;
        if (tileToSetMaterial.IsPlaceable)
        {
            meshRenderer.material = redMaterial;
        }
        else 
        {
            meshRenderer.material = greyMaterial;
        }
    }


}
