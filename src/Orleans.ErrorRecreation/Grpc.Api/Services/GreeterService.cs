using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Greet.Domain;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Grpc.Api
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly IGreeterDataClient _dataClient;

        public GreeterService(ILogger<GreeterService> logger, IGreeterDataClient dataClient)
        {
            _logger = logger;
            _dataClient = dataClient;
        }

        public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            var message = await _dataClient.Greet(request.Name);
            return new HelloReply
            {
                Message = message
            };
        }
    }
}