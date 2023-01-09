using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class TileCoordinates : MonoBehaviour
{
    [SerializeField] int unityGridSize = 1;
    
    
    TextMeshPro tileCoordinatesText;

    Vector2Int tileCoordinates;



    void Awake()
    {  
        tileCoordinatesText = GetComponent<TextMeshPro>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            UpdateCoordinates();
            UpdateName();
        }

    }

    void UpdateCoordinates()
    {
        tileCoordinates.x = Mathf.RoundToInt(transform.parent.position.x / unityGridSize);
        tileCoordinates.y = Mathf.RoundToInt(transform.parent.position.z / unityGridSize);
        tileCoordinatesText.text = tileCoordinates.ToString();


    }

    void UpdateName()
    {
        transform.parent.name = tileCoordinates.ToString();
    }
}
