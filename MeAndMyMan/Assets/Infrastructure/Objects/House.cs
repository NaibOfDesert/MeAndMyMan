using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class House: Object
{
    int resident;

    public House(ObjectType objectType, int areaSize, ObjectLevel objectLevel, int improvementTime) : base(objectType, areaSize, objectLevel, improvementTime)
    {
        resident = 1; 
    }

    override public void DevelopeObject()
    {

    }

}

