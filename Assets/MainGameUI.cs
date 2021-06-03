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

    // Getting list of hex fields.
    public List<int> GetListOfFields()
    {
        List<int> fieldsPositonsList = new List<int>();

        Transform board = GameObject.Find("Board").transform;
        foreach (Transform boardHex in board)
        {
            // Debug.Log("board:" + boardHex.name);
            var tmpBoardHex = boardHex.name.Split('[', ']')[1].Split('.'); 
            int fieldPosition = int.Parse(tmpBoardHex[0]);
            // Debug.Log("board:" + boardHex.name);
            fieldsPositonsList.Add(fieldPosition);
        }

        return fieldsPositonsList;
    }
    
    public void Update()
    {
        int currentPlayerId = gameManager.currentPlayerId;
        currentRound.text = "Round #" + gameManager.currentRound;
        currentPlayer.text = gameManager.playersList[currentPlayerId].Nick;
        amountOfCitizens.text = "Citizens " + gameManager.amountOfCitizens;
        amountOfGold.text = "Gold: " + gameManager.amountOfGold; 
        amountOfWood.text = "Wood: " + gameManager.amountOfWood;
        amountOfStone.text = "Stone: " + gameManager.amountOfStone;
        amountOfFood.text = "Food: " + gameManager.amountOfFood;

        GameObject check = GameObject.Find("Board");
        if (check != null) RefreshBoard();
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
    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("MainScene");
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
}


