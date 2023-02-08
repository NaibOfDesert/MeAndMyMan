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

    public void CalculateInfrastructureIncom(InfrastructureController infrastructureController)
    {
        Console.WriteLine(infrastructureController); //to check is infrastructureController null? !!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        CalculateIncom(infrastructureController.FarmList);

    }




    public void CalculateIncom(List <Infrastructure> infrastructureList)
    {
        foreach(var infrastructure in infrastructureList)
        {
            foodAmount += infrastructure.InfrastructureObject.AreaActiveCount; 
        }
    }


    public bool CheckBuildInfrastructure(ObjectType objectType) // with upgrade? 
    {
        bool isAbletoBuild = false; 
        switch (objectType)
        {
            // if (!(citizensAmount < houseCost.UserCost || goldAmount < houseCost.GoldCost || foodAmount < houseCost.FoodCost || woodAmount < houseCost.WoodCost || stoneAmount < houseCost.StoneCost || ironAmount < houseCost.IronCost))
            case ObjectType.House:
                if (!(goldAmount < houseCost.GoldCost || foodAmount < houseCost.FoodCost || woodAmount < houseCost.WoodCost))
                {
                    isAbletoBuild = true; 
                }
                break;
            default:
                if (!(citizensAmount < houseCost.UserCost || goldAmount < houseCost.GoldCost || foodAmount < houseCost.FoodCost || woodAmount < houseCost.WoodCost))
                {
                    isAbletoBuild = true;
                }
                break;
        }
        return isAbletoBuild;
    }

    public bool CheckUpdateInfrastructure(ObjectType objectType)
    {

        bool isAbletoBuild = false;
        /*switch (objectType)
        {
            // if (!(citizensAmount < houseCost.UserCost || goldAmount < houseCost.GoldCost || foodAmount < houseCost.FoodCost || woodAmount < houseCost.WoodCost || stoneAmount < houseCost.StoneCost || ironAmount < houseCost.IronCost))
            case ObjectType.House:
                if (!(goldAmount < houseCost.GoldCost || foodAmount < houseCost.FoodCost || woodAmount < houseCost.WoodCost))
                {
                    isAbletoBuild = true;
                }
                break;
            default:
                if (!(citizensAmount < houseCost.UserCost || goldAmount < houseCost.GoldCost || foodAmount < houseCost.FoodCost || woodAmount < houseCost.WoodCost))
                {
                    isAbletoBuild = true;
                }
                break;
        }*/

        return isAbletoBuild;
    }
    public void CalculateBuildInfrastructure(ObjectType objectType) // with upgrade? 
    {

       
    }

    public bool CalculateUpgradeInfrastructure()
    {
        return true;
    }

    public void CalculateDeleteInfrastructure(Infrastructure infrastructure)
    {
        switch (infrastructure.InfrastructureObject.ObjectType)
        {
            case ObjectType.House:
                break;
            default:
                citizensAmount += infrastructure.InfrastructureObject.Users;
                if (infrastructure.InfrastructureObject.Users < infrastructure.InfrastructureObject.UsersMax)
                {
                    workersAmount -= infrastructure.InfrastructureObject.Users;
                }
                else
                {
                    workersAmount -= infrastructure.InfrastructureObject.UsersMax;
                }
                break;
        }
    }

    public void BalanceBuildInfrastructure(Infrastructure infrastructure)
    {

    }

}

