using System.Collections;
using System.Collections.Generic;
public class Field 
{
    int id;
    int size; 
    int area;
    FieldType fieldType;

    bool isProductional;
    public bool IsProductional { get { return isProductional; } set { isProductional = value; } }
    public Field()
    {

    }

    public Field(FieldType fieldType)
    {

    }
}
