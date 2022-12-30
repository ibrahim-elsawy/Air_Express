using Dotnet_webapi.Models.DAO;
using Dotnet_webapi.Models.DTO;
using Dotnet_webapi.Models.Entity;

namespace Dotnet_webapi.Models.Repository
{
	public class FlightRepo : IFlightRepo
	{
		private readonly IFlightDAO dao;

		public FlightRepo(IFlightDAO dao)
		{
			this.dao = dao;
		}

		public async Task<IEnumerable<DelayedFlights>> GetDelayedFlights(DelayedFlightsRequest delayedFlightsRequest)
		{
			try
			{
				// Func<Flight, bool> updateTS1 = f => f.Include();
				var flights = await dao.GetFlightsByFilters(delayedFlightsRequest);
				return null;
			}
			catch (System.Exception ex)
			{
				// TODO
				return null;
			}
		}
	}
}