using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public abstract class Object    
{
    // virtual int id { get; set; }
    public ObjectType objectType { get; }

    public int areaFields;
    protected int areaFieldsActive; 

    public Object(ObjectType objectType)
    {
        this.objectType = objectType;
    }
 
}

