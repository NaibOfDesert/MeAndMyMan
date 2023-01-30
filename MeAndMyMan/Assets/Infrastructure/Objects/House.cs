using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class House: Object
{
    int resident;

    public House(ObjectType objectType, int areaSize, ObjectLevel objectLevel) : base(objectType, areaSize, objectLevel)
    {
        resident = 1; 
    }



}

