using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Attributes
{
    /// <summary>
    /// Attribute: Trường bắt buộc nhập
    /// </summary>
    /// CreatedBy: nvdien(19/8/2021)
    /// ModifiedBy: nvdien(19/8/2021)
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = true)  ]
    public class MisaRequired: Attribute
    {
    }

    /// <summary>
    /// Attribute: hiển thị tên trường
    /// </summary>
    /// CreatedBy: nvdien(19/8/2021)
    /// ModifiedBy: nvdien(19/8/2021)
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = true)]
    public class MisaDisplayName: Attribute
    {
        public MisaDisplayName(string name)
        {
            FieldName = name;
        }
        public string FieldName { get; set; }
    }

    /// <summary>
    /// Attribute: những Property của đối tượng không map với cơ sở dữ liệu
    /// </summary>
    /// CreatedBy: nvdien(19/8/2021)
    /// ModifiedBy: nvdien(19/8/2021)
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = true)]
    public class MisaNotMap: Attribute
    {

    }

    /// <summary>
    /// Attribute: Những trường không được phép trùng
    /// </summary>
    /// CreatedBy: nvdien(19/8/2021)
    /// ModifiedBy: nvdien(19/8/2021)
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = true)]
    public class MisaUnique: Attribute
    {

    }
}
