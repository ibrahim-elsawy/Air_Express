FROM node:alpine

RUN adduser app;
USER app

WORKDIR /app

COPY ./package.json .
COPY ./build/ .

RUN npm install

EXPOSE 9092

CMD ["node", "app.js"]