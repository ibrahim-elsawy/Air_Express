import IEntity from "./IEntity";

export default interface AccountEntity extends IEntity { 
	account_id: number;
	login: string;
	first_name: string;
	last_name: string;
	frequent_flyer_id?: number;
	update_ts?: Date;
	passwordhash: string;
};