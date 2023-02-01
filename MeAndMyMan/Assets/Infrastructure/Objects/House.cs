using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class House: Object
{
    int residents;
    int maxResidents = 8;


    public House(ObjectType objectType, int areaSize, ObjectLevel objectLevel, int improvementTime) : base(objectType, areaSize, objectLevel, improvementTime)
    {
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
        maxResidents += maxResidents;
    }

}

