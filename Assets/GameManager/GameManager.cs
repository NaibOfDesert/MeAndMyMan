using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

class GameManager : MonoBehaviour
{
    public int currentPlayerId; 
    public int currentRound;

    public int amountOfCitizens = 1000;
    public int amountOfGold = 700;
    public int amountOfWood = 500;
    public int amountOfStone = 200;
    public int amountOfFood = 200;

    private MainGameUI mainGameUI;
    public List<Player> playersList = new List<Player>();
    public List<Field> fieldsList = new List<Field>();

    public Field onClickField = null;

    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    public void startGame(List<Player> __playersList)
    {
        playersList = __playersList;
        fieldsList = new List<Field>();
        currentPlayerId = 0;
        currentRound = 1;
    }

    // Generate fields by lsit od fields positions. This method is called when MainGameUI object has been created. It should be called only once. 
    public void MainGameUILoadBoard()
    {
        var rand = new Random();
        mainGameUI = MainGameUI.Instance;
        List<int> fieldsPositonsList = mainGameUI.GetListOfFields();

        foreach (int fieldPosition in fieldsPositonsList)
        {
            // Debug.Log("position field:" + fieldPosition);
            FieldType newFieldType = NewFieldType(fieldPosition, rand);
            Field newField = new Field(fieldPosition, newFieldType);
            fieldsList.Add(newField);
            // Debug.Log("position field:" + newFieldType);
        }
    }

    // Fill randomly board.
    public FieldType NewFieldType(int __fieldPosition, System.Random __rand)
    {
        // Debug.Log("NewFieldType: " + __fieldPosition);

        FieldType newFieldType = FieldType.none;
        GameObject field = GameObject.Find("Board/Hex1 [" + __fieldPosition + "].");

        int tempValue = __rand.Next(1, 10);

        // castle and house
        if (__fieldPosition == 64)
        {
            newFieldType = FieldType.castle;
            GameObject newHex = GameObject.Find("BoardTemp/Castle/Castle");
            field.GetComponent<MeshFilter>().mesh = newHex.GetComponent<MeshFilter>().mesh;
        }
        else if ((__fieldPosition == 51) || (__fieldPosition == 63) || (__fieldPosition == 65))
        {
            newFieldType = FieldType.house;
            GameObject newHex = GameObject.Find("BoardTemp/Town/Town");
            field.GetComponent<MeshFilter>().mesh = newHex.GetComponent<MeshFilter>().mesh;
        }
        else if ((__fieldPosition == 76) || (__fieldPosition == 77))
        {
            newFieldType = FieldType.field;
            GameObject newHex = GameObject.Find("BoardTemp/Crops/Crops");
            field.GetComponent<MeshFilter>().mesh = newHex.GetComponent<MeshFilter>().mesh;
        }
        else
        {
            // mine
            if (tempValue < 10)
            {
                newFieldType = FieldType.mine;
                GameObject newHex = GameObject.Find("BoardTemp/RockyEdge");
                field.GetComponent<MeshFilter>().mesh = newHex.GetComponent<MeshFilter>().mesh;
            }

            // grass
            if (tempValue < 9)
            {
                newFieldType = FieldType.grass;
                GameObject newHex = GameObject.Find("BoardTemp/Hex11");
                field.GetComponent<MeshFilter>().mesh = newHex.GetComponent<MeshFilter>().mesh;
            }

            // forest
            if (tempValue < 5)
            {
                newFieldType = FieldType.forest;
                GameObject newHex = GameObject.Find("BoardTemp/Forest/Forest");
                field.GetComponent<MeshFilter>().mesh = newHex.GetComponent<MeshFilter>().mesh;
            }

            // mountain
            if (tempValue < 3)
            {
                newFieldType = FieldType.mountain;
                GameObject newHex = GameObject.Find("BoardTemp/SnowyMountain/Mountain");
                field.GetComponent<MeshFilter>().mesh = newHex.GetComponent<MeshFilter>().mesh;
            }

            if (tempValue < 2) // water
            {
                newFieldType = FieldType.water;
                GameObject newHex = GameObject.Find("BoardTemp/Hex19");
                field.GetComponent<MeshFilter>().mesh = newHex.GetComponent<MeshFilter>().mesh;
            }
        }
        return newFieldType;
    }

    private Field GetFieldByPosition(int fieldId)
    {
        foreach (Field field in fieldsList)
            if (field.fieldId.Equals(fieldId))
                return field;
        return null;
    }
    public void SetBoardValue()
    {
        foreach (Field field in fieldsList)
        {
            if (field.fieldType == FieldType.house)
            {
                amountOfCitizens += 200;
            }
        }
    }

    public void SetField()
    {
        if (onClickField != null)
        {
            onClickField = fieldsList.Where(f => f.fieldId == onClickField.fieldId).FirstOrDefault();
            GameObject field = GameObject.Find("Board/Hex1 [" + onClickField.fieldId + "].");

            onClickField.fieldType = FieldType.field;

            GameObject newHex = GameObject.Find("BoardTemp/Crops/Crops");
            field.GetComponent<MeshFilter>().mesh = newHex.GetComponent<MeshFilter>().mesh;
            amountOfGold -= 50;
            amountOfWood -= 10;
            amountOfStone -= 5;
            amountOfFood -= 20;
        }
    }

    public void SetHouse()
    {
        if (onClickField != null)
        {
            onClickField = fieldsList.Where(f => f.fieldId == onClickField.fieldId).FirstOrDefault();
            GameObject field = GameObject.Find("Board/Hex1 [" + onClickField.fieldId + "].");

            onClickField.fieldType = FieldType.house;

            GameObject newHex = GameObject.Find("BoardTemp/Town/Town");
            field.GetComponent<MeshFilter>().mesh = newHex.GetComponent<MeshFilter>().mesh;
            amountOfWood -= 20;
            amountOfStone -= 20;
            amountOfFood -= 30;
        }
    }



    public GameManagerAction GetAvailableAction(int __fieldPosition)
    {
        GameManagerAction availableAction = GameManagerAction.none;

        return availableAction;
    }

    public void EndTurn()
    {
        if (currentPlayerId + 1 < playersList.Count)
        {
            currentPlayerId++;
        }
        else
        {
            NextRound();
            currentPlayerId = 0;
        }
    }
    public void NextRound()
    {
        SetBoardValue();
        if (currentRound < 11)
        {
            currentRound++;
        }
        else
        {
            //
            //        
            mainGameUI.BackToMenu();
        }
 
    }

    //This method is called when MainGameUI object has been created. It should be called only once. 
    public void MainGameUIIsLoaded() 
    {
        mainGameUI = MainGameUI.Instance;
    }

}
