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

    Object infrastructureObject;
    public Object InfrastructureObject { get { return infrastructureObject; } }

    InfrastructureArea infrastructureArea;
    public InfrastructureArea InfrastructureArea { get { return infrastructureArea; } }

    MeshRenderer meshRenderer;
    Material infrastructureMaterial;

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




    }

    void Start()
    {
        infrastructureArea.SetTextRotation();

    }

    void Update() 
    {
        if (!isPlaced) /// to fix, area position should set objectposition, not world posiiton
        {
            Vector3 worldPosition = mouseController.WorldPosition;
            infrastructureArea.BoardList = boardController.BoardCheck(worldPosition, infrastructureSize); // move saving value to infArea

            if (infrastructureArea.BoardList.Count() == Mathf.Pow(infrastructureSize, 2))
            {
                infrastructureArea.BoardAreaList = boardController.BoardAreaCheck(worldPosition, infrastructureSize, infrastructureObject.AreaSize); // move saving value to infArea
                infrastructureArea.SetAreaLists();
                boardController.BoardClear(infrastructureArea.BoardList);
                boardController.BoardAreaClear(infrastructureArea.BoardAreaList);
                transform.position = mouseController.WorldPositionConvert(infrastructureSize, worldPosition);   

                if (infrastructureArea.BoardList.Any(n => n.IsUsedByInfrastructure == true)) /// implemented as square objects
                {
                    SetMaterial(infrastructureController.GreyMaterial);
                }
                else
                {
                    SetMaterial(infrastructureMaterial);
                }
            }
        }
    }

    public void InitiateInfrastructure(Object infrastructureObject, int infrastructureSize, float rotationAxisY)
    {
        this.infrastructureObject = infrastructureObject;
        this.infrastructureSize = infrastructureSize;
        transform.rotation = Quaternion.Euler(transform.rotation.x, rotationAxisY, transform.rotation.z);
    }

    public void SetInfrastructure()
    {
        isPlaced = true;
        SetMaterial(infrastructureMaterial);
        infrastructureArea.TextAreaValueAble();
        boardController.BoardAreaCheckUsedBy(infrastructureArea.BoardList, this); // to check
        StartCoroutine(infrastructureController.ImproveInfrastructure(this)); 
    }

    void SetMaterial(Material material)
    {
        meshRenderer.material = material;
    }

    public void DestroyInfrastructure()
    {

        Destroy(gameObject);
    }



    // start working

    // pause
}
