using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

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
    public MeshRenderer MeshRenderer { get { return meshRenderer; } }

    Material material;
    public Material Material { get { return material; } }


    GameController gameController; 
    MouseController mouseController;
    InfrastructureController infrastructureController; 

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        mouseController = gameController.MouseController;
        infrastructureController = gameController.InfrastructureController;

        meshRenderer = GetComponentInChildren<MeshRenderer>();
        material = meshRenderer.material;
        infrastructureArea = FindObjectOfType<InfrastructureArea>(); //--




        // infrastructureObject = new House(); // implement depends of type of new Infrastructure

    }

    void Start()
    {

    }

    void Update()
    {
        if (!isPlaced)
        {
            List<Tile> boardCheckList = gameController.BoardController.BoardCheck(mouseController.WorldPosition, infrastructureSize);

            if(!(boardCheckList.Count() < Mathf.Pow(infrastructureSize, 2)))
            {
                transform.position = mouseController.WorldPositionConvert(infrastructureSize);
                if (boardCheckList.Any(n => n.IsPlacable == false)) /// implemented as square objects
                {
                    meshRenderer.material = infrastructureController.GreyMaterial;
                }
                else
                {
                    meshRenderer.material = infrastructureController.GreenMaterial;

                }
                gameController.BoardController.BoardAreaCheck(mouseController.WorldPosition, infrastructureSize, infrastructureObject.AreaSize);

            }





        }
    }

    public void InitiateInfrastructure(Object infrastructureObject, int infrastructureSize)
    {
        this.infrastructureObject = infrastructureObject;
        this.infrastructureSize = infrastructureSize;
        Debug.Log(infrastructureObject.AreaSize + " infrastructureObject.AreaSize"); 


    }


    public void SetDefaultMaterial()
    {
        meshRenderer.material = material;
    }
}
