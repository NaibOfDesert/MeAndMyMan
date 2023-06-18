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
    Dictionary<ResourceType, int> resourcesDictionary;
    public Dictionary<ResourceType, int> ResourcesDictionary { get { return resourcesDictionary; } }
    List<Dictionary<string, int>> objectCostListDictionary; // TODO: dictionary of dictionary
    Dictionary<ObjectType, Dictionary<ResourceType, int>> objectCostDictionary; 
    int goldValueToRebuildSingle = 25;

    public string testValue; 
        public string testValue1; 


    public GameManager(GameController gameController, InfrastructureController infrastructureController, BoardController boardController)
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
        resourcesDictionary = new Dictionary<ResourceType, int>();
        resourcesDictionary.Add(ResourceType.user, 25);
        resourcesDictionary.Add(ResourceType.gold, 100);
        resourcesDictionary.Add(ResourceType.food, 200);
        resourcesDictionary.Add(ResourceType.wood, 150);
        resourcesDictionary.Add(ResourceType.stone, 100);
        resourcesDictionary.Add(ResourceType.iron, 10);
    }

    public void SetObjectCostListDictionary()
    {
        objectCostListDictionary = new List<Dictionary<string, int>>(); 
        objectCostListDictionary.Add(SetCostsByDictionary(ObjectType.house, 0, 5, 10, 25, 0, 0));
        objectCostListDictionary.Add(SetCostsByDictionary(ObjectType.farm, 12, 5, 10, 25, 0, 0));
        objectCostListDictionary.Add(SetCostsByDictionary(ObjectType.tower, 5, 2, 1, 5, 0, 0));
    }

    public void SetCostDictionary()
    {
        objectCostDictionary = new Dictionary<ObjectType, Dictionary<ResourceType, int>>(); 
        objectCostDictionary.Add(ObjectType.house, SetCosts(0, 5, 10, 25, 0, 0));
        objectCostDictionary.Add(ObjectType.farm, SetCosts(12, 5, 10, 25, 0, 0));
        objectCostDictionary.Add(ObjectType.tower, SetCosts(25, 20, 100, 25, 60, 70));    
    }
    public Dictionary<ResourceType, int> SetCosts(int userCost, int goldCost, int foodCost, int woodCost, int stoneCost, int ironCost){
        Dictionary<ResourceType, int> newCostDictionary = new Dictionary<ResourceType, int>();
        newCostDictionary.Add(ResourceType.user, userCost);
        newCostDictionary.Add(ResourceType.gold, goldCost);
        newCostDictionary.Add(ResourceType.food, foodCost);
        newCostDictionary.Add(ResourceType.wood, woodCost);
        newCostDictionary.Add(ResourceType.stone, stoneCost);
        newCostDictionary.Add(ResourceType.iron, ironCost);
        return newCostDictionary;
    }
    public Dictionary<string, int> SetCostsByDictionary(ObjectType objectType, int userCost, int goldCost, int foodCost, int woodCost, int stoneCost, int ironCost)
    {
        Dictionary<string, int> newCostDictionary = new Dictionary<string, int>();
        newCostDictionary.Add("objectType", (int)objectType);
        newCostDictionary.Add(ResourceType.user.ToString(), userCost);
        newCostDictionary.Add(ResourceType.gold.ToString(), goldCost);
        newCostDictionary.Add(ResourceType.food.ToString(), foodCost);
        newCostDictionary.Add(ResourceType.wood.ToString(), woodCost);
        newCostDictionary.Add(ResourceType.stone.ToString(), stoneCost);
        newCostDictionary.Add(ResourceType.iron.ToString(), ironCost);

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
            case ObjectType.house:
                // citizensAmount++;
                break;
            default:
                break;
        }

    }

    #region Check
    public bool CheckBuildInfrastructure(ObjectType? objectType, ObjectLevel objectLevel)  //TODO: to check i fix
    {
        if(objectType == null) return false;

        var objectCost = objectCostListDictionary.Where(d => d["objectType"].Equals((int)objectType)).SingleOrDefault();

        for(int i = 0; i < resourcesDictionary.Count(); i++)
        {
            var valueKey = resourcesDictionary.ElementAt(i).Key;
            var valueToCheck = objectCost[valueKey.ToString()];
            // testValue = valueToCheck.ToString();   
            testValue1 = resourcesDictionary[valueKey].ToString();   
            testValue = "value " + objectCost["objectType"].ToString();
            if((resourcesDictionary[valueKey] < (objectCost[valueKey.ToString()] * (int) objectLevel))) return false; 

        }
        return true;
    }

    public bool CheckRebuildInfrastructure(int fieldsToRebuild)
    {
        return (fieldsToRebuild * goldValueToRebuildSingle <= resourcesDictionary[ResourceType.gold]) ? true : false;
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
            resourcesDictionary[ResourceType.food] += infrastructure.InfrastructureObject.AreaActiveCount;
        }
    }


    public void CalculateBuildInfrastructure(ObjectType objectType, ObjectLevel objectLevel)
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
        resourcesDictionary[ResourceType.gold] -= fieldsToRebuild * goldValueToRebuildSingle;
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

