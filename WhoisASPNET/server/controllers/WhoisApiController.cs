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

namespace WhoisASPNET
{
    [RoutePrefix("api"), EnableCors("*", "*", "*")]
    public class WhoisApiController : ApiController
    {
        // GET api/whois/:query
        [HttpGet, Route("whois/{query}"), CacheOutput(NoCache = true)]
        public WhoisResponse Query_V2(string query, string server = null, int port = 43, string encoding = "us-ascii")
        {
            if (string.IsNullOrWhiteSpace(query)) throw new ArgumentException("required 'query' parameter.", "query");
            return WhoisClient.Query(query, server, port, Encoding.GetEncoding(encoding));
        }

        // GET /api/query/
        [HttpGet, Route("query"), CacheOutput(NoCache = true)]
        public WhoisResponse Query_V1(string query, string server = null, int port = 43, string encoding = "us-ascii")
        {
            return Query_V2(query, server, port, encoding);
        }

        // GET /api/rawquery/
        [HttpGet, Route("rawquery"), CacheOutput(NoCache = true)]
        public string RawQuery(string query, string server, int port = 43, string encoding = "us-ascii")
        {
            if (string.IsNullOrWhiteSpace(query)) throw new ArgumentException("required 'query' parameter.", "query");
            if (string.IsNullOrWhiteSpace(server)) throw new ArgumentException("required 'server' parameter.", "server");
            return WhoisClient.RawQuery(query, server, port, Encoding.GetEncoding(encoding));
        }

        // GET api/encodings
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