# BTM.IdentityServer

Docker Build Steps:

1. dotnet build BTM.Duende.ASPIdentity.csproj -c Release -o bin/Release/net9.0/publish /p:UseAppHost=false

2. dotnet publish BTM.Duende.ASPIdentity.csproj -c Release -o bin/Release/net9.0/publish /p:UseAppHost=false

3. docker build -t btm.duende:latest .

4. docker run -d -p 8080:8080 --name btm-app btm.duende:latest

5. docker run -d -p 5000:8080 --name btm-app btm.duende:latest

6. if https: docker run -d -p 5001:443 --name btm-app btm.duende:latest

docker run -d -p 5001:8081 --name btm-app btm.duende:latest

7. docker run -d -p 5000:80 -p 5001:443 -v "${PWD}/certs:/https" -e ASPNETCORE_URLS="http://+:80;https://+:443" -e ASPNETCORE_Kestrel__Certificates__Default__Path="/https/identityserver.pfx" -e ASPNETCORE_Kestrel__Certificates__Default__Password="Password@123" --name btm-app btm.duende:latest



docker run -d -p 5000:80 -p 5001:443 -v "${PWD}/certs:/https" -e ASPNETCORE_URLS="http://+:80;https://+:443" -e ASPNETCORE_Kestrel__Certificates__Default__Path="/https/identityserver.pfx" -e ASPNETCORE_Kestrel__Certificates__Default__Password="Password@123" -e "ConnectionStrings__DefaultConnection=Server=sqlserver2022,1433;Database=BTM.Account.Database;User Id=sa;Password=Password@123;Encrypt=True;TrustServerCertificate=True;" --name btm-app btm.duende:latest







# Running SQL Server Image on Docker
docker pull mcr.microsoft.com/mssql/server:2022-latest


docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=admin" -p 1433:1433 --name sqlserver2022 -d mcr.microsoft.com/mssql/server:2019-latest
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Password@123" -p 1433:1433 --name sqlserver2022 -v sqlvolume:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2022-latest


# create a backup of existing database
C:\Program Files\Microsoft SQL Server\MSSQL16.DEV\MSSQL\Backup

1. docker exec sqlserver2022 mkdir -p /var/opt/mssql/backup/

2. docker cp "C:\Program Files\Microsoft SQL Server\MSSQL16.DEV\MSSQL\Backup\BTM.Account.Database.bak" sqlserver2022:/var/opt/mssql/backup/

3. Connect:
docker run -it --rm mcr.microsoft.com/mssql-tools sqlcmd -S host.docker.internal -U sa -P Password@123

4. Restore line by line
BTM.Account.Database

RESTORE DATABASE [BTM.Account.Database]
FROM DISK = '/var/opt/mssql/backup/BTM.Account.Database.bak'
WITH 
    MOVE 'BTM.Account.Database' TO '/var/opt/mssql/data/BTM.Account.Database.mdf',
    MOVE 'BTM.Account.Database_log' TO '/var/opt/mssql/data/BTM.Account.Database_log.ldf',
    REPLACE;
GO


# with SQL Connection
# custom network container image
1. docker network create btm-network
2. docker run -d --name sqlserver2022 --network btm-network -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Password@123" -p 1433:1433 mcr.microsoft.com/mssql/server:2022-latest


3. docker run -d --name duende-server --network btm-network -p 5001:443 -e "ConnectionStrings__DefaultConnection=Server=sqlserver2022,1433;Database=BTM.Account.Database;User Id=sa;Password=Password@123;Encrypt=True;TrustServerCertificate=True;" your/btm-duende


Local Host SQL
59105
