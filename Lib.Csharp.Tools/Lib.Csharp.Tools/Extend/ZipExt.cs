using System.IO;
using System.IO.Compression;

namespace Lib.Csharp.Tools.Extend
{
    /// <summary>
    /// 压缩相关操作类
    /// </summary>
    public static class ZipExt
    {
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Compress(this byte[] data)
        {
            var ms = new MemoryStream();
            var stream = new GZipStream(ms, CompressionMode.Compress);
            stream.Write(data, 0, data.Length);
            stream.Close();
            return ms.ToArray();
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Decompress(this byte[] data)
        {
            var ms = new MemoryStream();
            ms.Write(data, 0, data.Length);
            ms.Position = 0;
            var stream = new GZipStream(ms, CompressionMode.Decompress);
            var temp = new MemoryStream();
            var buffer = new byte[1024];
            while (true)
            {
                int read = stream.Read(buffer, 0, buffer.Length);
                if (read <= 0)
                {
                    break;
                }
                else
                {
                    temp.Write(buffer, 0, buffer.Length);
                }
            }
            stream.Close();
            return temp.ToArray();
        }
    }
}
