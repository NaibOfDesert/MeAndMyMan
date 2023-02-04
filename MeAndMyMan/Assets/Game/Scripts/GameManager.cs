using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameManager
{
    GameController gameController;
    BoardController boardController;
    InfrastructureController infrastructureController;

    int experienceAmount = 0;
    public int ExperienceAmount { get { return experienceAmount; } }

    int citizenAmount = 0;
    public int CitizenAmount { get { return citizenAmount; } }

    int workersAmount = 0;
    public int WorkersAmount { get { return workersAmount; } }

    int moneyValue = 0;
    public int MoneyValue { get { return moneyValue; } }

    int foodValue = 0;
    public int FoodValue { get { return foodValue; } }

    int woodValue = 0;
    public int WoodValue { get { return woodValue; } }

    int stoneValue = 0;
    public int StoneValue { get { return stoneValue; } }

    int ironValue = 0;
    public int IronValue { get { return ironValue; } }


    ObjectCost houseCost;
    ObjectCost farmCost;


    public GameManager(GameController gameController)
    {
        this.gameController = gameController;
        boardController = gameController.BoardController;
        infrastructureController = gameController.InfrastructureController;
    }

    public void SetCosts()
    {
        houseCost = new ObjectCost(); 
    }
    public void CountExpValue()
    {
        foreach(var house in infrastructureController.HouseList)
        {
          //  experience += house.gameObject.ObjectLevel; 
        }

    }


    public bool CalculateBuildInfrastructure(Infrastructure infrastructure)
    {
        return false; // to change
    }

}

