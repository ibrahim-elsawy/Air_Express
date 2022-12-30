import { Pool } from 'pg';
import config from '@/config';


export default () => {
	const pool = new Pool({
		user: config.postgresDB.user,
		host: config.postgresDB.host,
		database: config.postgresDB.database,
		password: config.postgresDB.password,
		port: config.postgresDB.port,
	});
	return pool;
};