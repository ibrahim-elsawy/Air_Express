import AccountEntity from "../Entity/AccountEntity"
import { IUserInputDTO } from '@/interfaces/IUser';

export default interface IAccountRepo{

	createAccount(account: IUserInputDTO):Promise<AccountEntity | null>
	getAccount(id: number): Promise<AccountEntity | null>
	geyByAnyCol(colName:keyof AccountEntity, filterValue:AccountEntity[keyof AccountEntity]): Promise<AccountEntity[] | null>
	getAllAccounts(): Promise<AccountEntity[]>
	deleteAccount(id: number): void
	updateAccount(id: number): void
	findAccountByEmail(email: string): Promise<AccountEntity | null>
};