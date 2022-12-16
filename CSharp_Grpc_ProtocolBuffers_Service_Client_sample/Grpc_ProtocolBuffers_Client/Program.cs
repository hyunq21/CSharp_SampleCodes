using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static async Task Main(string[] args)
        {

            // The port number must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            //var test = client.SayHello(new HelloRequest { Name = "GreeterClient22" });
            //Console.WriteLine("Greeting: " + test.Message);
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);









            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
