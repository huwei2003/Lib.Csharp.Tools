﻿using Newtonsoft.Json;

namespace Lib.Csharp.Tools.Extend
{
    /// <summary>
    /// Json操作类  用Newtonsoft.Json实现 推荐用
    /// </summary>
    public static class JsonExt
    {

        static JsonExt()
        {
            var setting = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            };
            //setting.Converters.Add(new JsonInt32Converter());

            JsonConvert.DefaultSettings = () => setting;
        }
        /// <summary>
        /// 对象转换为Json字符串
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        public static string ToJson(this object jsonObject)
        {
            return JsonConvert.SerializeObject(jsonObject);
        }

        /// <summary>
        /// 对象转换为Json字符串
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <param name="jsonName">json串的名称</param>
        /// <returns></returns>
        public static string ToJson(this object jsonObject,string jsonName)
        {
            return "{\"" + jsonName + "\":"+JsonConvert.SerializeObject(jsonObject)+"}";
        }
        /// <summary>
        /// json字符串序列化为object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string strJson)
        {
            return JsonConvert.DeserializeObject<T>(strJson);
        }
    }
}
