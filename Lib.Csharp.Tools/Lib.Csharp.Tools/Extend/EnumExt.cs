using System;
using System.ComponentModel;

namespace Lib.Csharp.Tools.Extend
{
    public static class EnumExt
    {
        public static string GetDescription(this Enum e)
        {
            var enumInfo = e.GetType().GetField(e.ToString());
            var enumAttributes = (DescriptionAttribute[])enumInfo.
                GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (enumAttributes.Length > 0)
            {
                return enumAttributes[0].Description;
            }
            return e.ToString();
        }
    }
}
