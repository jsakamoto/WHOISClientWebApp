# WHOIS Client Web App/API

[![Deploy to Azure](https://azuredeploy.net/deploybutton.svg)](https://azuredeploy.net/)
[![Deploy](https://www.herokucdn.com/deploy/button.svg)](https://heroku.com/deploy)

## Summary

This is source code repository of the Web app which allows you to look up WHOIS information on Web browser, and also provide "CORS" enabled Web API endpoint.

## Official site

**[https://whois.azurewebsites.net](https://whois.azurewebsites.net)**

## API document

### GET /api/whois/{query}

Send WHOIS protocol request recursive and return structured response.

#### Content-Type

`application/json`

#### Parameters

Parameter | Description | Parameter type | Data type
----------|-------------|----------------|-----------
**query** | **Domain name or IP address to query WHOIS information.** | path | string
server | [optional] Host name or IP address of WHOIS server. | query | string
port | [optional] Port number of WHOIS protocol. default value is 43. | query | integer
encoding | [optional] Encoding name to decode the text which responded from WHOIS servers. default value is 'us-ascii'. | query | string

#### Response

```JavaScript
{
  "OrganizationName": "string", // Organization name.
  "Raw": "string", // Raw response text of WHOIS protocol.
  "RespondedServers": [ // Responded servers host name.
    "string"
  ],
  "AddressRange": { // Range of IP address.
    "Begin": "string", // Begin of IP address in range.
    "End": "string" // End of IP address in range.
  }
}
```

#### Example

```
curl -H "Accept: application/json" "{host}/api/whois/{domain-name}"
```


### GET /api/rawquery

Send WHOIS protocol request to single server simply and return response as is.

#### Content-Type

`application/json`

#### Parameters

Parameter | Description | Parameter type | Data type
----------|-------------|----------------|-----------
**query** | **Domain name or IP address to query WHOIS information.** | query | string
**server** | **Host name or IP address of WHOIS server.** | query | string
port | [optional] Port number of WHOIS protocol. default value is 43. | query | integer
encoding | [optional] Encoding name to decode the text which responded from WHOIS servers. default value is 'us-ascii'. | query | string

#### Response

Response text of WHOIS protocol. (string)

#### Example

```
curl -H "Accept: application/json" "{host}/api/rawquery?query={domain-name}&server={whois-server-host-name}"
```

### GET /api/encodings

Get all encoding names that can specify the 'encoding' argument of APIs.

#### Content-Type

`application/json`

#### Parameters

nothing.

#### Response

```JavaScript
["string"] // The array of available encoding names.
```

#### Example

```
curl -H "Accept: application/json" "{host}/api/encodings"
```


## Implementation

Server side codes are written by C# with ASP.NET Web API based OWIN.

Client side codes are written by TypeScript with AngularJS.

## License

[GNU General Public License v2](LICENSE)