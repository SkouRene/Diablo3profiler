﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.ObjectModel;

/// <summary>
/// Summary description for DynamicJsonConverter
/// </summary>
public class DynamicJsonConverter : JavaScriptConverter
{
    public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
    {
        if (dictionary == null)
            throw new ArgumentNullException("dictionary");

        if (type == typeof(object))
        {
            return new DynamicJsonObject(dictionary);
        }

        return null;
    }

    public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<Type> SupportedTypes
    {
        get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(object) })); }
    }
}