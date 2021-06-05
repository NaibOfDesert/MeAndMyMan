using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Field
{
    public int fieldId { get; set; }
    public float fieldX { get; set; }
    public float fieldY { get; set; }
    public float fieldZ { get; set; }
    public bool isActive { get; set; }
    public FieldType fieldType { get; set; }

    public Field (int __fieldId, FieldType __fieldType, float __fieldX, float __fieldY, float __fieldZ)
    {
        fieldId = __fieldId;
        fieldType = __fieldType;
        fieldX = __fieldX;
        fieldY = __fieldY;
        fieldZ = __fieldZ;
        isActive = false; 
    }
}
