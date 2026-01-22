using Newtonsoft.Json;

namespace FoodieDash.Utility
{
    public static class SessionExtensions
    {
        // Save an object (like a Cart) into Session
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // Retrieve an object from Session
        public static T? GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}