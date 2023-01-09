using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infrastructure : MonoBehaviour
{
    [SerializeField] bool isPlaced = false; 
    void Awake()
    {
        
    }

    void Update()
    {
        if (!isPlaced)
        {
            transform.position= GetPosition();


        }   
    }

    public void Initiate(Infrastructure infrastructurePrefab)
    {

        // generate class object
        Instantiate(infrastructurePrefab, GetPosition(), Quaternion.identity);
    }

    Vector2 GetPosition()
    {
        // convert by screen size
        Vector3 mousePosition = Input.mousePosition;
        Vector2 position = new Vector2Int(Mathf.RoundToInt(mousePosition.x), Mathf.RoundToInt(mousePosition.z));
        return position;
    }
}
