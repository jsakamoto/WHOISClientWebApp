using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.OutputCache.V2;
using Whois.NET;

namespace WHOISClientWebApp
{
    /// <summary>
    /// WHOIS Client Web App API Controller
    /// </summary>
    [RoutePrefix("api"), EnableCors("*", "*", "*")]
    public class WhoisApiController : ApiController
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
        [HttpGet, Route("whois/{query}"), CacheOutput(NoCache = true)]
        public async Task<WhoisResponseWrapper> Query_V2(string query, string server = null, int port = 43, string encoding = "us-ascii")
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
        [HttpGet, Route("query"), CacheOutput(NoCache = true), Obsolete("Use Query_v2 instead.")]
        public async Task<WhoisResponseWrapper> Query_V1(string query, string server = null, int port = 43, string encoding = "us-ascii")
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
        [HttpGet, Route("rawquery"), CacheOutput(NoCache = true)]
        public async Task<string> RawQuery(string query, string server, int port = 43, string encoding = "us-ascii")
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
        [Route("encodings"), CacheOutput(ClientTimeSpan = 300, Private = false)]
        public string[] GetEncodings()
        {
            return Encoding.GetEncodings()
                .Select(encode => encode.GetEncoding().WebName)
                .OrderBy(name => name)
                .ToArray()
                .Distinct()
                .ToArray();
        }

        private static Encoding ParseEncoding(string encodingName)
        {
            encodingName = encodingName.ToLower();
            var encoding = Encoding.GetEncodings()
                .Select(e => e.GetEncoding())
                .FirstOrDefault(e => e.WebName.ToLower() == encodingName);
            if (encoding == null) throw new ArgumentException("Unknown encoding.", "encoding");
            return encoding;
        }
    }
}