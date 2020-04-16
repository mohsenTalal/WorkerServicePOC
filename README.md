## Get started
The Program.cs class contains the usual Main() method and a familiar CreateHostBuilder() method. This can be seen in the snippet below:
the main() method has Logger Configuration  
```csharp
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(@"C:\Users\mohsenTalal\Desktop\WorkerServicePOC\Logs\log.txt")
                .CreateLogger();
            CreateHostBuilder(args).Build().Run();
            try
            {
                Log.Information("Starting up the services");
            }
            catch (Exception ex)
            {
                Log.Fatal("There was a problem stating the services");
                return;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
         public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
                })
        .UseSerilog();
       }
```

 ## The Worker.cs class inherit from the IHostedService interface and IDisposable interface.  

```csharp
 public class Worker : IHostedService, IDisposable
```

*IHostedService:*

To allow developers to run a background service that can have a managed lifetime to its caller.

*IDisposable*

Is an interface that contains a single method, Dispose(), for releasing unmanaged resources, like files,
streams, database connections and so on. This method is implemented explicitly in the code when we need 
to clean up a disposable object and to release unmanaged resources that this disposable object holds.

```csharp
        public void Dispose()
        {
            _timer?.Dispose();
        }
  ```

