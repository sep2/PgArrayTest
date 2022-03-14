# PgArrayTest

```shell
dotnet run --project PgArrayTest
```

```sql
info: 3/14/2022 14:13:57.488 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT e."Id"
      FROM "Events" AS e
      WHERE e."Id" IN ('ab02c141-7706-4570-8893-ae60fa741f81', '0e89bc4e-63dd-4cb4-be17-7108d3e7a90b')

```
