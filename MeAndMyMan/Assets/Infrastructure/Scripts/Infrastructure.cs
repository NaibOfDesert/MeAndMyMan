using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro; 

public class Infrastructure : MonoBehaviour
{
    [SerializeField] Sprite infrastructureImage;
    public Sprite InfrastructureImage { get { return infrastructureImage; } }
    bool isPlaced;
    public bool IsPlaced { get { return isPlaced; }  set { isPlaced = value; } }
    //int infrastructureSize = 0;
   //public int InfrastructureSize { get { return infrastructureSize; } }
    ObjectBasic infrastructureObject;
    public ObjectBasic InfrastructureObject { get { return infrastructureObject; } }
    InfrastructureArea infrastructureArea;
    public InfrastructureArea InfrastructureArea { get { return infrastructureArea; } }
    InfrastructureAudio infrastructureAudio;
    public InfrastructureAudio InfrastructureAudio { get { return infrastructureAudio; } }
    InfrastructureUiController infrastructureUiController;
    public InfrastructureUiController InfrastructureUiController { get { return infrastructureUiController; } }

    MeshRenderer meshRenderer;
    Material infrastructureMaterial;
    
    GameController gameController;
    GameBoardController boardController; 
    GameUiMouseController mouseController;
    InfrastructureController infrastructureController; 

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        boardController = gameController.BoardController; 
        mouseController = gameController.MouseController;
        infrastructureController = gameController.InfrastructureController;
        infrastructureArea = GetComponentInChildren<InfrastructureArea>(); 
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        infrastructureAudio = GetComponent<InfrastructureAudio>();
        infrastructureUiController = GetComponent<InfrastructureUiController>();
        
        infrastructureMaterial = meshRenderer.material;
    }

    void Start()
    {
        infrastructureArea.SetTextRotation();
    }

    void Update() 
    {
        if (!isPlaced) // TODO: to fix, area position should set objectposition, not world posiiton
        {
            Vector3 worldPosition = mouseController.WorldPosition;
            infrastructureArea.BoardList = boardController.BoardCheck(worldPosition, infrastructureObject.Size); // TODO: move saving value to infArea

            if (infrastructureArea.BoardList.Count() == Mathf.Pow(infrastructureObject.Size, 2))
            {
                infrastructureArea.BoardAreaList = boardController.BoardAreaCheck(worldPosition, infrastructureObject.Size, infrastructureObject.AreaSize); // move saving value to infArea
                infrastructureArea.SetAreaLists();
                boardController.BoardClear(infrastructureArea.BoardList);
                boardController.BoardAreaClear(infrastructureArea.BoardAreaList);
                transform.position = mouseController.WorldPositionConvert(infrastructureObject.Size, worldPosition);   

                if (infrastructureArea.BoardList.Any(n => n.IsUsedByInfrastructure == true)) // TODO: implemented as square objects
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

    public void InitiateInfrastructure(ObjectBasic infrastructureObject, int infrastructureSize, float rotationAxisY)
    {
        this.infrastructureObject = infrastructureObject;
        transform.rotation = Quaternion.Euler(transform.rotation.x, rotationAxisY, transform.rotation.z);
    }

    public void SetInfrastructure()
    {
        isPlaced = true;
        SetMaterial(infrastructureMaterial);
        infrastructureArea.TextAreaValueAble();
        infrastructureUiController.SetInfrastructure(); 
        boardController.BoardAreaCheckUsedBy(infrastructureArea.BoardList, this); // TODO: to check // move to Controller
    }

    void SetMaterial(Material material)
    {
        meshRenderer.material = material;
    }

    public void DestroyInfrastructure()
    {
        Destroy(gameObject);
    }

}
