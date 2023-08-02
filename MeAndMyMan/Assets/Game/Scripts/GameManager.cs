using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameManager
{
    GameController gameController;
    GameBoardController boardController;
    InfrastructureController infrastructureController;
    int experienceAmount = 0;
    public int ExperienceAmount { get { return experienceAmount; } }
    Dictionary<EResourceType, int> resourcesDictionary;
    public Dictionary<EResourceType, int> ResourcesDictionary { get { return resourcesDictionary; } }
    List<Dictionary<string, int>> objectCostListDictionary; // TODO: dictionary of dictionary
    Dictionary<EObjectType, Dictionary<EResourceType,    int>> objectCostDictionary; 
    int goldValueToRebuildSingle = 25;


    public GameManager(GameController gameController, InfrastructureController infrastructureController, GameBoardController boardController)
    {
        this.gameController = gameController;
        this.boardController = boardController;
        this.infrastructureController = infrastructureController;
        

        SetResourcesDictionary();
        SetObjectCostListDictionary();
    }

    #region SetBasicValues
    public void SetResourcesDictionary()
    {
        resourcesDictionary = new Dictionary<EResourceType, int>();
        resourcesDictionary.Add(EResourceType.user, 25);
        resourcesDictionary.Add(EResourceType.gold, 100);
        resourcesDictionary.Add(EResourceType.food, 200);
        resourcesDictionary.Add(EResourceType.wood, 150);
        resourcesDictionary.Add(EResourceType.stone, 100);
        resourcesDictionary.Add(EResourceType.iron, 10);
    }

    public void SetObjectCostListDictionary()
    {
        objectCostListDictionary = new List<Dictionary<string, int>>(); 
        objectCostListDictionary.Add(SetCostsByDictionary(EObjectType.house, 0, 5, 10, 25, 0, 0));
        objectCostListDictionary.Add(SetCostsByDictionary(EObjectType.farm, 12, 5, 10, 25, 0, 0));
        objectCostListDictionary.Add(SetCostsByDictionary(EObjectType.tower, 5, 2, 1, 5, 0, 0));
    }

    public void SetCostDictionary()
    {
        objectCostDictionary = new Dictionary<EObjectType, Dictionary<EResourceType, int>>(); 
        objectCostDictionary.Add(EObjectType.house, SetCosts(0, 5, 10, 25, 0, 0));
        objectCostDictionary.Add(EObjectType.farm, SetCosts(12, 5, 10, 25, 0, 0));
        objectCostDictionary.Add(EObjectType.tower, SetCosts(25, 20, 100, 25, 60, 70));    
    }
    public Dictionary<EResourceType, int> SetCosts(int userCost, int goldCost, int foodCost, int woodCost, int stoneCost, int ironCost){
        Dictionary<EResourceType, int> newCostDictionary = new Dictionary<EResourceType, int>();
        newCostDictionary.Add(EResourceType.user, userCost);
        newCostDictionary.Add(EResourceType.gold, goldCost);
        newCostDictionary.Add(EResourceType.food, foodCost);
        newCostDictionary.Add(EResourceType.wood, woodCost);
        newCostDictionary.Add(EResourceType.stone, stoneCost);
        newCostDictionary.Add(EResourceType.iron, ironCost);
        return newCostDictionary;
    }
    public Dictionary<string, int> SetCostsByDictionary(EObjectType objectType, int userCost, int goldCost, int foodCost, int woodCost, int stoneCost, int ironCost)
    {
        Dictionary<string, int> newCostDictionary = new Dictionary<string, int>();
        newCostDictionary.Add("objectType", (int)objectType);
        newCostDictionary.Add(EResourceType.user.ToString(), userCost);
        newCostDictionary.Add(EResourceType.gold.ToString(), goldCost);
        newCostDictionary.Add(EResourceType.food.ToString(), foodCost);
        newCostDictionary.Add(EResourceType.wood.ToString(), woodCost);
        newCostDictionary.Add(EResourceType.stone.ToString(), stoneCost);
        newCostDictionary.Add(EResourceType.iron.ToString(), ironCost);

        return newCostDictionary;
    }
    #endregion

    public void CountExpValue()
    {
        foreach(var house in infrastructureController.HouseList)
        {
            // object list
            // dictionary to get exp value for each object * level

          //  experience += house.gameObject.ObjectLevel; 
        }

    }

    public void AddUsers(Infrastructure infrastructure)
    {
        switch (infrastructure.InfrastructureObject.ObjectType)
        {
            case EObjectType.house:
                // citizensAmount++;
                break;
            default:
                break;
        }

    }

    #region Check
    public bool CheckBuildInfrastructure(EObjectType? objectType, EObjectLevel objectLevel)  //TODO: to check i fix
    {
        if(objectType == null) return false;

        var objectCost = objectCostListDictionary.Where(d => d["objectType"].Equals((int)objectType)).SingleOrDefault();

        for(int i = 0; i < resourcesDictionary.Count(); i++)
        {
            var valueKey = resourcesDictionary.ElementAt(i).Key;
            var valueToCheck = objectCost[valueKey.ToString()];
            if((resourcesDictionary[valueKey] < (objectCost[valueKey.ToString()] * (int) objectLevel))) return false; 
        }
        return true;
    }

    public bool CheckRebuildInfrastructure(int fieldsToRebuild)
    {
        return (fieldsToRebuild * goldValueToRebuildSingle <= resourcesDictionary[EResourceType.gold]) ? true : false;
    }

    public void CalculateInfrastructureIncom(InfrastructureController infrastructureController)
    {
        CalculateIncom(infrastructureController.FarmList);

    }
    #endregion


    #region Calculate
    public void CalculateIncom(List<Infrastructure> infrastructureList)
    {
        foreach (var infrastructure in infrastructureList)
        {
            resourcesDictionary[EResourceType.food] += infrastructure.InfrastructureObject.AreaActiveCount;
        }
    }


    public void CalculateBuildInfrastructure(EObjectType objectType, EObjectLevel objectLevel)
    {
        var objectCost = objectCostListDictionary.Where(d => d["objectType"].Equals((int)objectType)).SingleOrDefault();

        for(int i = 0; i < resourcesDictionary.Count(); i++)
        {
            var valueKey = resourcesDictionary.ElementAt(i).Key;
            var valueToCheck = objectCost[valueKey.ToString()];  
            resourcesDictionary[valueKey] -= valueToCheck * (int) objectLevel;
        }
    }

    public void CalculateRebuildInfrastructure(int fieldsToRebuild)
    {
        resourcesDictionary[EResourceType.gold] -= fieldsToRebuild * goldValueToRebuildSingle;
    }
    public void CalculateDeleteInfrastructure(Infrastructure infrastructure)
    {
      // TODO: to implement




    }




    public void BalanceBuildInfrastructure(Infrastructure infrastructure)
    {

    }
    #endregion
}

