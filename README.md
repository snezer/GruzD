<h1>ГрузD</h1>
<h4>Реализованная функциональность</h4>
<ul>
    <li>Полнофункциональный бэк с видимыми эндпойнтами в swagger</li>
    <li>DAL на PostgreSQL, который позволил очень быстро прототипировать;</li>    
    <li>UI-клиент на vue.js</li>
	<li>Аутентификация/авторизация пользователей;</li>
	<li>Рабочая ролевая модель;</li>
    <li>Функционал работы со списками поставщиков;</li>
	<li>Функционал работы с сущностями поставок (включая список планируемого транспорта к поставке);</li>
    <li>Функционал учета собственного транспорта компании;</li>
    <li>Событийная модель от агентов распознавания;</li>
    <li>Интерфейс эмуляции работы крановщика на UI;</li>
    <li>Интерфейс создания событий на мобильной UI;</li>
    <li>Распознавание номеров транспорта/брака при генерации сообщений;</li>
    <li>Интерфейс работника ОТК;</li> 
	<li>Автоматическая генерация ддокументации, связанной с доставкой, получением сырья, выявением брака;</li>
    <li>Метрики на Prometheus</li>
</ul> 
<h4>Особенность проекта в следующем:</h4>
<ul>
 <li>Система реализации автоматизированной сортировочной станции;</li>
 <li>Распознавание номеров вагоном и машин;</li>
 <li>Ссылка на обученную модель и блокноты GoogleCollab: https://drive.google.com/drive/folders/15Zpaayjj5Euc8pgCuubzEM_ySLzi5Apr?usp=sharing</li>	
 <li>Автоматическая генерация логистической документации;</li>  
 </ul>
<h4>Основной стек технологий:</h4>
<ul>
	<li>HTML, CSS, TypeScript.</li>
	<li>Vue, Vuex, PrimeVue.</li>
	<li>LESS, SASS, PostCSS.</li>
	<li>NPM, Webpack, Babel.</li>
	<li>Git, GitHub.</li>
	<li>PostgreSQL</li>
	<li>.NET Core 3.1, Prometheus, Dapper, EF Core, Log4Net</li>
	<li>ASP Core, Swashbuckle/Swagger </li>
	<li>Docker, Docker-Compose</li>  
 </ul>
<h4>Демо</h4>
<p>Демо сервиса доступно по адресу: https://f506-89-232-75-108.ngrok.io/swagger/index.html </p>
<p>Демо UI системы по адресу: https://kreolz.github.io/#/ </p>
<p>Реквизиты администратора : логин: admin пароль: Pa55w0rd#</p>


СРЕДА ЗАПУСКА BACKEND
------------
0) клонирование репозитория ```git clone https://github.com/snezer/GruzD```
1) развертывание сервиса производилось на Windows-машине, однако может происходить и на linux-like средах, поддерживаемых .NET Core
2) требуется установленный  Docker и docker-compose для автоматизации развёртывания проекта;
3) Разворачиваем postgreSQL и mongoDB:
```
version: "3.7"
volumes:
    postgres:
    pgadmin:

services:
  mongodb:
    container_name: mongo-dev
    image: mongo
    environment:
      - MONGO_INITDB_ROOT_USERNAME=admin
      - MONGO_INITDB_DATABASE=auth
      - MONGO_INITDB_ROOT_PASSWORD=pass
    ports:
      - '27017:27017'
  mongo-express:
    container_name: mongo-express
    image: mongo-express
    depends_on:
      - mongodb
    environment:
      - ME_CONFIG_MONGODB_ADMINUSERNAME=admin
      - ME_CONFIG_MONGODB_ADMINPASSWORD=pass
      - ME_CONFIG_MONGODB_SERVER=mongo-dev
      - ME_CONFIG_BASICAUTH_USERNAME=admin
      - ME_CONFIG_BASICAUTH_PASSWORD=ihavealongpassword
    ports:
      - '8081:8081'
      
    postgres:
        container_name: postgres
        image: "postgres:latest"
        environment:
          POSTGRES_USER: "admin"
          POSTGRES_PASSWORD: "pass"
          PGDATA: "/data/postgres"
        volumes:
           - postgres:/data/postgres
           - ./docker_postgres_init.sql:/docker-entrypoint-initdb.d/docker_postgres_init.sql
        ports:
          - "15432:5432"
        restart: unless-stopped
      pgadmin:
        container_name: pgadmin
        image: "dpage/pgadmin4:4.24"
        depends_on:
            - postgres
        environment:
          PGADMIN_DEFAULT_EMAIL: admin
          PGADMIN_DEFAULT_PASSWORD: ihavealongpassword
          PGADMIN_CONFIG_SERVER_MODE: "False"
          PGADMIN_CONFIG_MASTER_PASSWORD_REQUIRED: "False"
        volumes:
           - pgadmin:/var/lib/pgadmin
           - ./docker_pgadmin_servers.json:/pgadmin4/servers.json
        ports:
          - "15433:80"
        entrypoint:
          - "/bin/sh"
          - "-c"
          - "/bin/echo 'postgres:5432:*:postgres:password' > /tmp/pgpassfile && chmod 600 /tmp/pgpassfile && /entrypoint.sh"
        restart: unless-stopped    
```
4) Простой ci процесс на bat:
```
sc stop gruzdapi
:loop
sc query gruzdapi | find "STOPPED"
if errorlevel 1 (
  timeout 1
  goto loop
)
taskkill /F /IM GruzD.API.Service.exe

cd c:\HACK2021FINAL\ACCENTURE\
git pull
cd c:\HACK2021FINAL\ACCENTURE\Backend\GruzD\GruzD.API.Service\ 
dotnet publish -c Debug -o c:\HACK2021FINAL\gruzdapi

sc start gruzdapi

echo Service updated
pause
```
5) Имеет смысл зарегистрировать сервис через sc
 
```sc create gruzdapi binPath="c:\HACK2021FINAL\gruzdapi\GruzD.API.Service.exe"```

СРЕДА ЗАПУСКА FRONTEND
------------
### Установка frontend
...
~~~
0) клонирование репозитория ```git clone https://github.com/snezer/GruzD```
1) cd frontend  перейти в папку с фронтом
3) npm install установит все зависимости для проекта 
4) npm run serve запустит проект в режиме разработки получить доступ можно по адресу localhost:8080
5) npm run build запустит процесс сборки проекта для продакшена
...
~~~


РАЗРАБОТЧИКИ

<h4>Мусихин Алексей Руководитель/Анализ https://t.me/writer22rus</h4>
<h4>Фокин Максим Full-stack/Мобильная разработка https://t.me/snezer</h4>
<h4>Грибков Александр Архитектура/Анализ/Full-stack разработка https://t.me/alex_gbk</h4>
<h4>Костенко Виталий Front-end разработка https://t.me/kreolz3245</h4>
<h4>Намакoнова Наиля Дизайн https://t.me/n_namakonova</h4>
