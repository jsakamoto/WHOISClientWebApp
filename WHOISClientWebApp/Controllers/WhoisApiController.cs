using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Whois.NET;

namespace WHOISClientWebApp
{
    /// <summary>
    /// WHOIS Client Web App API Controller
    /// </summary>
    [Route("api"), EnableCors("Any")]
    public class WhoisApiController : Controller
    {
        // GET api/whois/:query
        /// <summary>
        /// Send WHOIS protocol request recursive and return structured response.
        /// </summary>
        /// <param name="query">Domain name or IP address to query WHOIS information.</param>
        /// <param name="server">[optional] Host name or IP address of WHOIS server.</param>
        /// <param name="port">[optional] Port number of WHOIS protocol. default value is 43.</param>
        /// <param name="encoding">[optional] Encoding name to decode the text which responded from WHOIS servers. default value is 'us-ascii'.</param>
        /// <returns>Structured responce of WHOIS protocol.</returns>
        [HttpGet, Route("whois/{query}"), ResponseCache(NoStore = true)]
        public async Task<WhoisResponseWrapper> Query_V2(string query, [FromQuery]string server = null, [FromQuery]int port = 43, [FromQuery]string encoding = "us-ascii")
        {
            if (string.IsNullOrWhiteSpace(query)) throw new ArgumentException("required 'query' parameter.", "query");
            var response = await WhoisClient.QueryAsync(query, server, port, ParseEncoding(encoding));
            return new WhoisResponseWrapper(response);
        }

        // GET /api/query/
        /// <summary>
        /// [DEPRECATED] Send WHOIS protocol request recursive and return structured response.
        /// </summary>
        /// <param name="query">Domain name or IP address to query WHOIS information.</param>
        /// <param name="server">[optional] Host name or IP address of WHOIS server.</param>
        /// <param name="port">[optional] Port number of WHOIS protocol. default value is 43.</param>
        /// <param name="encoding">[optional] Encoding name to decode the text which responded from WHOIS servers. default value is 'us-ascii'.</param>
        /// <returns>Structured responce of WHOIS protocol.</returns>
        [HttpGet, Route("query"), ResponseCache(NoStore = true), Obsolete("Use Query_v2 instead.")]
        public async Task<WhoisResponseWrapper> Query_V1([FromQuery]string query, [FromQuery]string server = null, [FromQuery]int port = 43, [FromQuery]string encoding = "us-ascii")
        {
            return await Query_V2(query, server, port, encoding);
        }

        // GET /api/rawquery/
        /// <summary>
        /// Send WHOIS protocol request to single server simply and return response as is.
        /// </summary>
        /// <param name="query">Domain name or IP address to query WHOIS information.</param>
        /// <param name="server">Host name or IP address of WHOIS server.</param>
        /// <param name="port">[optional] Port number of WHOIS protocol. default value is 43.</param>
        /// <param name="encoding">[optional] Encoding name to decode the text which responded from WHOIS servers. default value is 'us-ascii'.</param>
        /// <returns>Response text of WHOIS protocol</returns>
        [HttpGet, Route("rawquery"), ResponseCache(NoStore = true)]
        public async Task<string> RawQuery([FromQuery]string query, [FromQuery]string server, [FromQuery]int port = 43, [FromQuery]string encoding = "us-ascii")
        {
            if (string.IsNullOrWhiteSpace(query)) throw new ArgumentException("required 'query' parameter.", "query");
            if (string.IsNullOrWhiteSpace(server)) throw new ArgumentException("required 'server' parameter.", "server");
            return await WhoisClient.RawQueryAsync(query, server, port, ParseEncoding(encoding));
        }

        // GET api/encodings
        /// <summary>
        /// Get all encoding names that can specify the 'encoding' argument of APIs.
        /// </summary>
        /// <returns>An array of encoding Web names.</returns>
        [HttpGet, Route("encodings"), ResponseCache(Duration = 300)]
        public IEnumerable<string> GetEncodings()
        {
            return Encodings.GetEncodingNames().OrderBy(name => name);
        }

        private static Encoding ParseEncoding(string encodingName)
        {
            encodingName = encodingName.ToLower();
            if (!Encodings.GetEncodingNames().Any(name => name == encodingName))
                throw new ArgumentException("Unknown encoding.", "encoding");
            return Encoding.GetEncoding(encodingName);
        }
    }
}