import IAccountRepo from './Repository/IAccountRepo';
import IFlightRepo from './Repository/IFlightRepo';

export default interface IDatabaseManager { 
	userModel: IAccountRepo;
	flightModel: IFlightRepo;
};