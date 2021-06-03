using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Field
{
    public int fieldId { get; set; }
    // public string fieldPosition { get; set; }
    public bool isActive { get; set; }
    public FieldType fieldType { get; set; }

    public Field (int __fieldId, FieldType __fieldType)
    {
        fieldId = __fieldId;
        fieldType = __fieldType;
    }
}
