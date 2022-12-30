using Dotnet_webapi.Models.DTO;
using Dotnet_webapi.Models.Entity;

namespace Dotnet_webapi.Models.DAO
{
    public interface IFlightDAO
    {
		Task<IEnumerable<Flight>> GetFlightsByFilters( DelayedFlightsRequest delayedFlightRequest);
		// IEnumerable<Flight> GetFlightsByFilters(IEnumerable<Func<Flight,bool>> filters);
	}
}