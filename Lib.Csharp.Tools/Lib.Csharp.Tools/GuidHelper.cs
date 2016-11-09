using System;

namespace Lib.Csharp.Tools
{
    public static class GuidHelper
    {
        public static long ToLong(this Guid id)
        {
            return BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        }
    }
}
