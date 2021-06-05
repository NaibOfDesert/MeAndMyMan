using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainGameUI : MonoBehaviour
{
    private GameManager gameManager;
    public static MainGameUI Instance { get; set; }

    public Text currentRound;
    public Text currentPlayer;

    public Text amountOfCitizens;
    public Text amountOfGold;
    public Text amountOfWood;
    public Text amountOfStone;
    public Text amountOfFood;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.MainGameUILoadBoard();
    }

    public List<Transform> GetListOfHexs()
    {
        List<Transform> boardHexList = new List<Transform>();

        Transform board = GameObject.Find("Board").transform;
        foreach (Transform boardHex in board)
        {
            boardHexList.Add(boardHex);
        }
        return boardHexList;
    }        

    public void Update()
    {
        int currentPlayerId = gameManager.currentPlayerId;

        currentRound.text = "Round #" + gameManager.currentRound;
        currentPlayer.text = gameManager.playersList[currentPlayerId].Nick;

        if (currentPlayerId == 0)
        {
            amountOfCitizens.text = "Citizens " + gameManager.amountOfCitizens;
            amountOfGold.text = "Gold: " + gameManager.amountOfGold;
            amountOfWood.text = "Wood: " + gameManager.amountOfWood;
            amountOfStone.text = "Stone: " + gameManager.amountOfStone;
            amountOfFood.text = "Food: " + gameManager.amountOfFood;
        }

        if (currentPlayerId == 1)
        {
            amountOfCitizens.text = "Citizens " + gameManager.amountOfCitizens;
            amountOfGold.text = "Gold: " + "???";
            amountOfWood.text = "Wood: " + "???";
            amountOfStone.text = "Stone: " + "???";
            amountOfFood.text = "Food: " + gameManager.amountOfFood;
        }

        if (currentPlayerId == 2)
        {
            amountOfCitizens.text = "Citizens " + gameManager.amountOfCitizens;
            amountOfGold.text = "Gold: " + "???";
            amountOfWood.text = "Wood: " + gameManager.amountOfWood;
            amountOfStone.text = "Stone: " + gameManager.amountOfStone;
            amountOfFood.text = "Food: " + "???";
        }

        GameObject check = GameObject.Find("Board");
        if (check != null) RefreshBoard();
        gameManager.SetActive();
    }

    public void SetField()
    {
        gameManager.SetField();
    }

    public void SetHouse()
    {
        gameManager.SetHouse();
    }

    public void RefreshBoard()
    {

    }

    public void EndTurn()
    {
        gameManager.EndTurn();
        //
        //
        //

    }

    public void Exit()
    {
        SceneManager.LoadSceneAsync("EndScene");
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
}


