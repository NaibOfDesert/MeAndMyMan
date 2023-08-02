using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public abstract class ObjectBasic    
{
    protected int Value;
    protected EObjectType objectType;
    public EObjectType ObjectType { get { return objectType;  } }
    protected int size;
    public int Size { get { return size; } }
    protected int areaSize;
    public int AreaSize { get { return areaSize; } }
    protected int areaActiveCount; 
    public int AreaActiveCount { get { return areaActiveCount; } set { areaActiveCount = value; } }
    protected int areaDisactiveCount;
    public int AreaDisactiveCount { get { return areaDisactiveCount; } set { areaDisactiveCount = value; } }
    protected EObjectLevel objectLevel;
    public EObjectLevel ObjectLevel { get { return objectLevel; } set { objectLevel = value; } }
    protected int improvementTime;
    public int ImprovementTime { get { return improvementTime; } }
    protected int users;
    public int Users { get { return users; } }
    protected int usersMax;
    public int UsersMax { get { return usersMax; } }
    protected int usersMaxBacic;
    protected int energy;
    public int Energy { get { return energy; } }
    protected int health;
    public int Health { get { return health; } }
    protected int healthMax;
    public int HealthMax { get { return healthMax; } }

    // TODO: name from JSON? 
    // TODO: description from JSON? 

    public ObjectBasic()
    {
        objectLevel = EObjectLevel.Level1;
        areaActiveCount = 0;
        areaDisactiveCount = 0;
        energy = 0;
        usersMaxBacic = usersMax;
    }
 
    public virtual void UpgradeObject()
    {
        if (objectLevel == EObjectLevel.Level4) return;
        else if (objectLevel == EObjectLevel.Level1){
            usersMax = usersMaxBacic * (int) EObjectLevel.Level2; 
            objectLevel = EObjectLevel.Level2;
            return; 
        } 
        else if (objectLevel == EObjectLevel.Level2){
            objectLevel = EObjectLevel.Level3;
            usersMax = usersMaxBacic * (int)EObjectLevel.Level3;
            return;
        }
        else if (objectLevel == EObjectLevel.Level3)
        {
            usersMax = usersMaxBacic * (int)EObjectLevel.Level4;
            objectLevel = EObjectLevel.Level4;
            return;
        }
    }
    public virtual void DevelopeObject()
    {
        users++; 
    }
    public virtual bool DevelopeObjectIsAble()
    {
        return (users < usersMax);
    }
}

