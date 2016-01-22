using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Whois.NET;

namespace WhoisASPNET
{
    /// <summary>
    /// Wrpper class for WhoisResponse.
    /// </summary>
    public class WhoisResponseWrapper
    {
        private WhoisResponse _Response;

        /// <summary>
        /// Organization name.
        /// </summary>
        public string OrganizationName  => _Response?.OrganizationName;

        /// <summary>
        /// Raw response text of WHOIS protocol.
        /// </summary>
        public string Raw => _Response?.Raw;

        /// <summary>
        /// Responded servers host name.
        /// </summary>
        public string[] RespondedServers => _Response?.RespondedServers ?? new string[0];

        /// <summary>
        /// Range of IP address.
        /// </summary>
        public IPAddressRangeWrapper AddressRange { get; private set; }

        /// <summary>
        /// Wrpper class for WhoisResponse.
        /// </summary>
        public WhoisResponseWrapper(WhoisResponse response)
        {
            _Response = response;
            AddressRange = new IPAddressRangeWrapper(response?.AddressRange);
        }
    }
}