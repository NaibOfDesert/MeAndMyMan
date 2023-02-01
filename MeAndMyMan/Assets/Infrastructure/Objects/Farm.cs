using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Farm : Object
{
    int worker;

    public Farm(ObjectType objectType, int areaSize, ObjectLevel objectLevel, int improvementTime) : base(objectType, areaSize, objectLevel, improvementTime)
    {
        worker = 0; 


    }

    override public void DevelopeObject()
    {
        worker++;
    }
}
