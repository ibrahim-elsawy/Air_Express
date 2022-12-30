
import DelayedFlightDTO from '../DTO/DelayedFlightDTO';
export default interface IFlightDAO{

	getDelayedFlight(start_date: string, end_date: string) : Promise<DelayedFlightDTO[] | null>
	deleteFlight(id: number): void
	updateFlight(id: number): void

};