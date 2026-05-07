using Microsoft.AspNetCore.Http;
using System.Text.Json;
//using Newtonsoft.Json;            (optional)
namespace NexusCart.Repository
{
    public static class Session
    {
        public static void SetJson<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? GetJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }

        //public static void SetJson<T>(this ISession session, string key, object value)
        //{
        //    session.SetString(key, JsonConvert.SerializeObject(value));
        //}

        //public static T? GetJson<T>(this ISession session, string key)
        //{
        //    var value = session.GetString(key);
        //    return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        //}
    }
}
