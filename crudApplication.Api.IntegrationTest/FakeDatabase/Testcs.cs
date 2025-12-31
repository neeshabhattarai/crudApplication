//using System;
//using System.Threading.Tasks;
//using DotNet.Testcontainers.Builders;
//using DotNet.Testcontainers.Containers;
//using DotNet.Testcontainers.Configurations;

//class SimplePostgresTest
//{
//    static async Task Main()
//    {
//        // Create a Postgres container
//        var container = new TestcontainersBuilder<PostgreSqlTestcontainer>()
//            .WithImage("postgres:16")            // make sure this image exists locally
//            .WithDatabase("testdb")
//            .WithUsername("postgres")
//            .WithPassword("postgres")
//            .WithCleanUp(true)                   // remove container after stopping
//            .WithDockerEndpoint("npipe://./pipe/docker_cli") // Windows Docker host
//            .Build();

//        // Start the container
//        await container.StartAsync();
//        Console.WriteLine("✅ Postgres container started!");

//        // Show connection info
//        Console.WriteLine($"Host: {container.Hostname}");
//        Console.WriteLine($"Port: {container.Port}");
//        Console.WriteLine($"Username: {container.Username}");
//        Console.WriteLine($"Password: {container.Password}");

//        // Wait for user input so you can verify in Docker Desktop
//        Console.WriteLine("Press any key to stop the container...");
//        Console.ReadKey();

//        // Stop and clean up
//        await container.StopAsync();
//        Console.WriteLine("✅ Container stopped and cleaned up.");
//    }
//}
