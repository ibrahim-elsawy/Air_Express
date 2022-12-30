import dotenv from 'dotenv';

// Set the NODE_ENV to 'development' by default
process.env.NODE_ENV = process.env.NODE_ENV || 'development';

const envFound = dotenv.config();
if (envFound.error) {
  // This error should crash whole process

  throw new Error("⚠️  Couldn't find .env file  ⚠️");
}

export default {
  /**
   * Environment
   */
  ENV: process.env.NODE_ENV,

  /**
   * Your favorite port
   */
  port: parseInt((process.env.PORT)??"8080", 10),

  /**
   * postgress DB connection
   */
  postgresDB:{
    user: process.env.PGUSER,
    host: process.env.PGHOST,
    password: process.env.PGPASSWORD,
    database: process.env.PGDATABASE,
    port: parseInt(process.env.PGPORT ?? '5432', 10)
  },
  /**
   * Your secret sauce
   */
  jwtSecret: process.env.JWT_SECRET ?? "weak_default_secret",
  jwtAlgorithm: process.env.JWT_ALGO ?? '',

  /**
   * Used by winston logger
   */
  logs: {
    level: process.env.LOG_LEVEL || 'silly',
  },

  /**
   * Google Credentials
   */

  googleCredentials: {
    GOOGLE_CLIENT_ID: process.env.GOOGLE_CLIENT_ID || 'undefined',
    GOOGLE_CLIENT_SECRET: process.env.GOOGLE_CLIENT_SECRET || 'undefined'
  },


  /**
   * API configs
   */
  api: {
    prefix: '/api',
    redirectURL: '/redirect/google'
  },
  /**
   * Mailgun email credentials
   */
  
};
