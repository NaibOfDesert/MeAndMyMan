using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoardController : MonoBehaviour
{
    [SerializeField] GameObject forestPrefab;
    [SerializeField] GameObject rockPrefab;


    [SerializeField] Material redMaterial;
    public Material RedMaterial { get { return redMaterial; } } //-- 

    [SerializeField] Material greenMaterial;
    public Material GreenMaterial { get { return greenMaterial; } } //--

    [SerializeField] Material blueMaterial;
    public Material BlueMaterial { get { return blueMaterial; } } //--

    [SerializeField] Material greyMaterial;
    public Material GreyMaterial { get { return greyMaterial; } } //--

    [SerializeField] Material yellowMaterial;
    public Material YellowMaterial { get { return yellowMaterial; } } //--

    [SerializeField] List<Tile> tilesList;
    public List<Tile> TilesList { get { return tilesList; } }

    GameController gameController;

    List<Tile> lastBoardList;
    public List<Tile> LastBoardList { get { return lastBoardList; ; } }

    List<Tile> lastBoardAreaList;
    public List<Tile> LastBoardAreaList { get { return lastBoardAreaList; ; } }

    List<Tile> rockList;
    List<Tile> forestList;

    List<float> environmentRotationsList; 
    List<float> environmentScaleList; // ?? is it needed? 



    void Awake()
    {
        tilesList = FindObjectsOfType<Tile>().ToList();
        lastBoardList = new List<Tile>();
        lastBoardAreaList = new List<Tile>();
        rockList = new List<Tile>(); ;

        forestList = new List<Tile>(); ;
        environmentRotationsList = new List<float>() { 0f, 90f, 180f, 270f };
      
        BoardInitialization(); 
        AbleBoardPlane();
        AbleBoardCoordinates();



    }

    void Update()
    {
        
    }

    public void BoardInitialization() //++ random location of environment elements
    {
        foreach (var tile in tilesList)
        {
            if(tile.Field.FieldType == FieldType.Rocks){
                tile.IsUsedByInfrastructure = true;
                SetMaterialDefault(tile);
                GenerateEnvironmentObject(tile.transform.position, rockPrefab);
                rockList.Add(tile);
            }
            else if (tile.Field.FieldType == FieldType.Forest)
            {
                tile.IsUsedByInfrastructure = true;
                SetMaterialDefault(tile);
                GenerateEnvironmentObject(tile.transform.position, forestPrefab);
                forestList.Add(tile);
            }
        }
    }

    void GenerateEnvironmentObject(Vector3 position, GameObject environmentPrefab) //++ random location of environment elements
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

    public void AbleInfrastructurePlane(Infrastructure infrastructure)
    {
        foreach (var tile in infrastructure.InfrastructureArea.BoardList)
        {
            tile.AbleMeshRenderer();
        }

        foreach (var tile in infrastructure.InfrastructureArea.BoardAreaList)
        {
            tile.AbleMeshRenderer();
        }

        foreach (var tile in infrastructure.InfrastructureArea.BoardAreaBlockedList)
        {
            tile.AbleMeshRenderer();
            // SetMaterialBlocked(tile);
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
                Tile tileToCheck = GetBoardTile(worldPosition + tilePosition);

                if(tileToCheck != null)
                {
                    boardCheckList.Add(tileToCheck);
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

                if (tileToCheck != null && !lastBoardList.Contains(tileToCheck))
                {
                    boardAreaCheckList.Add(tileToCheck);
                }
            }
        }
        return boardAreaCheckList; 
    }


    public void BoardAreaCheckUsedBy(List<Tile> boardList, Infrastructure infrastructure)

    {
        foreach (var tile in boardList)
        {
            if (tile.IsUsedByInfrastructureArea)
            {
                tile.UsedByInfrastructure.InfrastructureArea.SetAreaAsBlocked(tile); 
            }
        }
    }


    public void BoardClear(List<Tile> boardList)
    {
        SetMaterialForListDefault(lastBoardList);
        SetMaterialForListUpdate(boardList);
        lastBoardList = boardList;
    }

    public void BoardAreaClear(List<Tile> boardAreaList)
    {
        SetMaterialForListDefault(lastBoardAreaList);
        SetMaterialForListUpdate(boardAreaList);
        lastBoardAreaList = boardAreaList;
    }


    public void StartBuildState()
    {
        AbleBoardPlane();

    }
    public void EndBuildState()
    {
        AbleBoardPlane();
        BoardClear(lastBoardList);
        BoardAreaClear(lastBoardAreaList);
        // lastBoardList.Clear(); /// why this Clear Infrastrcutre List
        // lastBoardAreaList.Clear(); /// why this Clear Infrastrcutre List
    }

    public void QuitBuildState()
    {
        AbleBoardPlane();
        SetMaterialForListDefault(lastBoardList);
        SetMaterialForListDefault(lastBoardAreaList);
        // lastBoardList.Clear(); //-- no needed?
        // lastBoardAreaList.Clear(); //-- no needed?
    }


    public void SetMaterialForListDefault(List<Tile> boardArea)
    {
        foreach (var tile in boardArea)
        {
            if (!tile.IsUsedByInfrastructure)
            {
                if (tile.IsUsedByInfrastructureArea)
                {
                    tile.TilePlane.TileMesh.material = blueMaterial;
                    Debug.Log("blue");
                }
                else
                {
                    tile.TilePlane.TileMesh.material = redMaterial;
                }
            }
            else
            {
                tile.TilePlane.TileMesh.material = greyMaterial;
            }
        }
    }

    public void SetMaterialForListUpdate(List<Tile> boardArea)
    {
        foreach (var tile in boardArea)
        {
            if (!tile.IsUsedByInfrastructure)
            {
                if (tile.IsUsedByInfrastructureArea)
                {
                    tile.TilePlane.TileMesh.material = blueMaterial; // get tile form fields for all position!!!
                }
                else
                {
                    tile.TilePlane.TileMesh.material = greenMaterial;
                }
            }
        }
    }

    public void SetMaterialForListBlocked(List<Tile> boardArea)
    {
        foreach (var tile in boardArea)
        {
            tile.TilePlane.TileMesh.material = yellowMaterial;
        }
    }

    void SetMaterialDefault(Tile tile)
    {
        if (!tile.IsUsedByInfrastructure)
        {
            tile.TilePlane.TileMesh.material = redMaterial;
        }
        else
        {
            tile.TilePlane.TileMesh.material = greyMaterial;
        }
    }

    public void SetBoardDefault(Infrastructure infrastructure)
    {
        SetMaterialForListDefault(infrastructure.InfrastructureArea.BoardAreaBlockedList);
        AbleInfrastructurePlane(infrastructure);
    }
    public void SetDefaultInfrastructure(List<Tile> infrastructureList)
    {
        Debug.Log("count " + infrastructureList.Count());
        foreach (var tile in infrastructureList)
        {
            tile.SetUsedByDefault();
        }
        SetMaterialForListDefault(infrastructureList);
    }

    public void SetDefaultInfrastructureArea(List<Tile> infrastructureAreaList)
    {
        foreach(var tile in infrastructureAreaList)
        {
            tile.SetUsedByDefault();
        }
        SetMaterialForListDefault(infrastructureAreaList);
    }


    public void BoardAreaSetAsUsedByInfrastructure(List<Tile> boardAreaUsedByInfrastructure, Infrastructure infrastructure)
    {
        foreach(var tile in boardAreaUsedByInfrastructure)
        {
            tile.UsedByInfrastructure = infrastructure;
            tile.IsUsedByInfrastructure = true;
        }
    }

    public void BoardAreaSetAsUsedByInfrastructureArea(List<Tile> boardAreaUsedByInfrastructureArea, Infrastructure infrastructure)
    {
        foreach (var tile in boardAreaUsedByInfrastructureArea)
        {
            tile.UsedByInfrastructure = infrastructure; 
            tile.IsUsedByInfrastructureArea = true; 
        }
    }




}
