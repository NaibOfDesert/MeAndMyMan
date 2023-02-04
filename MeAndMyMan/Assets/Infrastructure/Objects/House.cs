using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class House: Object
{
    int residents;
    int maxResidents = 8;


    public House() : base()
    {
        objectType = ObjectType.House;
        size = 1;
        areaSize = 0;
        improvementTime = 1;
        residents = 1; 
    }

    public override int GetUsers()
    {
        return residents; 
    }

    public override int GetUsersMax()
    {
        return maxResidents;
    }

    public override void DevelopeObject()
    {
        residents++; 
    }
    public override bool DevelopeObjectIsAble()
    {
        return (residents < maxResidents); 
    }

    public override void UpgradeObject()
    {
        base.UpgradeObject();
        maxResidents += maxResidents * (int)objectLevel;
    }

}

