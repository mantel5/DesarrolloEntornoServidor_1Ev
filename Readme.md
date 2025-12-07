docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 8310:1433 -d mcr.microsoft.com/mssql/server:2019-CU21-ubuntu-20.04

dotnet add package Microsoft.Data.SqlClient



docker login
docker build -t marcoslahuertasalas/suplementos-api:v1 .  => esto si ahgo cambios
docker pull marcoslahuertasalas/suplementos-api:v1  => esto es para bajarmelo cuando no haga cambios
docker push marcoslahuertasalas/suplementos-api:v1  => con esto subo la nueva version
docker-compose down  => apaga y borra los contenedores viejos
docker-compose up -d  => descargala imagen nueva y arranca todo

http://localhost:8310/swagger 