using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NetTools;

namespace WhoisASPNET
{
    /// <summary>
    /// Wrpper class for IPAddressRange.
    /// </summary>
    public class IPAddressRangeWrapper
    {
        private IPAddressRange _IPAddressRange;

        /// <summary>
        /// Begin of IP address in range.
        /// </summary>
        public string Begin => _IPAddressRange?.Begin.ToString();

        /// <summary>
        /// End of IP address in range.
        /// </summary>
        public string End => _IPAddressRange?.End.ToString();

        /// <summary>
        /// Wrpper class for IPAddressRange.
        /// </summary>
        public IPAddressRangeWrapper(IPAddressRange ipaddressRange)
        {
            _IPAddressRange = ipaddressRange;
        }
    }
}