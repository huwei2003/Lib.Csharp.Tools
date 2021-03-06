﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Lib.Csharp.Tools.Extend
{
    public static class DataRowExt
    {
        public static Dictionary<string, object> ToDictionary(this DataRow dr)
        {
            if (dr == null)
            {
                return null;
            }
            return dr.Table.Columns.Cast<DataColumn>().ToDictionary(c => c.ColumnName, c =>
            {
                var v = dr[c];
                if (v is DBNull)
                {
                    v = c.DataType.DefaultValue();
                }
                return v;
            });
        }

        public static Dictionary<string, object>[] ToDictionarys(this IEnumerable<DataRow> drs)
        {
            if (drs == null)
            {
                return null;
            }
            var rs = drs.ToArray();
            if (rs.Length == 0)
            {
                return null;
            }
            var columns = rs.ElementAt(0).Table.Columns.Cast<DataColumn>().ToArray();
            return rs.Select(r => columns.ToDictionary(c => c.ColumnName, c =>
            {
                var v = r[c];
                if (v is DBNull)
                {
                    v = c.DataType.DefaultValue();
                }
                return v;
            })).ToArray();
        }

        public static List<T> ToList<T>(this IEnumerable<DataRow> drs, DataColumnCollection cns)
        {
            var result = new List<T>();

            if (drs != null)
            {
                var type = typeof(T);
                if (type != typeof(Nullable))
                {
                    FieldInfo[] fis;
                    var isTable = type.BaseType.FullName.Contains("Data.Table");
                    if (isTable)
                    {
                        fis = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                    }
                    else
                    {
                        fis = type.GetFields();
                    }
                    foreach (DataRow dr in drs)
                    {
                        var constructor = type.GetConstructor(new Type[0]);
                        var obj = constructor.Invoke(new Object[0]);
                        foreach (var fi in fis)
                        {
                            var fn = fi.Name.ToLower();
                            if (fn.StartsWith("_"))
                            {
                                fn = fn.Substring(1);
                            }
                            if (cns.Contains(fn))
                            {
                                var v = dr[fn];
                                if (v.GetType() != typeof(DBNull))
                                {
                                    fi.SetValue(obj, Convert.ChangeType(v, fi.FieldType));
                                }
                            }
                        }

                        result.Add((T)obj);
                    }
                }
            }

            return result;
        }

        public static string ToCsonString(this IEnumerable<DataRow> drs)
        {
            return drs.CopyToDataTable().ToCsonString();
        }

        public static Dictionary<string, KeyValuePair<object, object>> Different(this DataRow dr, DataRow dr2, string[] columns)
        {
            var result = new Dictionary<string, KeyValuePair<object, object>>();
            foreach (var column in columns)
            {
                if (!dr[column].Equals(dr2[column]))
                {
                    result[column] = new KeyValuePair<object, object>(dr[column], dr2[column]);
                }
            }
            return result;
        }

        public static Dictionary<string, object> GetUpdate(this DataRow dr, DataRow dr2, string[] columns)
        {
            var result = new Dictionary<string, object>();
            foreach (var column in columns)
            {
                if (!dr[column].Equals(dr2[column]))
                {
                    result[column] = dr2[column];
                }
            }
            return result;
        }
    }
}