using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public abstract class Object    
{
    protected int Value; // to add in Initiate

    protected ObjectType objectType;
    public ObjectType ObjectType { get { return objectType;  } }

    protected int areaSize;
    public int AreaSize { get { return areaSize; } }

    protected int areaActiveCount; 
    public int AreaActiveCount { get { return areaActiveCount; } set { areaActiveCount = value; } }

    protected ObjectLevel objectLevel;
    public ObjectLevel ObjectLevel { get { return objectLevel; } set { objectLevel = value; } }

    public Object(ObjectType objectType, int areaSize, ObjectLevel objectLevel)
    {
        this.objectType = objectType;
        this.areaSize = areaSize;
        this.objectLevel = objectLevel;
    }
 
}

