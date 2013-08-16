using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace MYOB.AccountRight.SDK.Extensions
{
    internal static class JsonExtensions
    {
        public static string ToJson(this object entity)
        {
            var serializer = new JsonSerializer();
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, entity);
                return writer.ToString();
            }
        }

        public static T FromJson<T>(this string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
