using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Lib.Csharp.Tools.Extend
{
    public static class DictionaryExt
    {
        public static Dictionary<T, TK> Copy<T, TK>(this Dictionary<T, TK> dictionary)
        {
            return dictionary.ToDictionary(d => d.Key, d => d.Value);
        }
        public static SortedDictionary<T, TK> Copy<T, TK>(this SortedDictionary<T, TK> dictionary)
        {
            var rslt = new SortedDictionary<T, TK>();
            foreach (var d in dictionary)
            {
                rslt.Add(d.Key, d.Value);
            }
            return rslt;
        }

        public static DataTable ToDataTable(this List<Dictionary<string, object>> dicts)
        {
            if (dicts == null)
            {
                return null;
            }
            if (dicts.Count > 0)
            {
                var columns = dicts.First().Select(a => new DataColumn(a.Key)).ToArray();
                var dt = new DataTable();
                dt.Columns.AddRange(columns);
                foreach (var dict in dicts)
                {
                    var row = dt.NewRow();
                    foreach (var kv in dict)
                    {
                        row[kv.Key] = kv.Value.ToString("").IsNullOrEmpty() ? "" : kv.Value;
                    }
                    dt.Rows.Add(row);
                }
                return dt;
            }
            return null;
        }
        public static DataTable ToDataTable(this List<SortedDictionary<string, object>> dicts)
        {
            if (dicts == null)
            {
                return null;
            }
            if (dicts.Count > 0)
            {
                var columns = dicts.First().Select(a => new DataColumn(a.Key)).ToArray();
                var dt = new DataTable();
                dt.Columns.AddRange(columns);
                foreach (var dict in dicts)
                {
                    var row = dt.NewRow();
                    foreach (var kv in dict)
                    {
                        row[kv.Key] = kv.Value.ToString("").IsNullOrEmpty() ? "" : kv.Value;
                    }
                    dt.Rows.Add(row);
                }
                return dt;
            }
            return null;
        }

        public static DataTable ToDataTable(this IEnumerable<Dictionary<string, object>> dicts, DataTable dtSource)
        {
            if (dicts == null)
            {
                return null;
            }
            var arr = dicts.ToArray();
            if (arr.Length > 0)
            {
                var dt = dtSource.Clone();
                foreach (var dict in arr)
                {
                    var row = dt.NewRow();
                    foreach (var kv in dict)
                    {
                        row[kv.Key] = kv.Value.ToString("").IsNullOrEmpty() ? "" : kv.Value;
                    }
                    dt.Rows.Add(row);
                }
                return dt;
            }
            return null;
        }
        public static DataTable ToDataTable(this IEnumerable<SortedDictionary<string, object>> dicts, DataTable dtSource)
        {
            if (dicts == null)
            {
                return null;
            }
            var arr = dicts.ToArray();
            if (arr.Length > 0)
            {
                var dt = dtSource.Clone();
                foreach (var dict in arr)
                {
                    var row = dt.NewRow();
                    foreach (var kv in dict)
                    {
                        row[kv.Key] = kv.Value.ToString("").IsNullOrEmpty() ? "" : kv.Value;
                    }
                    dt.Rows.Add(row);
                }
                return dt;
            }
            return null;
        }

        public static bool Remove<TK, TV>(this ConcurrentDictionary<TK, TV> dict, TK key)
        {
            if (key == null)
            {
                return false;
            }
            TV val;
            return dict.TryRemove(key, out val);
        }

        public static TV GetValue<TK, TV>(this Dictionary<TK, TV> dic, TK key, TV defaultValue = default(TV))
        {
            TV r;
            if (!dic.TryGetValue(key, out r))
            {
                r = defaultValue;
            }
            return r;
        }

        public static SortedDictionary<TK, TV> ToSortedDictionary<TK, TV>(this IDictionary<TK, TV> dict)
        {
            if (dict == null)
            {
                return null;
            }
            var result = new SortedDictionary<TK, TV>();
            foreach (var kv in dict)
            {
                result[kv.Key] = kv.Value;
            }
            return result;
        }
    }
}
