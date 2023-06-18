using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class InfrastructureArea : MonoBehaviour
{
    [SerializeField] TextMeshPro textCount;

    Infrastructure infrastructure; 

    GameController gameController; // ? shoud be taken from Infrastructure or GetFind in here??

    List<Tile> boardList; 
    public List<Tile> BoardList { get { return boardList; } set { boardList = value; } } 

    [SerializeField] List<Tile> boardAreaList; 
    public List<Tile> BoardAreaList { get { return boardAreaList; } set { boardAreaList = value; } } 

    [SerializeField] List<Tile> boardAreaBlockedList; // TODO: ++to implement 
    public List<Tile> BoardAreaBlockedList { get { return boardAreaBlockedList; } set { boardAreaBlockedList = value; } }
    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        infrastructure = GetComponentInParent<Infrastructure>();
        textCount = GetComponentInChildren<TextMeshPro>();

        boardList = new List<Tile>();
        boardAreaList = new List<Tile>();
        boardAreaBlockedList = new List<Tile>();
    }

    public void SetTextRotation()
    {
        textCount.transform.LookAt(gameController.GameCamera.transform); // TODO: to fix
    }

    public void SetAreaLists() // TODO: rebuild
    {
        boardAreaBlockedList = boardAreaList.FindAll(n => n.IsUsedByInfrastructure == true || n.IsUsedByInfrastructureArea == true); //??? is ONLY used by instrastrucutre
        infrastructure.InfrastructureObject.AreaDisactiveCount = boardAreaBlockedList.Count();
        boardAreaList.RemoveAll(n => n.IsUsedByInfrastructure == true || n.IsUsedByInfrastructureArea == true); //??? is ONLY used by instrastrucutre
        SetAreaValue(); 
    }

    public void SetAreaValue() // TODO: rebuild
    {
        infrastructure.InfrastructureObject.AreaActiveCount = boardAreaList.Count();
        textCount.text = $"{boardAreaList.Count() }";
    }

    public void TextAreaValueAble() 
    {
        textCount.enabled = !textCount.enabled;
    }

    public void SetAreaAsBlocked(Tile tile)
    {
        boardAreaList.Remove(tile);
        boardAreaBlockedList.Add(tile);
        SetAreaValue();
    }

    public void SetAreaAsActive()
    {

    }

}

