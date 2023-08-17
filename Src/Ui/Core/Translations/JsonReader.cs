﻿using I18NPortable;
using Newtonsoft.Json.Linq;

namespace Ui.Core.Translations;

public class JsonReader : ILocaleReader
{
    public Dictionary<string, string> Read(Stream stream)
    {
        using var streamReader = new StreamReader(stream);
        var json = streamReader.ReadToEnd();

        return JObject.Parse(json)
         .Descendants()
         .OfType<JValue>()
         .ToDictionary(jvalue => jvalue.Path, jvalue => jvalue.ToString());
    }
}