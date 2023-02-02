using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Farm : Object
{
    int workers;
    int maxWorkers = 12;

    public Farm(ObjectType objectType, int areaSize, ObjectLevel objectLevel, int improvementTime) : base(objectType, areaSize, objectLevel, improvementTime)
    {
        workers = 1; 

    }
    public override int GetUsers()
    {
        return workers;
    }

    public override int GetUsersMax()
    {
        return maxWorkers;
    }

    public override void DevelopeObject()
    {
        workers++;
    }

    public override bool DevelopeObjectIsAble()
    {
        return (workers < maxWorkers);
    }

    public override void UpgradeObject()
    {
        base.UpgradeObject();
        maxWorkers += maxWorkers * 2; 

    }

    
}
