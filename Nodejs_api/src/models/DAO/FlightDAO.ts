import { Pool } from 'pg';
import { Container, Service } from 'typedi';
import DelayedFlightDTO from '../DTO/DelayedFlightDTO';
import Logger from '@/loaders/logger';
import IFlightDAO from './IFlightDAO';

const create = () => { 
	return new FlightDAO();
};

@Service({factory:create})
export default class FlightDAO implements IFlightDAO {

	private dbConn: Pool;

	constructor() { 

		// this.dbConn  = Container.get("DbConn");
		this.dbConn  = Container.get(Pool);
	}

	async getDelayedFlight(start_date: string, end_date: string): Promise<DelayedFlightDTO[] | null> {
		Logger.info(`Getting delayed flights between ${start_date} and ${end_date}`);
		try {
			const res = await this.dbConn.query<DelayedFlightDTO>(
				"SELECT BP.UPDATE_TS BOARDING_PASS_ISSUED, \
				SCHEDULED_DEPARTURE, \
				ACTUAL_DEPARTURE, \
				STATUS \
				FROM postgres_air.FLIGHT F \
				JOIN postgres_air.BOOKING_LEG BL USING(FLIGHT_ID) \
				JOIN postgres_air.BOARDING_PASS BP USING(BOOKING_LEG_ID) \
				WHERE BP.UPDATE_TS > SCHEDULED_DEPARTURE + interval '30 minutes' \
				AND F.UPDATE_TS >= SCHEDULED_DEPARTURE - interval '1 hour' \
				AND BP.UPDATE_TS >= $1 AND BP.UPDATE_TS < $2; ",
				[start_date, end_date]
			);
			console.log("..............%o", res.rows);
			return res.rows;
		} catch (error) {
			Logger.error(error);
			return null;
		}
	}
	deleteFlight(id: number): void {
		throw new Error('Method not implemented.');
	}
	updateFlight(id: number): void {
		throw new Error('Method not implemented.');
	}
}