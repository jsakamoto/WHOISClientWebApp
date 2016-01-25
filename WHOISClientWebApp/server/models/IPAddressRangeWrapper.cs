using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NetTools;

namespace WHOISClientWebApp
{
    /// <summary>
    /// Wrpper class for IPAddressRange.
    /// </summary>
    public class IPAddressRangeWrapper
    {
        /// <summary>
        /// Begin of IP address in range.
        /// </summary>
        public string Begin { get; private set; }

        /// <summary>
        /// End of IP address in range.
        /// </summary>
        public string End { get; private set; }

        /// <summary>
        /// Wrpper class for IPAddressRange.
        /// </summary>
        public IPAddressRangeWrapper(IPAddressRange ipaddressRange)
        {
            if (ipaddressRange != null)
            {
                Begin = ipaddressRange.Begin.ToString();
                End = ipaddressRange.End.ToString();
            }
        }
    }
}