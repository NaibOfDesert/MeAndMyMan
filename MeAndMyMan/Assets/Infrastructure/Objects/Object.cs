using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public abstract class Object    
{
    protected int Value;

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
 
    public void UpgradeObject()
    {
        if (objectLevel == ObjectLevel.Level4) return;
        else if (objectLevel == ObjectLevel.Level1){
            objectLevel = ObjectLevel.Level2;
            return; 
        } 
        else if (objectLevel == ObjectLevel.Level2){
            objectLevel = ObjectLevel.Level3;
            return;
        }
        else if (objectLevel == ObjectLevel.Level3)
        {
            objectLevel = ObjectLevel.Level4;
            return;
        }
    }
}

