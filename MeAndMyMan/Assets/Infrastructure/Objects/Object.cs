﻿using System;
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

    protected int areaDisactiveCount;
    public int AreaDisactiveCount { get { return areaDisactiveCount; } set { areaDisactiveCount = value; } }

    protected ObjectLevel objectLevel;
    public ObjectLevel ObjectLevel { get { return objectLevel; } set { objectLevel = value; } }

    protected int improvementTime;
    public int ImprovementTime { get { return improvementTime; } }

    public Object(ObjectType objectType, int areaSize, ObjectLevel objectLevel, int improvementTime)
    {
        this.objectType = objectType;
        this.areaSize = areaSize;
        this.objectLevel = objectLevel;
        this.improvementTime = improvementTime;
        areaActiveCount = 0;
        areaDisactiveCount = 0; 
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

    abstract public void DevelopeObject(); 
}

