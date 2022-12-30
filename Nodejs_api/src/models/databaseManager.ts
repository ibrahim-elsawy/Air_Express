
import IAccountRepo from '@/models/Repository/IAccountRepo';
import IDatabaseManager from './IDatabaseManager';
import AccountRepo from './Repository/AccountRepo';
import { Container } from 'typedi';
import IFlightRepo from './Repository/IFlightRepo';
import FlightRepo from './Repository/FlightRepo';


export default class databaseManager implements IDatabaseManager{
	
	public readonly userModel: IAccountRepo;
	public readonly flightModel: FlightRepo;
	
	constructor() {
		this.userModel = Container.get(AccountRepo);
		this.flightModel = Container.get(FlightRepo);
	}


}