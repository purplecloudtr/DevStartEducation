﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DevStart_Service.Extensions
{
    public static class SessionExtensions
{
    public static void SetJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static T GetJson<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonSerializer.Deserialize<T>(value);
    }
}
}
