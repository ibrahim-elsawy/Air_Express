import AccountEntity from "../Entity/AccountEntity";
import IAccountRepo from "./IAccountRepo";
import IAccountDAO from '../DAO/IAccountDAO';
import { Container, Inject, Service } from 'typedi';
import AccountDAO from '../DAO/AccountDAO';
import { IUserInputDTO } from "@/interfaces/IUser";


const createRepo = () => { 
	return new AccountRepo()
};


@Service({factory:createRepo})
export default class AccountRepo implements IAccountRepo {

	
	
	dao: IAccountDAO = Container.get(AccountDAO);

	async createAccount(account: IUserInputDTO): Promise<AccountEntity | null> { 
		try {
			const acc = await this.dao.creatAccount(account);
			return acc;
		} catch (error) {
			return null;
		}
	};
	async getAccount(id: number): Promise<AccountEntity | null> {
		const acc = await this.dao.getAccount(id);
		return acc ? acc : null;
	}

	async geyByAnyCol(colName: keyof AccountEntity, filterValue: AccountEntity[keyof AccountEntity]): Promise<AccountEntity[] | null> {
		const res = await this.dao.geyByAnyCol(colName, filterValue);
		return res;
	}

	getAllAccounts(): Promise<AccountEntity[]> {
		throw new Error("Method not implemented.");
	}
	deleteAccount(id: number): void {
		throw new Error("Method not implemented.");
	}
	updateAccount(id: number): void {
		throw new Error("Method not implemented.");
	}

	async findAccountByEmail(email: string): Promise<AccountEntity | null>{
		const acc = await this.dao.getAccountByEmail(email);
		return (acc) ? acc : null
	}

};