using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataApplication.Database
{
    public static class Extensions
    {
        public static List<T> ConvertDataTable<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = row.GetItem<T>();
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(this DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        if (dr[column.ColumnName] is DBNull nullValue)
                            pro.SetValue(obj, null, null);
                        else
                            pro.SetValue(obj, dr[column.ColumnName], null);
                    }
                    else
                        continue;
                }
            }
            return obj;
        }

        public static string GenerateInsertQuery<T>(T obj, string tableName)
        {
            var type = typeof(T);

            var s1 = new StringBuilder();
            var s2 = new StringBuilder();

            var info = type.GetProperties().AsEnumerable().GetEnumerator();

            bool first = true;
            while (info.MoveNext())
            {
                if (first) first = false;
                else
                {
                    s1.Append(", ");
                    s2.Append(", ");
                }
                s1.Append(info.Current.Name.ToString());

                s2.Append("'");
                if (info.Current.PropertyType == typeof(DateTime))
                    s2.Append(((DateTime)info.Current.GetValue(obj)).ToString("yyyy-MM-dd HH:mm:ss"));
                else
                {
                    s2.Append(info.Current.GetValue(obj).ToString());
                }
                s2.Append("'");
            }

            return $"INSERT INTO {tableName} ({s1}) VALUES ({s2})";
        }
    }
}
