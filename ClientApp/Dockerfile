# Dockerfile
FROM node:10

WORKDIR /usr/src/app/ClientApp

COPY package*.json ./
# COPY ./* ./
# COPY ["ClientApp/*", "./ClientApp/"]

RUN npm install -g @angular/cli@v10-lts

EXPOSE 4200
# COPY . . 

CMD ["npm", "start"]