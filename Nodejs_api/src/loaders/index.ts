import expressLoader from './express';
import dependencyInjectorLoader from './dependencyInjector';
import Logger from './logger';
//We have to import at least all the events once so they can be triggered
import * as core from 'express-serve-static-core';
import conn from './databaseConn';
import DbContext from '../models/databaseManager';


interface IApp { 
  expressApp: core.Express
};

export default ({ expressApp }: IApp) => async () => {
  const pgConnection = conn();
  // const x = await pgConnection.connect();
  // const s = await x.query('SELECT * FROM postgres_air.account where account_id=$1', [4]);
  
  // Logger.info('✌️ DB loaded and connected! %o', s.rows[0]);
  
  Logger.info('✌️ DB loaded and connected!');

  /**
   * WTF is going on here?
   *
   * We are injecting the mongoose models into the DI container.
   * I know this is controversial but will provide a lot of flexibility at the time
   * of writing unit tests, just go and check how beautiful they are!
   */

  // const userModel = {
  //   name: 'userModel',
  //   // Notice the require syntax and the '.default'
  //   model: ,
  // };

  dependencyInjectorLoader({
    pgConnection,
    dbContext: new DbContext()
    // models: [
    //   userModel,
    //   // salaryModel,
    //   // whateverModel
    // ],
  });
  Logger.info('✌️ Dependency Injector loaded');


  expressLoader({ app: expressApp });
  Logger.info('✌️ Express loaded');
};
