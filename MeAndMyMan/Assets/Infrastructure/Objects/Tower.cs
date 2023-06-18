using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Tower : ObjectBasic, IObjectActiveDefence
{
    public Tower() : base()
    {
        objectType = ObjectType.tower;
        size = 1;
        areaSize = 0;
        improvementTime = 20; 
        users = 1;
        usersMax = 6;
        usersMaxBacic = 6;
        health = 100;
        healthMax = 100;

    }

    public override void UpgradeObject()
    {
        base.UpgradeObject();
    }

    public void Attack()
    {

    }


}
