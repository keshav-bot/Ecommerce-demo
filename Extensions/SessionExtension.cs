using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Extensions
{
    public static  class SessionExtension
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
        public static void SetCartSession( this  ISession session , string key, string value)
        {
            session.SetString(key, value);
        }
        public static string GetCartSession(this ISession session , string key)
        {
            var value = session.GetString(key);
            return value;
        }
        public static int item()
        {

            return 1;
        }

        


    }
}

