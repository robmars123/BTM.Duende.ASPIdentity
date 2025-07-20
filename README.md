# BTM.IdentityServer

Docker Build Steps:

1. dotnet build BTM.Duende.ASPIdentity.csproj -c Release -o bin/Release/net9.0/publish /p:UseAppHost=false

2. dotnet publish BTM.Duende.ASPIdentity.csproj -c Release -o bin/Release/net9.0/publish /p:UseAppHost=false

3. docker build -t btm.duende:latest .

4. docker run -d -p 8080:8080 --name btm-app btm.duende:latest

5. docker run -d -p 5000:8080 --name btm-app btm.duende:latest

