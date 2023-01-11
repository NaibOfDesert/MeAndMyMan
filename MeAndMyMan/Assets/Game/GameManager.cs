using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameManager
{
    List<Field> fieldList; 
    public List<Field> FieldList { get { return fieldList; } } // is it needed?

    List<Object> objectList;
    public List<Object> ObjectList { get { return objectList; } } // is it needed?

    public GameManager()
    {
        fieldList = new List<Field>();
        objectList = new List<Object>();



    }


    public void AddFieldToList(Field newField)
    {
        fieldList.Add(newField); 
    }

    public void AddObjectToList(Object newObject)
    {
        objectList.Add(newObject);
    }
}

