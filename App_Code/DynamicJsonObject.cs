﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;
using System.Collections;

/// <summary>
/// Summary description for DynamicJsonObject
/// </summary>
public class DynamicJsonObject : DynamicObject
{
    private IDictionary<string, object> Dictionary { get; set; }

    public DynamicJsonObject(IDictionary<string, object> dictionary)
    {
        this.Dictionary = dictionary;
    }

    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
        result = this.Dictionary[binder.Name];

        if (result is IDictionary<string, object>)
        {
            result = new DynamicJsonObject(result as IDictionary<string, object>);
        }
        else if (result is ArrayList && (result as ArrayList) is IDictionary<string, object>)
        {
            result = new List<DynamicJsonObject>((result as ArrayList).ToArray().Select(x => new DynamicJsonObject(x as IDictionary<string, object>)));
        }
        else if (result is ArrayList)
        {
            result = new List<object>((result as ArrayList).ToArray());
        }

        return this.Dictionary.ContainsKey(binder.Name);
    }
}