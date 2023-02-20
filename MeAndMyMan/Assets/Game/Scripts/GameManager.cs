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

    int goldValueToRebuildSingle = 25;

    ObjectCost houseCost;
    ObjectCost farmCost;


    public bool Test; 



    public GameManager(GameController gameController, InfrastructureController infrastructureController, BoardController boardController)
    {
        this.gameController = gameController;
        this. boardController = boardController;
        this.infrastructureController = infrastructureController;
    }

    public void SetCosts()
    {
        houseCost = new ObjectCost(0, 5, 10, 25, 0, 0);
        farmCost = new ObjectCost(12, 5, 10, 25, 0, 0);


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



    public bool CheckBuildInfrastructure(ObjectType objectType, ObjectLevel objectLevel) // with upgrade? 
    {
        bool isAbletoBuild = false;
        int infrastructureLevel = (int) objectLevel;

        switch (objectType)
        {
            // if (!(citizensAmount < houseCost.UserCost || goldAmount < houseCost.GoldCost || foodAmount < houseCost.FoodCost || woodAmount < houseCost.WoodCost || stoneAmount < houseCost.StoneCost || ironAmount < houseCost.IronCost))
            case ObjectType.House:
                if (!(goldAmount < houseCost.GoldCost * infrastructureLevel || foodAmount < houseCost.FoodCost * infrastructureLevel || woodAmount < houseCost.WoodCost * infrastructureLevel))
                {
                    isAbletoBuild = true; 
                }
                break;
            case ObjectType.Farm:
                if (!(citizensAmount < farmCost.UserCost * infrastructureLevel || goldAmount < farmCost.GoldCost * infrastructureLevel || foodAmount < farmCost.FoodCost * infrastructureLevel || woodAmount < farmCost.WoodCost * infrastructureLevel))
                {
                    isAbletoBuild = true;
                }
                break;
            default:
                isAbletoBuild = false;
                break;
        }
        return isAbletoBuild;
    }





    public bool CheckRebuildInfrastructure(int fieldsToRebuild)
    {
        return (fieldsToRebuild * goldValueToRebuildSingle <= goldAmount) ? true : false;
    }

    public void CalculateInfrastructureIncom(InfrastructureController infrastructureController)
    {
        CalculateIncom(infrastructureController.FarmList);

    }




    public void CalculateIncom(List<Infrastructure> infrastructureList)
    {
        foreach (var infrastructure in infrastructureList)
        {
            foodAmount += infrastructure.InfrastructureObject.AreaActiveCount;
        }
    }


    public void CalculateBuildInfrastructure(ObjectType objectType, ObjectLevel objectLevel)
    {
        int infrastructureLevel = (int)objectLevel;

        switch (objectType)
        {
            case ObjectType.House:
                goldAmount -= houseCost.GoldCost * infrastructureLevel;
                foodAmount -= houseCost.FoodCost * infrastructureLevel;
                woodAmount -= houseCost.WoodCost * infrastructureLevel;

                break;
            case ObjectType.Farm:
                citizensAmount -= farmCost.UserCost * infrastructureLevel;
                goldAmount -= farmCost.GoldCost * infrastructureLevel;
                foodAmount -= farmCost.FoodCost * infrastructureLevel;
                woodAmount -= farmCost.WoodCost * infrastructureLevel;
                break;
            default:
                break;
        }
    }

    public void CalculateRebuildInfrastructure(int fieldsToRebuild)
    {
        goldAmount -= fieldsToRebuild * goldValueToRebuildSingle;
    }
    public void CalculateDeleteInfrastructure(Infrastructure infrastructure)
    {
        switch (infrastructure.InfrastructureObject.ObjectType)
        {
            case ObjectType.House:
                citizensAmount -= infrastructure.InfrastructureObject.Users;
                break;
            default:
                citizensAmount += infrastructure.InfrastructureObject.UsersMax;
                workersAmount -= infrastructure.InfrastructureObject.Users; // TO DO: fix removing 1 user less

                /*if (infrastructure.InfrastructureObject.Users < infrastructure.InfrastructureObject.UsersMax)
                {
                }
                else
                {
                    workersAmount -= infrastructure.InfrastructureObject.UsersMax;
                }*/
                break;
        }
    }




    public void BalanceBuildInfrastructure(Infrastructure infrastructure)
    {

    }

}

