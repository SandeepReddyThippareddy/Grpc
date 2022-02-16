using System;
using Grpc.Net.Client;
using System.Threading.Tasks;
using Grpc_Server;
using Grpc.Core;

namespace Grpc_Client
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {


            Console.WriteLine("Hello ---------- ");

            var result1 = await GetDataFromHelloService();

            Console.WriteLine(result1);

            Console.WriteLine("Customer ---------- ");

            var result2 = await GetDataFromCustomerService();

            Console.WriteLine(result2.FirstName);
            Console.WriteLine(result2.LastName);
            Console.WriteLine(result2.Email);


            await GetNewCustomersData();

            Console.ReadLine();

        }

        private static async Task<HelloReply> GetDataFromHelloService()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var client = new Greeter.GreeterClient(channel);

            return await client.SayHelloAsync(new HelloRequest() { Name = "Sandeep" });
        }

        private static async Task<CustomerModel> GetDataFromCustomerService()
        {

            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var client = new Customer.CustomerClient(channel);

            return await client.GetCustomerInfoAsync(new CustomerLookupModel() { Id = 1 });
        }

        private static async Task GetNewCustomersData()
        {

            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var client = new Customer.CustomerClient(channel);

            using (var call = client.GetNewCustomers(new NewCustomerModel()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var customer = call.ResponseStream.Current;

                    Console.WriteLine(customer.FirstName);
                    Console.WriteLine(customer.LastName);
                    Console.WriteLine(customer.Email);
                }
            }
        }
    }
}
