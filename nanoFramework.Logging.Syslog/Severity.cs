//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using System;

namespace nanoFramework.Logging.Syslog
{
    /// <summary>
    /// Severity represents the Severity in the Syslog messages. This corresponds to the values defined in RFC 3164 except None which means that the message won't be sent.
    /// </summary>
    public enum Severity {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        None = -1,
        Emergency, 
        Alert, 
        Critical, 
        Error, 
        Warning, 
        Notice, 
        Informational, 
        Debug
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    };
}
