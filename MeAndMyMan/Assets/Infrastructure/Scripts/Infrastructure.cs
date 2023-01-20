using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro; 

public class Infrastructure : MonoBehaviour
{
    [SerializeField] bool isPlaced;
    public bool IsPlaced { get { return isPlaced; }  set { isPlaced = value; } }

    int infrastructureSize = 0;
    public int InfrastructureSize { get { return infrastructureSize; } }

    InfrastructureArea infrastructureArea;
    public InfrastructureArea InfrastructureArea { get { return infrastructureArea; } }

    Object infrastructureObject;
    public Object InfrastructureObject { get { return infrastructureObject; } }

    MeshRenderer meshRenderer;
    Material infrastructureMaterial;

    List<Tile> boardList;
    public List<Tile> BoardList { get { return boardList; } set { boardList = value;} }

    List<Tile> boardAreaList;
    public List<Tile> BoardAreaList { get { return boardAreaList; } set { boardAreaList = value; } }

    // [SerializeField] TextMeshPro textCount;

    GameController gameController;
    BoardController boardController; 
    MouseController mouseController;
    InfrastructureController infrastructureController; 

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        boardController = gameController.BoardController; 
        mouseController = gameController.MouseController;
        infrastructureController = gameController.InfrastructureController;
        infrastructureArea = GetComponentInChildren<InfrastructureArea>(); 
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        infrastructureMaterial = meshRenderer.material;

        boardList = new List<Tile>();
        boardAreaList = new List<Tile>();



    }

    void Start()
    {
        infrastructureArea.SetTextRotation();

    }

    void Update()
    {
        if (!isPlaced)
        {
            Vector3 worldPosition = mouseController.WorldPosition;
            boardList = boardController.BoardCheck(worldPosition, infrastructureSize);

            if (boardList.Count() == Mathf.Pow(infrastructureSize, 2))
            {
                boardAreaList = boardController.BoardAreaCheck(worldPosition, infrastructureSize, infrastructureObject.AreaSize);
                boardController.BoardClear(boardList);
                boardController.BoardAreaClear(boardAreaList);
                transform.position = mouseController.WorldPositionConvert(infrastructureSize, worldPosition);

                if (boardList.Any(n => n.IsUsedByInfrastructure == true)) /// implemented as square objects  // set material method
                {
                    SetMaterial(infrastructureController.GreyMaterial);
                }
                else
                {
                    SetMaterial(infrastructureMaterial);
                }
                infrastructureArea.SetAreaValue(boardAreaList);
            }
        }
    }

    public void InitiateInfrastructure(Object infrastructureObject, int infrastructureSize, float rotationAxisY)
    {
        this.infrastructureObject = infrastructureObject;
        this.infrastructureSize = infrastructureSize;
        transform.rotation = Quaternion.Euler(transform.rotation.x, rotationAxisY, transform.rotation.z);
        SetAreaCount();

    }

    public void SetInfrastructure()
    {
        IsPlaced = true;
        SetMaterial(infrastructureMaterial);
        InfrastructureArea.TextAreaValueAble();
    }

    void SetAreaCount()
    {
        infrastructureObject.AreaActiveCount = boardAreaList.Count();
    }

    void SetMaterial(Material material)
    {
        meshRenderer.material = material;

    }




}
