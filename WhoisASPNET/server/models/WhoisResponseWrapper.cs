using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Whois.NET;

namespace WhoisASPNET
{
    public class WhoisResponseWrapper
    {
        private WhoisResponse _Response;

        public string OrganizationName  => _Response?.OrganizationName;

        public string Raw => _Response?.Raw;

        public string[] RespondedServers => _Response?.RespondedServers ?? new string[0];

        public IPAddressRangeWrapper AddressRange { get; private set; }

        public WhoisResponseWrapper(WhoisResponse response)
        {
            _Response = response;
            AddressRange = new IPAddressRangeWrapper(response?.AddressRange);
        }
    }
}