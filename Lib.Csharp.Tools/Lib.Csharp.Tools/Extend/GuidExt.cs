using System;

namespace Lib.Csharp.Tools.Extend
{
    public static class GuidExt
    {
        public static long ToLong(this Guid id)
        {
            return BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        }
    }
}
