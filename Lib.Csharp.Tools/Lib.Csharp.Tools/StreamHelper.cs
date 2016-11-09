using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Csharp.Tools
{
    /// <summary>
    /// Stream 处理类
    /// </summary>
    public static class StreamHelper
    {
        public static byte[] ToBytes(this Stream stream)
        {
            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                stream.Seek(0, SeekOrigin.Begin);
                return bytes;
            }
            using (var ms = CopyStream(stream))
            {
                return ms.ToArray();
            }
        }
        /// <summary>
        /// 流拷贝
        /// </summary>
        private static MemoryStream CopyStream(this Stream input)
        {
            const int bufferSize = 4096;
            var output = new MemoryStream();
            var buffer = new byte[bufferSize];
            while (true)
            {
                var read = input.Read(buffer, 0, buffer.Length);
                if (read <= 0)
                {
                    break;
                }
                output.Write(buffer, 0, read);
            }
            output.Position = 0;
            return output;
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy(this Stream input)
        {
            return CopyStream(input);
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy(this FileStream input)
        {
            return CopyStream(input);
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy(this GZipStream input)
        {
            return CopyStream(input);
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy(this DeflateStream input)
        {
            return CopyStream(input);
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy(this BufferedStream input)
        {
            return CopyStream(input);
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy(this MemoryStream input)
        {
            return CopyStream(input);
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy(this SqlFileStream input)
        {
            return CopyStream(input);
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy(this UnmanagedMemoryStream input)
        {
            return CopyStream(input);
        }

        /// <summary>
        /// 流拷贝
        /// </summary>
        public static MemoryStream Copy(this SslStream input)
        {
            return CopyStream(input);
        }

        public static string Md5(this Stream input, bool toUpper = true)
        {
            var bytes = MD5.Create().ComputeHash(input);
            var md5 = ByteHelper.ToHexString(bytes);
            return toUpper ? md5.ToUpper() : md5;
        }
    }
}
