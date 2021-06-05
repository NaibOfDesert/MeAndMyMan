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

    public int amountOfCitizens;
    public int amountOfGold;
    public int amountOfWood;
    public int amountOfStone;
    public int amountOfFood;

    public bool isWin = false;

    public MainGameUI mainGameUI;
    public MainEndUI mainEndUI;

    public List<Player> playersList = new List<Player>();
    public List<Field> fieldsList = new List<Field>();
    public List<Transform> boardHexList = new List<Transform>();

    public Field onClickField = null;

    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    public void startGame(List<Player> __playersList)
    {
        amountOfCitizens = 100;
        amountOfGold = 100;
        amountOfWood = 100;
        amountOfStone = 100;
        amountOfFood = 100;
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
        boardHexList = mainGameUI.GetListOfHexs();

        foreach (Transform boardHex in boardHexList)
        {
            var tmpBoardHex = boardHex.name.Split('[', ']')[1].Split('.');
            int fieldPosition = int.Parse(tmpBoardHex[0]);

            FieldType newFieldType = NewFieldType(fieldPosition, rand);
            Field newField = new Field(fieldPosition, newFieldType, boardHex.position.x, boardHex.position.y, boardHex.position.z);
            fieldsList.Add(newField);
        }
        SetActive();
    }

    public void MainEndUILoad()
    {
        mainEndUI = MainEndUI.Instance;
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
        }
        else if ((__fieldPosition == 51) || (__fieldPosition == 63) || (__fieldPosition == 65))
        {
            newFieldType = FieldType.house;
        }
        else if ((__fieldPosition == 76) || (__fieldPosition == 77))
        {
            newFieldType = FieldType.field;
        }
        else if ((__fieldPosition == 39))
        {
            newFieldType = FieldType.mine;
        }
        else
        {
            // grass
            if (tempValue < 10)
            {
                newFieldType = FieldType.grass;
            }
            // forest
            if (tempValue < 8)
            {
                newFieldType = FieldType.forest;
            }
            // mountain
            if (tempValue < 4)
            {
                newFieldType = FieldType.mountain;
            }
            // water
            if (tempValue < 2) // water
            {
                newFieldType = FieldType.water;
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
                amountOfCitizens += 30;
                amountOfGold += 20;
            }
            if (field.fieldType == FieldType.houseupgrade)
            {
                amountOfCitizens += 70;
                amountOfGold += 40; 
            }
            if (field.fieldType == FieldType.mine)
            {
                amountOfCitizens -= 20;
                amountOfStone += 50;
            }
            if (field.fieldType == FieldType.field)
            {
                amountOfFood += 80;
                amountOfGold -= 10;
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
            amountOfCitizens += 10;
        }
    }

    public void SetMine()
    {
        if (onClickField != null)
        {
            onClickField = fieldsList.Where(f => f.fieldId == onClickField.fieldId).FirstOrDefault();
            GameObject field = GameObject.Find("Board/Hex1 [" + onClickField.fieldId + "].");

            onClickField.fieldType = FieldType.mine;

            GameObject newHex = GameObject.Find("BoardTemp/RockyEdge");
            field.GetComponent<MeshFilter>().mesh = newHex.GetComponent<MeshFilter>().mesh;
            amountOfWood -= 20;
            amountOfStone -= 20;
            amountOfGold -= 100;
            amountOfFood -= 30;
            amountOfCitizens += 10;
        }
    }
    public void UpgradeHouse()
    {
        if (onClickField != null)
        {
            onClickField = fieldsList.Where(f => f.fieldId == onClickField.fieldId).FirstOrDefault();
            GameObject field = GameObject.Find("Board/Hex1 [" + onClickField.fieldId + "].");

            onClickField.fieldType = FieldType.houseupgrade;

            GameObject newHex = GameObject.Find("BoardTemp/TownUpgrade");
            field.GetComponent<MeshFilter>().mesh = newHex.GetComponent<MeshFilter>().mesh;
            amountOfWood -= 20;
            amountOfStone -= 20;
            amountOfFood -= 30;
            amountOfCitizens += 20;
        }
    }

    public void CutForest()
    {
        if (onClickField != null)
        {
            onClickField = fieldsList.Where(f => f.fieldId == onClickField.fieldId).FirstOrDefault();
            GameObject field = GameObject.Find("Board/Hex1 [" + onClickField.fieldId + "].");

            onClickField.fieldType = FieldType.forestcut;

            GameObject newHex = GameObject.Find("BoardTemp/ForestCutDown");
            field.GetComponent<MeshFilter>().mesh = newHex.GetComponent<MeshFilter>().mesh;
            amountOfWood += 100;
            amountOfFood -= 10;
        }
    }

    public void SetActive()
    {
        foreach (Field field in fieldsList)
        {
            Field tempField = null;
            if (field.fieldType == FieldType.castle || field.fieldType == FieldType.house || field.fieldType == FieldType.field || field.fieldType == FieldType.mine)
            {
                SetHexValue(field);
                tempField = fieldsList.Find(f => f.fieldX == field.fieldX - 100 && f.fieldZ == field.fieldZ);
                if (tempField != null)
                {
                    tempField.isActive = true;
                    SetHexValue(tempField);
                }

                tempField = fieldsList.Find(f => f.fieldX == field.fieldX + 100 && f.fieldZ == field.fieldZ);
                if (tempField != null)
                {
                    tempField.isActive = true;
                    SetHexValue(tempField);
                }

                tempField = fieldsList.Find(f => f.fieldX == field.fieldX - 50 && f.fieldZ == field.fieldZ - 86);
                if (tempField != null)
                {
                    tempField.isActive = true;
                    SetHexValue(tempField);
                }

                tempField = fieldsList.Find(f => f.fieldX == field.fieldX - 50 && f.fieldZ == field.fieldZ + 86);
                if (tempField != null)
                {
                    tempField.isActive = true;
                    SetHexValue(tempField);
                }

                tempField = fieldsList.Find(f => f.fieldX == field.fieldX + 50 && f.fieldZ == field.fieldZ - 86);
                if (tempField != null)
                {
                    tempField.isActive = true;
                    SetHexValue(tempField);
                }

                tempField = fieldsList.Find(f => f.fieldX == field.fieldX + 50 && f.fieldZ == field.fieldZ + 86);
                if (tempField != null)
                {
                    tempField.isActive = true;
                    SetHexValue(tempField);
                }
            }
        }   
    }

    public void SetHexValue(Field __field)
    {
        GameObject tempField = GameObject.Find("Board/Hex1 [" + __field.fieldId + "].");

        if (__field.fieldType == FieldType.castle)
        {
            GameObject newHex = GameObject.Find("BoardTemp/Castle/Castle");
            tempField.GetComponent<MeshFilter>().mesh = newHex.GetComponent<MeshFilter>().mesh;
        }
        if (__field.fieldType == FieldType.house)
        {
            GameObject newHex = GameObject.Find("BoardTemp/Town/Town");
            tempField.GetComponent<MeshFilter>().mesh = newHex.GetComponent<MeshFilter>().mesh;
        }
        if (__field.fieldType == FieldType.field)
        {
            GameObject newHex = GameObject.Find("BoardTemp/Crops/Crops");
            tempField.GetComponent<MeshFilter>().mesh = newHex.GetComponent<MeshFilter>().mesh;
        }
        if (__field.fieldType == FieldType.mine)
        {
            GameObject newHex = GameObject.Find("BoardTemp/RockyEdge");
            tempField.GetComponent<MeshFilter>().mesh = newHex.GetComponent<MeshFilter>().mesh;
        }
        if (__field.fieldType == FieldType.grass)
        {
            GameObject newHex = GameObject.Find("BoardTemp/Hex11");
            tempField.GetComponent<MeshFilter>().mesh = newHex.GetComponent<MeshFilter>().mesh;
        }
        if (__field.fieldType == FieldType.forest)
        {
            GameObject newHex = GameObject.Find("BoardTemp/Forest/Forest");
            tempField.GetComponent<MeshFilter>().mesh = newHex.GetComponent<MeshFilter>().mesh;
        }
        if (__field.fieldType == FieldType.mountain)
        {
            GameObject newHex = GameObject.Find("BoardTemp/SnowyMountain/Mountain");
            tempField.GetComponent<MeshFilter>().mesh = newHex.GetComponent<MeshFilter>().mesh;
        }
        if (__field.fieldType == FieldType.water)
        {
            GameObject newHex = GameObject.Find("BoardTemp/Hex19");
            tempField.GetComponent<MeshFilter>().mesh = newHex.GetComponent<MeshFilter>().mesh;
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
        if (currentRound < 6)
        {
            currentRound++;
        }
        else
        {
            isWin = false;
            mainEndUI.isWin = false;
            mainGameUI.Exit();
        }
 
    }

    //This method is called when MainGameUI object has been created. It should be called only once. 
    public void MainGameUIIsLoaded() 
    {
        mainGameUI = MainGameUI.Instance;
    }

    public void MainEndUIIsLoaded()
    {
        mainEndUI = MainEndUI.Instance;
    }

}
