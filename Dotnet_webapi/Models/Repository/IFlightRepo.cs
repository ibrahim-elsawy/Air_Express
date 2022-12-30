using Dotnet_webapi.Models.DTO;

namespace Dotnet_webapi.Models.Repository
{
    public interface IFlightRepo
    {
		Task<IEnumerable<DelayedFlights>> GetDelayedFlights(DelayedFlightsRequest delayedFlightsRequest);
	}
}