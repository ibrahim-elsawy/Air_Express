import { Router, Request, Response } from 'express';
import { Container } from 'typedi';
import IDatabaseManager from '@/models/IDatabaseManager';
const route = Router();

export default (app: Router) => {
	app.use('/flights', route);
	route.get('/delay', async (req: Request, res: Response) => {
		//call respository for return id of account 
		const dbContext: IDatabaseManager = Container.get('dbContext');
		const flights = await dbContext.flightModel.getDelayedFlight(req.body.start_date, req.body.end_date);
		if (flights != null) {
			return res.json(flights);
		};
		return res.json(flights);
	});
};
