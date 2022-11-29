using System;
namespace Dotnet_webapi.Services
{
    public class Operation : IScopedOperation, ITransiantOperation, ISingletonOperation
    {
        public string OperationId { get; }

        public Operation()
        {
			OperationId = Guid.NewGuid().ToString()[^6..];
		}
	}
}