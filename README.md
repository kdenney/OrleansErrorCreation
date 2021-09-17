# OrleansErrorCreation
Goal: Recreate 'cannot find grain implementation' issue with Orleans

# Steps to recreate:

1. Clone this repository
1. Open the solution under `src/Orleans.ErrorRecreation`
1. Build the project and run the following services in this order:
    1. Silos/Greet.Silo
    1. Apis/Grpc.Api
    1. Grpc.Tester/Grpc.Tester
    
#Expected results
The tester communicates with the grpc server, which creates a grain in the Greet.Silo and the execution returns a result.

#Actual results
The tester communicates with the grpc server, which then throws an exception (details below)

#Error Logs
Here is an example of what the error should look like, and are currently the results of running everything above.

```Client successfully connected to silo host

fail: Grpc.AspNetCore.Server.ServerCallHandler[6]
      Error when executing service method 'SayHello'.
      System.InvalidOperationException: Cannot find generated GrainReference class for interface 'Grains.Contracts.IDoGreetUserGrain'
         at Orleans.Runtime.TypeMetadataCache.GetGrainReferenceType(Type interfaceType)
         at Orleans.GrainFactory.MakeCaster(Type interfaceType)
         at System.Collections.Concurrent.ConcurrentDictionary`2.GetOrAdd(TKey key, Func`2 valueFactory)
         at Orleans.GrainFactory.Cast(IAddressable grain, Type interfaceType)
         at Orleans.GrainFactory.Cast[TGrainInterface](IAddressable grain)
         at Orleans.GrainFactory.GetGrain[TGrainInterface](String primaryKey, String grainClassNamePrefix)
         at Orleans.ClusterClient.GetGrain[TGrainInterface](String primaryKey, String grainClassNamePrefix)
         at Greet.Domain.GreeterDataClient.Greet(String name) in C:\git\OrleansErrorCreation\src\Orleans.ErrorRecreation\Greet.Domain\GreeterDataClient.cs:line 23
         at Grpc.Api.GreeterService.SayHello(HelloRequest request, ServerCallContext context) in C:\git\OrleansErrorCreation\src\Orleans.ErrorRecreation\Grpc.Api\Services\GreeterService.cs:line 24
         at Grpc.Shared.Server.UnaryServerMethodInvoker`3.AwaitInvoker(Task`1 invokerTask, GrpcActivatorHandle`1 serviceHandle)
         at Grpc.Shared.Server.UnaryServerMethodInvoker`3.AwaitInvoker(Task`1 invokerTask, GrpcActivatorHandle`1 serviceHandle)
         at Grpc.AspNetCore.Server.Internal.CallHandlers.UnaryServerCallHandler`3.HandleCallAsyncCore(HttpContext httpContext, HttpContextServerCallContext serverCallContext)
         at Grpc.AspNetCore.Server.Internal.CallHandlers.ServerCallHandlerBase`3.<HandleCallAsync>g__AwaitHandleCall|8_0(HttpContextServerCallContext serverCallContext, Method`2 method, Task handleCall)

```