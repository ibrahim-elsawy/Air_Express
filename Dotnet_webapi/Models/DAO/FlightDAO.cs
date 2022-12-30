using System.Globalization;
using Dotnet_webapi.Models.DTO;
using Dotnet_webapi.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_webapi.Models.DAO
{
	public class FlightDAO : IFlightDAO
	{
        private readonly PostgresContext _context;

		public FlightDAO(PostgresContext context)
		{
			_context = context;
		}

		// public IEnumerable<Flight> GetFlightsByFilters(IEnumerable<Func<Flight,bool>> filters)
		// {

		// 	var flights = _context.Flights.Include(f => f.BookingLegs.Where(bl => bl.UpdateTs >)).Where(filters.ElementAt(0));
        //     foreach (var f in filters.Skip(1))
        //     {
		// 		flights = flights.Where(f);
		// 	}
		// 	return flights;
		// }

		public async Task<IEnumerable<Flight>> GetFlightsByFilters(DelayedFlightsRequest delayedFlightRequest)
		{ 
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime startDate = DateTime.ParseExact(delayedFlightRequest.StartDate,"MM-dd-yyyy",provider);
            DateTime endDate = DateTime.ParseExact(delayedFlightRequest.EndDate,"MM-dd-yyyy",provider);
			var flights = await _context.Flights
            .Include(f => f.BookingLegs)
            .ThenInclude(bl => bl.BoardingPasses.Where(bp => bp.UpdateTs > bp.BookingLeg.Flight.ScheduledDeparture.Add(new TimeSpan(0, 30, 0)) && 
                                                                bp.UpdateTs >= startDate && 
                                                                bp.UpdateTs < endDate))
            .Where(f => f.UpdateTs >= f.ScheduledDeparture.Subtract(new TimeSpan(1,0,0))).ToListAsync();


			return flights;
		}
	}
}