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
        /// <summary>
        /// Gets the data table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns>DataTable.</returns>
        /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
        public static DataTable ToDataTable<T>(this IEnumerable<T> list)
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
                //// Name of the property and the name of the column will be same.
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
