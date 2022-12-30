import express, { NextFunction, Request, Response } from 'express';
import cors from 'cors';
import { OpticMiddleware } from '@useoptic/express-middleware';
import routes from '@/api';
import config from '@/config';
import session from 'express-session';
import RedisConn from 'connect-redis';
import Redis from 'ioredis';
import cookieParser from 'cookie-parser';
import passport from 'passport';


export default ({ app }: { app: express.Application }) => {
  
  
  const RedisStore = RedisConn(session);
  const redisClient = new Redis;

// view engine setup
  console.log(__dirname);
  // app.set('views', path.join(__dirname, '../views'));
  app.set('views', '././views');
  app.set('view engine', 'ejs');


  /**
   * Health Check endpoints
   * @TODO Explain why they are here
   */
  app.get('/status', (req, res) => {
    res.status(200).end();
  });
  app.head('/status', (req, res) => {
    res.status(200).end();
  });

  // Useful if you're behind a reverse proxy (Heroku, Bluemix, AWS ELB, Nginx, etc)
  // It shows the real origin IP in the heroku or Cloudwatch logs
  app.enable('trust proxy');

  // The magic package that prevents frontend developers going nuts
  // Alternate description:
  // Enable Cross Origin Resource Sharing to all origins by default
  app.use(cors());

  // Transforms the raw string of req.body into json
  app.use(express.json());

  app.use(express.urlencoded({ extended: false }));
  app.use(cookieParser());
  // app.use(express.static(path.join(__dirname, '../public')));
  app.use(express.static('././public'));
  // app.use(csrf());
  // Restoring sessions in Redis server
  app.use(
  session({
    secret: config.jwtSecret,
    // name: "sid",
    store: new RedisStore({ client: redisClient }),
    resave: false,
    saveUninitialized: false,
    // cookie: {
    //   secure: config.ENV === "production" ? true : "auto",
    //   httpOnly: true,
    //   expires: new Date(1000 * 60 * 60 * 24 * 7) ,
    //   sameSite: config.ENV === "production" ? "none" : "lax",
    // },
  })
  );
  app.use(passport.authenticate('session'));
  // Load API routes
  app.use(config.api.prefix, routes());

  // API Documentation
  app.use(OpticMiddleware({
      enabled: process.env.NODE_ENV !== 'production',
  }));

  /// catch 404 and forward to error handler
  app.use((req, res, next) => {
    const err = new Error('Not Found');
    err.name = "not-found";
    next(err);
  });

  /// error handlers
  app.use((err:Error, req: Request, res: Response, next:NextFunction) => {
    /**
     * Handle 401 thrown by express-jwt library
     */
    if (err.name === 'UnauthorizedError') {
      return res
        .status(401)
        .send({ message: err.message })
        .end();
    }
    return next(err);
  });
  app.use((err:Error, req: Request, res: Response, next: NextFunction) => {
    res.status((err.name==="not-found")? 400 : 500);
    res.json({
      errors: {
        message: err.message,
      },
    });
  });
};
