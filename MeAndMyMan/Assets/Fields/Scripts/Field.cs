using System.Collections;
using System.Collections.Generic;
public class Field 
{
    FieldType fieldType;
    public FieldType FieldType { get { return fieldType; } }

    bool isProductional;
    public bool IsProductional { get { return isProductional; } set { isProductional = value; } }
    public Field()
    {

    }

    public Field(FieldType fieldType)
    {
        this.fieldType = fieldType;
        this.isProductional = false; 
    }
}
