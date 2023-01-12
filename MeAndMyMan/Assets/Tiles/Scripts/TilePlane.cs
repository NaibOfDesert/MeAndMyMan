using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePlane : MonoBehaviour
{
    [SerializeField] Material redMaterial;
    [SerializeField] Material greenMaterial;
    [SerializeField] Material greyMaterial;



    Tile tile;
    MeshRenderer tileMesh;



    void Awake()
    {
        tile = GetComponentInParent<Tile>();
        tileMesh = GetComponent<MeshRenderer>();

    }

    void Update()
    {
        
    }


    void OnMouseEnter()
    {
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
    }

    private void OnMouseExit()
    {
        // tileMesh.enabled = false;
        // temporarily unlit implementation of system tiles verification
        tileMesh.material = redMaterial; 
    }

    void MeshEnable()
    {

    }

    void MeshDisable()
    {

    }
}
