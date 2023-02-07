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

    int citizensAmount = 25;
    public int CitizensAmount { get { return citizensAmount; } }

    int workersAmount = 50;
    public int WorkersAmount { get { return workersAmount; } }

    int goldAmount = 100;
    public int GoldAmount { get { return goldAmount; } }

    int foodAmount = 150;
    public int FoodAmount { get { return foodAmount; } }

    int woodAmount =75;
    public int WoodAmount { get { return woodAmount; } }

    int stoneAmount = 25;
    public int StoneAmount { get { return stoneAmount; } }

    int ironAmount = 25;
    public int IronAmount { get { return ironAmount; } }


    ObjectCost houseCost;
    ObjectCost farmCost;


    public GameManager(GameController gameController, InfrastructureController infrastructureController, BoardController boardController)
    {
        this.gameController = gameController;
        this. boardController = boardController;
        this.infrastructureController = infrastructureController;
    }

    public void SetCosts()
    {
        houseCost = new ObjectCost(0, 5, 10, 25, 0, 0);
        farmCost = new ObjectCost(5, 5, 10, 25, 0, 0);


    }
    public void CountExpValue()
    {
        foreach(var house in infrastructureController.HouseList)
        {
          //  experience += house.gameObject.ObjectLevel; 
        }

    }


    public void CalculateInfrastructureIncom(InfrastructureController infrastructureController)
    {
        Console.WriteLine(infrastructureController); //to check is infrastructureController null? !!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        CalculateIncom(infrastructureController.FarmList);

    }
    public void AddUsers(Infrastructure infrastructure)
    {
        switch (infrastructure.InfrastructureObject.ObjectType)
        {
            case ObjectType.House:
                citizensAmount++;
                break;
            default:
                workersAmount++;
                break;
        }

    }

    public void RemoveCitizens(Infrastructure infrastructure)
    {
        switch (infrastructure.InfrastructureObject.ObjectType)
        {
            case ObjectType.House:
                citizensAmount -= infrastructure.InfrastructureObject.Users;
                break;
            default:
                workersAmount -= infrastructure.InfrastructureObject.Users;
                break;
        }
    }
    public void CalculateIncom(List <Infrastructure> infrastructureList)
    {
        foreach(var infrastructure in infrastructureList)
        {
            foodAmount += infrastructure.InfrastructureObject.AreaActiveCount; 
        }
    }

    public bool CalculateBuildInfrastructure(ObjectType objectType)
    {
        return true; // to change
    }

    public bool CalculateUpgradeInfrastructure()
    {
        return true;
    }

    public void BalanceBuildInfrastructure(Infrastructure infrastructure)
    {

    }

}

