using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NetTools;

namespace WhoisASPNET
{
    public class IPAddressRangeWrapper
    {
        private IPAddressRange _IPAddressRange;

        public string Begin => _IPAddressRange?.Begin.ToString();

        public string End => _IPAddressRange?.End.ToString();

        public IPAddressRangeWrapper(IPAddressRange ipaddressRange)
        {
            _IPAddressRange = ipaddressRange;
        }
    }
}