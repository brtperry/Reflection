using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLAParameter
{
    [AttributeUsage(AttributeTargets.All)]
    public class DataAttribute : Attribute
    {
        private string fieldName;

        private bool include;

        public DataAttribute(string fld, bool inc)
        {
            fieldName = fld;

            include = inc;
        }

        public virtual string FieldName
        {
            get { return fieldName; }
        }

        public virtual bool Include
        {
            get { return include; }
        }
    }
}
