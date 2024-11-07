FROM node:alpine

WORKDIR /usr/src/app

COPY . /usr/src/app

RUN npm install -g @angular/cli
RUN npm install -g serve

RUN npm install

RUN npm run build

EXPOSE 80

CMD ["npx", "serve", "dist/p2-i", "-p", "80"]