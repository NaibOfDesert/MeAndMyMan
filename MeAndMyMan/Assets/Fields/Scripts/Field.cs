using System.Collections;
using System.Collections.Generic;
public class Field 
{
    int id;
    int size; 
    int area;
    FieldType fieldType;

    bool isPlacable; 
    public bool IsPlacable { get { return isPlacable; } set { isPlacable = value; } }

    bool isProductional;
    public bool IsProductional { get { return isProductional; } set { isProductional = value; } }
    public Field()
    {
        isPlacable = true;
    }

    public Field(FieldType fieldType)
    {

    }
}
