using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class House: Object
{



    public House() : base()
    {
        objectType = ObjectType.House;
        size = 1;
        areaSize = 0;
        improvementTime = 1;
        users = 1;
        usersMax = 8;
        usersMaxBacic = 8;
    }



}

