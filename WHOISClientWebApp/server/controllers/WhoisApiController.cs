using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.OutputCache.V2;
using Whois.NET;

namespace WHOISClientWebApp
{
    /// <summary>
    /// (Summary of WhoisApiController)
    /// </summary>
    [RoutePrefix("api"), EnableCors("*", "*", "*")]
    public class WhoisApiController : ApiController
    {
        // GET api/whois/:query
        /// <summary>
        /// (Summary of Query_V2)
        /// </summary>
        /// <param name="query"></param>
        /// <param name="server">optional.</param>
        /// <param name="port">optional. default value is 43.</param>
        /// <param name="encoding">optional. default value is 'us-ascii'.</param>
        /// <returns>WhoisResponse object</returns>
        [HttpGet, Route("whois/{query}"), CacheOutput(NoCache = true)]
        public WhoisResponseWrapper Query_V2(string query, string server = null, int port = 43, string encoding = "us-ascii")
        {
            if (string.IsNullOrWhiteSpace(query)) throw new ArgumentException("required 'query' parameter.", "query");
            return new WhoisResponseWrapper(WhoisClient.Query(query, server, port, Encoding.GetEncoding(encoding)));
        }

        // GET /api/query/
        /// <summary>
        /// (Summary of Query_V1)
        /// </summary>
        /// <param name="query"></param>
        /// <param name="server">optional.</param>
        /// <param name="port">optional. default value is 43.</param>
        /// <param name="encoding">optional. default value is 'us-ascii'.</param>
        /// <returns>WhoisResponse object</returns>
        [HttpGet, Route("query"), CacheOutput(NoCache = true), Obsolete("OMG")]
        public WhoisResponseWrapper Query_V1(string query, string server = null, int port = 43, string encoding = "us-ascii")
        {
            return Query_V2(query, server, port, encoding);
        }

        // GET /api/rawquery/
        /// <summary>
        /// (Summary of RawQuery)
        /// </summary>
        /// <param name="query"></param>
        /// <param name="server">host name or IP address of whois server.</param>
        /// <param name="port">optional. default value is 43.</param>
        /// <param name="encoding">optional. default value is 'us-ascii'.</param>
        /// <returns>response text of whois protocol</returns>
        [HttpGet, Route("rawquery"), CacheOutput(NoCache = true)]
        public string RawQuery(string query, string server, int port = 43, string encoding = "us-ascii")
        {
            if (string.IsNullOrWhiteSpace(query)) throw new ArgumentException("required 'query' parameter.", "query");
            if (string.IsNullOrWhiteSpace(server)) throw new ArgumentException("required 'server' parameter.", "server");
            return WhoisClient.RawQuery(query, server, port, Encoding.GetEncoding(encoding));
        }

        // GET api/encodings
        /// <summary>
        /// (Summary of GetEncodings)
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
    }
}