using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Farm : Object
{
    public Farm() : base()
    {
        objectType = ObjectType.farm;
        size = 2;
        areaSize = 1;
        improvementTime = 2; 
        users = 1;
        usersMax = 12;
        health = 20;
        healthMax = 20;

    }

    public override void UpgradeObject()
    {
        base.UpgradeObject();
    }

}
