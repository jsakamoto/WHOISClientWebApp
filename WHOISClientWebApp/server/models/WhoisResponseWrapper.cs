using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Whois.NET;

namespace WHOISClientWebApp
{
    /// <summary>
    /// Wrpper class for WhoisResponse.
    /// </summary>
    public class WhoisResponseWrapper
    {
        /// <summary>
        /// Organization name.
        /// </summary>
        public string OrganizationName { get; private set; }

        /// <summary>
        /// Raw response text of WHOIS protocol.
        /// </summary>
        public string Raw { get; private set; }

        /// <summary>
        /// Responded servers host name.
        /// </summary>
        public string[] RespondedServers { get; private set; }

        /// <summary>
        /// Range of IP address.
        /// </summary>
        public IPAddressRangeWrapper AddressRange { get; private set; }

        /// <summary>
        /// Wrpper class for WhoisResponse.
        /// </summary>
        public WhoisResponseWrapper(WhoisResponse response)
        {
            if (response != null)
            {
                OrganizationName = response.OrganizationName;
                RespondedServers = response.RespondedServers;
                Raw = response.Raw;
            }
            else
            {
                RespondedServers = new string[0];
            }
            AddressRange = new IPAddressRangeWrapper(response == null ? null : response.AddressRange);
        }
    }
}