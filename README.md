# graphql_sample
## Backend for frontend
using hot chocolate v11.0.9  
application db is sqlite  

### first db migrations
```bash
dotnet restore  
dotnet ef migrations add Initial  
dotnet ef database update  
```

### graphql endpoint
https://localhost:5001/graphql/  
wss://localhost:5001/graphql/  

## Frontend
using react v17.0.1 & apollo client v3.3.11  
check for websocket subscription.  

### first launch
```bash
npm install
npm start
```
