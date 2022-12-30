import { Router, Request, Response } from 'express';
import middlewares from '../middlewares';
import { Container } from 'typedi';
import IDatabaseManager from '@/models/IDatabaseManager';
const route = Router();

export default (app: Router) => {
  app.use('/users', route);

  route.get('/:id', async(req: Request, res: Response) => {
    //call respository for return id of account
    const dbContext: IDatabaseManager = Container.get('dbContext');
    const account = await dbContext.userModel.geyByAnyCol('account_id', parseInt(req.params.id));
    if (account != null) {
      Reflect.deleteProperty(account[0], 'passwordhash');
      return res.json(account[0]);
    };
    return res.json(account);
  });
  route.get('/me', middlewares.isAuth, middlewares.attachCurrentUser, (req: Request, res: Response) => {
    // return res.json({ user: req.currentUser }).status(200);
  });
};
