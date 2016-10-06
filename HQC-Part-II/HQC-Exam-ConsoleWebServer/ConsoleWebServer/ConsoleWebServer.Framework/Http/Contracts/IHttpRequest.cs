﻿using System;
using System.Collections.Generic;

namespace ConsoleWebServer.Framework.Http.Contracts
{
    public interface IHttpRequest
    {
        ActionDescriptor Action { get; }
        string Method { get; }
        Version ProtocolVersion { get; }
        IDictionary<string, ICollection<string>> Headers { get; }
        string Uri { get; }
    }
}