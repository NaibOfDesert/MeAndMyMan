using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePlane : MonoBehaviour
{
/*    [SerializeField] Material redMaterial;
    public Material RedMaterial { get { return redMaterial; } }

    [SerializeField] Material greenMaterial;
    public Material GreenMaterial { get { return greenMaterial; } }

    [SerializeField] Material greyMaterial;
    public Material GreyMaterial { get { return greyMaterial; } }*/



    Tile tile;
    MeshRenderer tileMesh;
    public MeshRenderer TileMesh { get { return tileMesh; } }


    void Awake()
    {
        tile = GetComponentInParent<Tile>();
        tileMesh = GetComponent<MeshRenderer>();

    }

    void Update()
    {
        
    }


    /*void OnMouseEnter()
    {

        // to move
        tileMesh.enabled = true;
        Debug.Log(transform.parent.name);
        if (tile.Field.IsPlacable)
        {
            tileMesh.material = greenMaterial;
        }
        else
        {
            tileMesh.material = greyMaterial;
        }
    }*/

    private void OnMouseExit()
    {
        // tileMesh.enabled = false;
        // temporarily unlit implementation of system tiles verification
        // tileMesh.material = redMaterial; 
    }

    void MeshEnable()
    {

    }

    void MeshDisable()
    {

    }
}
