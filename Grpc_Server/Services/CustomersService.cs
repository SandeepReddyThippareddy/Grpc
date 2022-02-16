using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grpc_Server.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            _logger.LogInformation("------------Hit to Server-----------");

            return Task.FromResult(GetCustomerInfo(request.Id));

        }

        public override async Task GetNewCustomers(NewCustomerModel request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> data = new List<CustomerModel>()
            {
                new CustomerModel() {
                 Age = 1,
                        Email = "sandeep@gmail.com",
                        FirstName = "sandeep",
                        IsAlive = true,
                        LastName = "Reddy"
                },
                new CustomerModel() {
                    Age = 2,
                        Email = "prakash@gmail.com",
                        FirstName = "prakash",
                        IsAlive = true,
                        LastName = "Reddy"
                }
            };

            foreach(var item in data)
            {
                await responseStream.WriteAsync(item);
            }
        }

        private CustomerModel GetCustomerInfo(int id)
            {
                switch (id)
                {
                    case 1:
                        return new CustomerModel()
                        {
                            Age = 1,
                            Email = "sandeep@gmail.com",
                            FirstName = "sandeep",
                            IsAlive = true,
                            LastName = "Reddy"
                        };
                    case 2:
                        return new CustomerModel()
                        {
                            Age = 2,
                            Email = "prakash@gmail.com",
                            FirstName = "prakash",
                            IsAlive = true,
                            LastName = "Reddy"
                        };

                    default:
                        return null;
                }
            }
        }
    }
