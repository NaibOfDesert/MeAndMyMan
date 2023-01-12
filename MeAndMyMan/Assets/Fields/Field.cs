using System.Collections;
using System.Collections.Generic;
public class Field 
{
    int id;
    FieldType fieldType;

    bool isPlacable; 
    public bool IsPlacable { get { return isPlacable; } set { isPlacable = value; } }

    public Field()
    {
        isPlacable = true;
    }

    public Field(FieldType fieldType)
    {

    }
}
