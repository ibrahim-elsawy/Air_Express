import Container, { Service } from 'typedi';
import DelayedFlightDTO from '../DTO/DelayedFlightDTO';
import IFlightRepo from './IFlightRepo';
import IFlightDAO from '../DAO/IFlightDAO';
import FlightDAO from '../DAO/FlightDAO';


const createRepo = () => { 
	return new FlightRepo();
};


@Service({factory:createRepo})
export default class FlightRepo implements IFlightRepo{

	dao: IFlightDAO = Container.get(FlightDAO);
	async getDelayedFlight(start_date: string, end_date: string): Promise<DelayedFlightDTO[] | null> {
		try {
			return await this.dao.getDelayedFlight(start_date, end_date);
			
		} catch (error) {
			console.log(error);
			return null;
		}
	} 

};