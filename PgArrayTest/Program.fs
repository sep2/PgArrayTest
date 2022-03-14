open System
open System.Linq
open Microsoft.EntityFrameworkCore
open Microsoft.Extensions.Logging

#nowarn "20"
            
type Event() =
    member val Id: Guid = Guid.Empty with get, set

type ApplicationDbContext() =
    inherit DbContext()

    override this.OnConfiguring(builder) =
        builder
            .UseNpgsql(@"Host=localhost;Username=test;Password=test")
            .LogTo(Action<string>(Console.WriteLine), LogLevel.Information)
            .EnableSensitiveDataLogging() |> ignore
        
    [<DefaultValue>]
    val mutable private events: DbSet<Event>

    member this.Events
        with public get () = this.events
        and public set v = this.events <- v


[<EntryPoint>]
let main args =
    use db = new ApplicationDbContext()

    db.Database.EnsureCreated()

    let guids = [| Guid("ab02c141-7706-4570-8893-ae60fa741f81"); Guid("0e89bc4e-63dd-4cb4-be17-7108d3e7a90b") |]

    // F# isn't ok
    db.Events.Where(fun x -> guids.Contains(x.Id)).ToList()

    0



