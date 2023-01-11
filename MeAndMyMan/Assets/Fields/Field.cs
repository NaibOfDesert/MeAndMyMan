using System.Collections;
using System.Collections.Generic;
public class Field 
{
    int id;
    FieldType fieldType;
    // vector 2 int
    bool isPlacable; 
    public bool IsPlacable { get { return isPlacable; } set { isPlacable = value; } }

    public Field(FieldType fieldType)
    {

    }
}
