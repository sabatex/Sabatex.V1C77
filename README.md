[![MIT License](https://img.shields.io/badge/license-MIT-red.svg)](https://github.com/sabatex/Extensions/blob/master/LICENSE.TXT)
![sabatex.V1C77](https://github.com/sabatex/1C77/workflows/sabatex.V1C77/badge.svg)
[![NuGet sabatex.V1C77](https://buildstats.info/nuget/sabatex.V1C77)](https://www.nuget.org/packages/sabatex.V1C77/)
![sabatex.V1C77.Models](https://github.com/sabatex/1C77/workflows/sabatex.V1C77.Models/badge.svg)
[![NuGet sabatex.V1C77.Models](https://buildstats.info/nuget/sabatex.V1C77.Models)](https://www.nuget.org/packages/sabatex.V1C77/)

# Web API для 1С7.7
 Данное API можна запустить для любой конфигурации 1С7.7.
 Что может API ?
   1. Читать Метаданные, Константы, Справочники,Документы ...
   2. API использует проверенную временем библиотеку для доступа по OLE (COM) к 1С7.7
			https://github.com/sabatex/1C77/src/sabatex.V1C77

Что нужно для запуска API?
   1. Windows 7 и выше;
   2. 1С7.7
   3. И всё ...

Как запустить ?

	1. Розпаковать в любую папку.
	2. Отредактировать файл appsettings.json заменив строчки -- **** -- на реальные не забывая
	что обратный слеш пишем два раза, например C:\\Users\\admin\\.1C\\MyCompany\\1C7.7
	3. Запускаєм WebApi1C.Server.exe
	4. Разрешаем доступ к порту 5000
	5. Запускаем браузер http://localhost:5000 или http://you ip:5000
