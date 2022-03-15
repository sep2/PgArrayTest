# PgArrayTest

Background issue: https://github.com/npgsql/efcore.pg/issues/2302

## F# Workaround
```f#
// wrap anything in a Some(...)
let ints = [| 1; 2 |] |> Some

// this will generate the correct behavior
db.Events.Where(fun x -> ints.Value.Contains(x.Id)).ToList()
```

```sql
info: 3/15/2022 18:32:55.869 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand DbCommand (16ms) [Parameters=[@__Value_0={ '1', '2' } (DbType = Object)], CommandType='Text', CommandTimeout='30']
      SELECT b."Id"
      FROM "Events" AS b
      WHERE b."Id" = ANY (@__Value_0)
```

## F# isn't okay

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

## C# is okay

```shell
dotnet run --project PgArrayTestCS
```

```sql
info: 3/14/2022 17:45:17.522 RelationalEventId.CommandExecuted[20101] (Microsoft.EntityFrameworkCore.Database.Command) 
      Executed DbCommand (14ms) [Parameters=[@__guids_0={ 'ab02c141-7706-4570-8893-ae60fa741f81', '0e89bc4e-63dd-4cb4-be17-7108d3e7a90b' } (DbType = Object)], CommandType='Text', CommandTimeout='30']
      SELECT e."Id"
      FROM "Events" AS e
      WHERE e."Id" = ANY (@__guids_0)
```
