﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace PrtgAPI.Parameters
{
    [ExcludeFromCodeCoverage]
    class ResolveAddressParameters : BaseParameters, IJsonParameters
    {
        JsonFunction IJsonParameters.Function => JsonFunction.GeoLocator;

        public ResolveAddressParameters(string address)
        {
            this[Parameter.Custom] = new List<CustomParameter>
            {
                new CustomParameter("cache", 0),
                new CustomParameter("dom", 0),
                //Replace spaces with + so they will be encoded into %2B to prevent issues with PRTG 18.2.41+
                new CustomParameter("path", address.Replace(" ", "+"))
            };
        }
    }
}
