using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;
using System.Globalization;

namespace EcommerceDataLayer
{
    public static class EntityToDataTable
    {
        public static DataTable GetDataTable<T>(IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list), "Value cannot be null.");
            }

            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            DataTable dt = new DataTable();
            dt.Locale = CultureInfo.InvariantCulture;
            foreach(PropertyInfo property in properties)
            {
                dt.Columns.Add(property.Name, property.PropertyType);
            }

            foreach(T entity in list)
            {
                List<object> values = new List<object>();
                foreach(PropertyInfo property in properties)
                {
                    values.Add(property.GetValue(entity));
                }

                dt.Rows.Add(values.ToArray());
            }

            return dt;
        }
    }
}
