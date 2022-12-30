import AccountEntity from "../Entity/AccountEntity";
import { IUserInputDTO } from '@/interfaces/IUser';

export default interface IAccountDAO {

	creatAccount(user: IUserInputDTO): Promise<AccountEntity | null>
	getAccount(id: number): Promise<AccountEntity | null>
	getAccountByEmail(email: string): Promise<AccountEntity | null>
	geyByAnyCol(colName: keyof AccountEntity, filterValue: AccountEntity[keyof AccountEntity]): Promise<AccountEntity[] | null>
	getAllAccounts(): Promise<AccountEntity[]>
	deleteAccount(id: number): void
	updateAccount(id: number): void

};