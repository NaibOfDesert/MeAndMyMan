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

    Object infrastructureObject;
    public Object InfrastructureObject { get { return infrastructureObject; } }

    MeshRenderer meshRenderer;
    // public MeshRenderer MeshRenderer { get { return meshRenderer; } } //--

    Material infrastructureMaterial;
    // public Material InfrastructureMaterial { get { return infrastructureMaterial; } } //--

    List<Tile> boardList;
    public List<Tile> BoardList { get { return boardList; } set { boardList = value;} }

    List<Tile> boardAreaList;
    public List<Tile> BoardAreaList { get { return boardAreaList; } set { boardAreaList = value; } }

    [SerializeField] TextMeshPro textCount;

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

        meshRenderer = GetComponentInChildren<MeshRenderer>();
        infrastructureMaterial = meshRenderer.material;
        infrastructureArea = FindObjectOfType<InfrastructureArea>(); //-- ???????
        textCount = GetComponentInChildren<TextMeshPro>();

        boardList = new List<Tile>();
        boardAreaList = new List<Tile>();


        // infrastructureObject = new House(); // implement depends of type of new Infrastructure

    }

    void Start()
    {

    }

    void Update()
    {
        SetTextRotation();

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
                    meshRenderer.material = infrastructureController.GreyMaterial;
                }
                else
                {
                    meshRenderer.material = infrastructureMaterial; // to rebuild, object should be a bit grey


                }
                SetAreaValue();
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

    void SetAreaCount()
    {
        infrastructureObject.AreaActiveCount = boardAreaList.Count();
    }

    public void SetTextRotation() // move to plane
    { 
        // to improve
    }

    void SetMaterial() // move to plane
    {

    }


    public void SetDefaultMaterial() // move to plane
    {
        meshRenderer.material = infrastructureMaterial;
    }

    public void SetAreaValue() // move to plane
    {
        boardAreaList.RemoveAll(n => n.IsUsedByInfrastructureArea == true);
        textCount.text =  $"{boardAreaList.Count() }";
    }

    public void TextAreaValueAble() // move to plane
    {
        textCount.enabled = !textCount.enabled;
    }
}
