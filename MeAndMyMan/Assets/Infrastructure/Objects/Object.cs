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

    protected int size;
    public int Size { get { return size; } }

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

    protected int users;
    public int Users { get { return users; } }

    protected int usersMaxBacic;

    protected int usersMax;
    public int UsersMax { get { return usersMax; } }

    public Object()
    {
        objectLevel = ObjectLevel.Level1;
        areaActiveCount = 0;
        areaDisactiveCount = 0; 
    }
 
    public virtual void UpgradeObject()
    {
        if (objectLevel == ObjectLevel.Level4) return;
        else if (objectLevel == ObjectLevel.Level1){
            usersMax = usersMaxBacic * (int) ObjectLevel.Level2; 
            objectLevel = ObjectLevel.Level2;
            return; 
        } 
        else if (objectLevel == ObjectLevel.Level2){
            objectLevel = ObjectLevel.Level3;
            usersMax = usersMaxBacic * (int)ObjectLevel.Level3;
            return;
        }
        else if (objectLevel == ObjectLevel.Level3)
        {
            usersMax = usersMaxBacic * (int)ObjectLevel.Level4;
            objectLevel = ObjectLevel.Level4;
            return;
        }
    }
    // public abstract int GetUsers();
    // public abstract int GetUsersMax();
    public virtual void DevelopeObject()
    {
        users++; 
    }
    public virtual bool DevelopeObjectIsAble()
    {
        return (users < usersMax);
    }
}

