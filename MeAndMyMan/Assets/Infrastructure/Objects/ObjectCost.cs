using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public struct ObjectCost
{
    int userCost;
    public int UserCost { get { return userCost; } }

    int goldCost;
    public int GoldCost { get { return goldCost; } }
    int foodCost;
    public int FoodCost { get { return foodCost; } }

    int woodCost;
    public int WoodCost { get { return woodCost; } }

    int stoneCost;
    public int StoneCost { get { return stoneCost; } }

    int ironCost;
    public int IronCost { get { return ironCost; } }

    public ObjectCost (int userCost, int goldCost, int foodCost, int woodCost, int stoneCost, int ironCost)
    {
        this.userCost = userCost;
        this.goldCost = goldCost;
        this.foodCost = foodCost;
        this.woodCost = woodCost;
        this.stoneCost = stoneCost;
        this.ironCost = ironCost;
    }
}

