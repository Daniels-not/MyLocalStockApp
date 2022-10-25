using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MyStockApp.Core.Application.Helpers
{
    public static class SessionHelper
    {
        /*
            Session is nothing but a Dictionary (a collection based on key/value pairs) 
            whose key type is a string and the value type is an object 
            (i.e., any serializable object inheriting from object).
         */

        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
