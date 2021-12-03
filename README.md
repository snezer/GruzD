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
    <li>Метрики на Prometheus</li>
</ul> 
<h4>Особенность проекта в следующем:</h4>
<ul>
 <li>???</li>
 <li>???</li>
 <li>???</li>  
 </ul>
<h4>Основной стек технологий:</h4>
<ul>
	<li>HTML, CSS, JavaScript.</li>
	<li>Vue, Vuex, Vuetify.</li>
	<li>LESS, SASS, PostCSS.</li>
	<li>Yarn, Webpack, Babel.</li>
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
1) развертывание сервиса производилось на Windows-машине, однако может происходить и на linux-like средах, поддерживаемых .NET Core
2) требуется установленный  Docker и docker-compose для автоматизации развёртывания проекта;
3) Разворачиваем postgreSQL:
```
version: "3.7"
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
      - ME_CONFIG_BASICAUTH_PASSWORD=admin
    ports:
      - '8081:8081'
```
4) Простой ci процесс на bat:
```
sc stop kontaktormn
:loop
sc query kontaktormn | find "STOPPED"
if errorlevel 1 (
  timeout 1
  goto loop
)
taskkill /F /IM KONTAKTOR.Main.Service.exe

cd C:\Users\fofin\Documents\Work_2020\2021\hack\full-sources
git pull
cd C:\Users\fofin\Documents\Work_2020\2021\hack\full-sources\backend\kontaktor-network\kontaktor-network.Service\
dotnet publish -c Debug -o C:\Users\fofin\Documents\Work_2020\2021\hack\published\kontaktor-network

sc start kontaktormn
```
5) Имеет смысл зарегистрировать сервис через sc
 
```sc create kontaktormn "C:\Users\fofin\Documents\Work_2020\2021\hack\full-sources\backend\kontaktor-network\kontaktor-network.Service\KONTAKTOR.Main.Service.exe"```

СРЕДА ЗАПУСКА FRONTEND
------------
### Установка fronend
...
~~~
Для запуска проекта необходим глобально установленный yarn. Установить его можно с помощью команды npm install -g yarn
git clone https://github.com/snezer/Kontactor.Pro100.studio склонировать себе проект 
cd frontend  перейти в папку с фронтом
yarn install установит все зависимости для проекта 
yarn run serve запустит проект в режиме разработки получить доступ можно по адресу localhost:8080
yarn run build запусти процесс сборки проекта для продакшена
...
~~~


РАЗРАБОТЧИКИ

<h4>Мусихин Алексей Руководитель https://t.me/writer22rus</h4>
<h4>Фокин Максим Full-stack https://t.me/snezer</h4>
<h4>Грибков Александр Full-stack https://t.me/alex_gbk</h4>
<h4>Костенко Виталий Front-end https://t.me/kreolz3245</h4>
<h4>Намаканова Наиля Дизайн https://t.me/n_namakonova</h4>
