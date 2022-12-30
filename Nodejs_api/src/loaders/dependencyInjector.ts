import { Container } from 'typedi';
import LoggerInstance from './logger';
import { Pool } from 'pg';
import IDatabaseManager from '../models/IDatabaseManager';



interface IDependency { 
  pgConnection: Pool;
  dbContext: IDatabaseManager
};

export default ({ pgConnection, dbContext}: IDependency) => {
  try {

    Container.set('DbConn', pgConnection);

    Container.set('dbContext', dbContext);

    Container.set('logger', LoggerInstance);


  } catch (e) {
    LoggerInstance.error('🔥 Error on dependency injector loader: %o', e);
    throw e;
  }
};
