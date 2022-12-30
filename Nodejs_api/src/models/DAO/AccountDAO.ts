import AccountEntity from '../Entity/AccountEntity';
import IAccountDAO from './IAccountDAO';
import { Container, Service } from "typedi";
import { Pool } from 'pg';
import Logger from '@/loaders/logger';
import { IUserInputDTO } from '@/interfaces/IUser';
import Filter from './coulmnFitrering';



const createDao = () => { 
	return new AccountDAO();
};

// @Service('acc.dao')
@Service({factory:createDao})
export default class AccountDAO implements IAccountDAO {

	private dbConn: Pool;

	constructor() { 

		// this.dbConn  = Container.get("DbConn");
		this.dbConn  = Container.get(Pool);
	}
		
	async creatAccount(user: IUserInputDTO): Promise<AccountEntity | null> {
		Logger.info(`Creating Account with email = ${user.email}`);
		try {
			const res = await this.dbConn.query<AccountEntity>(
				'INSERT INTO postgres_air.account (login, first_name, last_name, PasswordHash) VALUES ($1, $2, $3, $4) RETURNING *',
				[user.email, user.username, user.username, user.password]
			);
			console.log("..............%o", res.rows[0]);
			return res.rows[0];
		} catch (error) {
			Logger.error(error);
			return null;
		}
		
	}
;
	async getAccount(id: number): Promise<AccountEntity | null> {
		Logger.info(`About to excute query with account_id = ${id}`)
		const { rows } = await this.dbConn.query<AccountEntity>('SELECT * FROM postgres_air.account WHERE account_id = $1', [id]);
		if (rows.length === 0)
			return null

		Logger.info(`Query excuted succufully return with = ${rows[0]}`);
		return rows[0];
	}
	async getAccountByEmail(email: string): Promise<AccountEntity | null> {
		Logger.info(`About to excute query with account_id = ${email}`)
		const { rows } = await this.dbConn.query<AccountEntity>('SELECT * FROM postgres_air.account WHERE login= $1', [email]);
		if (rows.length === 0)
			return null
		Logger.info(`Query excuted succufully return with = ${rows[0]}`);
		return rows[0];
	}

	async geyByAnyCol(colName: keyof AccountEntity, filterValue: AccountEntity[keyof AccountEntity]): Promise<AccountEntity[] | null> {
		const filter = new Filter();
		const res = await filter.ByCloumn<AccountEntity>('account', colName, 'postgres_air', filterValue);
		return res;
		return null
	}
	

	async getAllAccounts(): Promise<AccountEntity[]> {
		const { rows } = await this.dbConn.query<AccountEntity>('SELECT * FROM postgres_air.account ORDER BY account_id ');
		return rows;
	}
	deleteAccount(id: number): void {
		throw new Error('Method not implemented.');
	}
	updateAccount(id: number): void {
		throw new Error('Method not implemented.');
	} 

};