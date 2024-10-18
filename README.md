# SGateway

## Architecture
User go to http://appclient.sgateway.local
1. At first  request is handled by `SGateway` 
	- handle all request match ( `*.sgateway.local`)
	- then check the request path by `ClientRoute`
	- if match route then check `policies of the route`
		- if policies is match (any)
			then foward request to `Target Uri`
		- not match
			redirect to http://auth.appclient.sgateway.local?clientid=appclient&return=http://appclient.sgateway.local
	- not match route
		- not found
2. When redirected to  http://auth.appclient.sgateway.local
	- verify clientid and return url
	- show dynamic login ui of the client if configured or show default
	- User fill login form and submit
	- System send login request to `AuthService` to check
		- http://authservice/login?clientid=appclient
			- bodyform: login, password
		- `AuthService` verify login
			- if valid then generate `jwttoken`
			- store `User Session`
				- ExpiredDate	 
			- Set repsonse `Cookie` for domain `appclient.sgateway.local` and `*.`
				- Cookie container `jwttoken`
	- User logged in successful then redirect to  http://appclient.sgateway.local
	- `http://auth.appclient.sgateway.local` can handle other `identity provider`
		- oidc
		- google
3. When redirected to http://auth.appclient.sgateway.local
	- `SGateway` recheck as step 1
	- But `secure policies` is matched
	- Then forward to  `Target Uri`
		- with Header `Authorization: Bearer` + `jwttoken`
		- or `custom Header` 
	- App run at `Target Uri` handle
		- Verfiy `jwttoken` by public `signature` from `http://auth.appclient.sgateway.local/.well-known/jwks.json`
		- Read Authorization Header to get base User Info
		- If need more UserInfo 
			-  call to `UserService` to get `User Profile`
	- Know App process its own bussiness

### Terms
- Route
- Policy
	- Check user or header
	- 
- Target


### Notes
- App must run at `*.sgateway.local` or `sgateway.local`

Examples
- `github.local`
- SGateway handle `github.local` and `*.github.local`

## Entities

### User Service
- User
	- UserId
	- Name
	- Email
	- IsActive

- User Profiles
	- UserId : key
	- ImageUrl
	- Properites : json

### Auth Service

- UserAccount
	- UserId : key
	- Login : unique
	- Password
	- Locked
	- ExpiredLockDate

- UserSessions
	- SessionId: key
	- UserId
	- 

- RoleGroup

- Role

### SGateway
- Client
	- ClientId : string, key, [a-z][0-9][-_], maxlength: 50
	- Name
	- IsActive
	
- ClientRoute
	- ClientId
	- RouteId
	- FromUri : uri
	- ToUri : uri
	- Policies
	
	- PathMatching
	- PathRewriting
	- Headers
	- Timeouts


## Dependencies
- Grpc.Tools - https://learn.microsoft.com/en-us/aspnet/core/grpc/basics?view=aspnetcore-8.0
	- projects should directly reference Grpc.Tools
	- https://learn.microsoft.com/en-us/aspnet/core/grpc/clientfactory?view=aspnetcore-8.0
	- https://learn.microsoft.com/en-us/aspnet/core/grpc/grpcweb?view=aspnetcore-8.0
	- 
- YARP: Reverse Proxy - https://microsoft.github.io/reverse-proxy/
	- RouteConfig
	- ClusterConfig
	- IProxyConfigProvider: ConfigurationConfigProvider  
	- IProxyConfig
	- Refs:
		- https://github.com/microsoft/reverse-proxy/blob/main/src/ReverseProxy/Management/ProxyConfigManager.cs#L138
		- https://github.com/microsoft/reverse-proxy/blob/main/src/ReverseProxy/Configuration/RouteConfig.cs
- LettuceEncrypt: Automatic HTTPS certificate generation - https://github.com/natemcmaster/LettuceEncrypt
	- ICertificateRepository
	- IAccountStore
	- ICertificateSource 
	- IServerCertificateSelector
	- Issues:
		- https://github.com/natemcmaster/LettuceEncrypt/issues/236
		- https://github.com/natemcmaster/LettuceEncrypt/issues/228
		- 
- Microsoft.AspNetCore.RateLimiting - https://learn.microsoft.com/en-us/aspnet/core/performance/rate-limit?view=aspnetcore-8.0
- Microsoft.AspNetCore.OutputCaching -https://learn.microsoft.com/en-us/aspnet/core/performance/caching/output?view=aspnetcore-8.0

- Authorization- https://learn.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-8.0
	- IAuthorizationPolicyProvider
	- https://learn.microsoft.com/en-us/aspnet/core/security/authorization/iauthorizationpolicyprovider?view=aspnetcore-8.0

## License

MIT License
- https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository
- https://choosealicense.com/