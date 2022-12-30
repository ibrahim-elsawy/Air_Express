import { Container, Service } from "typedi";
import { Pool } from 'pg';
import Logger from '@/loaders/logger';
import IEntity from '../Entity/IEntity';


export default class Filter { 

	private readonly dbConn: Pool = Container.get("DbConn");

	constructor() { 
		this.dbConn = Container.get("DbConn");
	};
	
	
	async ByCloumn<T extends IEntity>(tableName:string, colName:keyof T, schema:string="postgres_air", value: T[keyof T]) { 
		Logger.info(`About to excute query with ${tableName}.${String(colName)} = ${String(value)}`)
		const { rows } = await this.dbConn.query<T>(`SELECT * FROM ${schema}.${tableName} WHERE ${String(colName)} = $1`, [value]);
		if (rows.length === 0)
			return null

		Logger.info(`Query excuted succufully return with = ${rows}`);
		return rows;
	};
};