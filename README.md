### Objective

This service allow user to make conversions between different currencies.
The service  external API to request currency data , the api uuse is the Coinbase API : https://developers.coinbase.com/api/v2#get-exchange-rates
The service include user signup/singin 
The service includ authetication using jwt bearer token
Ther service store user requests in the database
The service include api rate limit to limit users requests
The service include swagger documentation for api endpoints



### General

- ASP.NET Core 6.2.3

### Frameworks, Libraries and Tools


- JwtBearer 6.0.6
- EntityFramorkCore 6.0.6
- EntityFramorkCore.SqlServer 6.0.6
- EntityFramorkCore.Tools
- Newtonsoft.Json 13.0.1
- RestSharp 108.0.1

