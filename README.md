# graphql_sample

## Backend for frontend
using hot chocolate v11.0.9  
https://chillicream.com/docs/hotchocolate/  

application db is sqlite  

### first db migrations and launch
```bash
cd Bff
dotnet restore
dotnet ef migrations add Initial
dotnet ef database update
dotnet run
```

### graphql endpoint
https://localhost:5001/graphql/  
wss://localhost:5001/graphql/  

## Frontend
using react v17.0.1 & apollo client v3.3.11  
https://www.apollographql.com/docs/react/

this front is check out for websocket subscription work.  

### first launch
```bash
cd Front
npm install
npm start
```
