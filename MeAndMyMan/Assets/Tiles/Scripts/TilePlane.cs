using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePlane : MonoBehaviour
{
    MeshRenderer tileMesh;
    public MeshRenderer TileMesh { get { return tileMesh; } }

    void Awake()
    {
        tileMesh = GetComponent<MeshRenderer>();

    }

    void Update()
    {
        
    }


    private void OnMouseExit()
    {
  
    }

    void MeshEnable()
    {

    }

    void MeshDisable()
    {

    }
}
