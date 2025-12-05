docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 8310:1433 -d mcr.microsoft.com/mssql/server:2019-CU21-ubuntu-20.04

dotnet add package Microsoft.Data.SqlClient



docker login
docker pull marcoslahuertasalas/suplementos-api:v1
docker push marcoslahuertasalas/suplementos-api:v1  => esto solo si actualizo Dockerfile
docker build -t marcoslahuertasalas/suplementos-api:v1 .
docker-compose up -d