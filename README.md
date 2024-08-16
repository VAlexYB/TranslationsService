TranslationsService - приложение, реализованное в рамках тестового задания на вакансию .Net/C# Middle+ разработчика

Тестовое задание
Написать сервис перевода, использующий Google Translate API (до полумиллиона символов там бесплатно) или любое иное внешнее API. 
(В процессе выполнения выяснилось, что такого бесплатного API нет, было решено использовать простое преобразование строк)
a) Сервис должен принимать список строк, с какого языка, на какой, и выдавать список переводов.
b) Сервис должен кэшировать уже сделанные переводы любым способом (память, EF+бд, Redis, ElasticSearch, любое иное средство по желанию).
Сделать три вида доступа к сервису:
a) консольная утилита (вводим текст, языки, получаем результат).
b) gRPC-сервис.
c) ASP.NET Core REST API сервис (контроллеры, Request-Endpoint-Response, Minimal API, или любой иной подход — на ваше усмотрение).
Сделать общий интерфейс с двумя методами: информация о сервисе (выдать применяемый внешний сервис и тип/объем кэша) и перевод. Сделать три его реализации:
a) Прямое обращение к сервису (или может сам сервис просто реализовывать интерфейс и подключаться через DI).
b) Через gRPC-клиент.
c) Через REST-клиент.