using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class InfrastructureArea : MonoBehaviour
{
    [SerializeField] TextMeshPro textCount;

    Infrastructure infrastructure; 

    GameController gameController; // shoud be taken from Infrastructure or GetFind in here??

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        infrastructure = GetComponentInParent<Infrastructure>();
        textCount = GetComponentInChildren<TextMeshPro>();

    }

    public void SetTextRotation()
    {
        textCount.transform.LookAt(gameController.GameCamera.transform); // to fix
    }

    public void SetAreaValue(List <Tile> boardAreaList) 
    {
        boardAreaList.RemoveAll(n => n.IsUsedByInfrastructureArea == true);
        infrastructure.InfrastructureObject.AreaActiveCount = boardAreaList.Count();
        textCount.text = $"{boardAreaList.Count() }";
    }

    public void TextAreaValueAble() 
    {
        textCount.enabled = !textCount.enabled;
    }



}

