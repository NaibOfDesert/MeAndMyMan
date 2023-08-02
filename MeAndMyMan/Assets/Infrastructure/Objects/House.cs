using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class House: ObjectBasic
{



    public House() : base()
    {
        objectType = EObjectType.house;
        size = 1;
        areaSize = 0;
        improvementTime = 1;
        users = 1;
        usersMax = 8;
        health = 10;
        healthMax = 10;

    }



}

