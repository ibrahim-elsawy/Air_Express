import { Router } from 'express';
import auth from './routes/auth';
import user from './routes/user';
import flight from './routes/flight';
import googleAuth from './routes/googleAuth';

// guaranteed to get dependencies
export default () => {
	const app = Router();
	auth(app);
	// googleAuth(app);
	flight(app);
	user(app);

	return app
}