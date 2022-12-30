namespace Dotnet_webapi.Models.DTO
{
    public class DelayedFlights
    {
        public string Update_Ts { get; set; }
        public string ScheduledDeparture {get; set;}
        public string ActualDeparture { get; set; }
        public string State { get; set; }
	}
}