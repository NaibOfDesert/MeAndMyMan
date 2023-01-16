using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public abstract class Object    
{
    // virtual int id { get; set; }
    protected ObjectType objectType;
    public ObjectType ObjectType { get { return objectType;  } }

    protected int areaSize { get; }
    public int AreaSize { get { return areaSize; } }

    protected int areaActiveCount; 
    public int AreaActiveCount { get { return areaActiveCount; } set { areaActiveCount = value; } } 

    public Object(ObjectType objectType, int areaSize)
    {
        this.objectType = objectType;
        this.areaSize = areaSize; 
    }
 
}

