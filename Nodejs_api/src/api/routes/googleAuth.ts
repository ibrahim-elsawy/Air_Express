import { Router, Request, Response, NextFunction } from 'express';
import config from '@/config';
import passport from 'passport';
// import GoogleStrategy, { StrategyOptionsWithRequest, VerifyFunctionWithRequest } from 'passport-google-oidc-token';
import { Container } from 'typedi';
import AuthService from '@/services/auth';
import middlewares from '../middlewares';
import { celebrate, Joi } from 'celebrate';
import { Logger } from 'winston';
import { OAuth2Client } from 'google-auth-library';
// @ts-ignore
import GoogleStrategy from 'passport-google-oidc';


const route = Router();

export default (app: Router) => {
	
	
	// const options: StrategyOptionsWithRequest = { 
	// 	clientID: config.googleCredentials.GOOGLE_CLIENT_ID,
	// 	passReqToCallback: true
	// };
	// const verify: VerifyFunctionWithRequest = (req, idToken, profile, doneCallback) => {
	// 	const logger: Logger = Container.get('logger');
	// 	logger.debug('req: %o', req);
	// 	logger.debug('id: %o', idToken);
	// 	logger.debug('profile: %o', profile);
	// 	doneCallback(null, { id: idToken, name: profile.displayName }, undefined);
	// };

	// const strategy = new GoogleStrategy(options, verify);
	// // strategy.client._clientSecret = config.googleCredentials.GOOGLE_CLIENT_SECRET;
	// strategy.client = new OAuth2Client(
	// 	config.googleCredentials.GOOGLE_CLIENT_ID,
	// 	config.googleCredentials.GOOGLE_CLIENT_SECRET,
	// 	'/api/auth/redirect/google'
	// )
	
	const strategy = new GoogleStrategy({
		clientID: config.googleCredentials.GOOGLE_CLIENT_ID,
		clientSecret: config.googleCredentials.GOOGLE_CLIENT_SECRET,
		callbackURL: '/api/auth/redirect/google',
		scope: ['profile']
	}, function verify(issuer: any, profile: any, cb: any) {
		const logger: Logger = Container.get('logger');
		logger.info('profile: %o', profile);
		cb(null, {id: profile.id, name: profile.displayName})
	});
	
	passport.use(strategy);
	passport.serializeUser(function (user:any, cb) {
		process.nextTick(function () {
			cb(null, { id: user.id, username: user.username, name: user.name });
		});
	});
	
	passport.deserializeUser(function (user:any, cb) {
		process.nextTick(function () {
			return cb(null, user);
		});
	});

	// app.use(passport.authenticate('session'));
	app.use('/auth', route);

	route.get('/login',
		(req, res, next) => {
			res.render('login');
		});

	route.get(
		'/signin',
		// passport.authenticate('google-oidc-token'),
		passport.authenticate('google'),
		// async (req: Request, res: Response, next: NextFunction) => {
		// 	const logger: Logger = Container.get('logger');
		// 	logger.debug('Calling Sign-In endpoint with body: %o', req.body);
		// 	try {
		// 		// const authServiceInstance = Container.get(AuthService);
		// 		// const { user, token } = await authServiceInstance.SignIn(email, password);
		// 		// return res.json({ user, token }).status(200);
		// 		return res.status(200);
		// 	} catch (e) {
		// 		logger.error('🔥 error: %o', e);
		// 		return next(e);
		// 	}
		// },
	);


	route.get('/redirect/google', passport.authenticate('google', {
		successRedirect: '/',
		failureRedirect: '/api/auth/login'
	}))

	/**
	 * @TODO Let's leave this as a place holder for now
	 * The reason for a logout route could be deleting a 'push notification token'
	 * so the device stops receiving push notifications after logout.
	 *
	 * Another use case for advance/enterprise apps, you can store a record of the jwt token
	 * emitted for the session and add it to a black list.
	 * It's really annoying to develop that but if you had to, please use Redis as your data store
	 */
	route.post('/logout', middlewares.isAuth, (req: Request, res: Response, next: NextFunction) => {
	const logger:Logger = Container.get('logger');
	logger.debug('Calling Sign-Out endpoint with body: %o', req.body);
	try {
	//@TODO AuthService.Logout(req.user) do some clever stuff
	return res.status(200).end();
	} catch (e) {
	logger.error('🔥 error %o', e);
	return next(e);
	}
	});
};

