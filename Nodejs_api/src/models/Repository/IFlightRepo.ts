import DelayedFlightDTO from "../DTO/DelayedFlightDTO";

export default interface IFlightRepo { 
	getDelayedFlight(start_date: string, end_date: string) : Promise<DelayedFlightDTO[] | null>
};