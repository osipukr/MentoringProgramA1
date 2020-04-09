# Задание #1 к модулю HTTPfundamentals

# Задание

Необходимо реализовать библиотеку и использующую её консольную программу для создания локальной копии сайта («аналог» программы [wget](https://ru.wikipedia.org/wiki/Wget)).

Работа с программой выглядит так: пользователь указывает стартовую точку (URL) и папку куда надо сохранять, а программа проходит по всем доступным ссылкам и рекурсивно выкачивает сайт(ы).

Опции программы/библиотеки:

- ограничение на глубину анализа ссылок (т.е. если вы скачали страницу, которую указал пользователь, это уровень 0, все страницы на которые введут ссылки с неё, это уровень 1 и т.д.)
- ограничение на переход на другие домены (без ограничений/только внутри текущего домена/не выше пути в исходном URL)
- ограничение на «расширение» скачиваемых ресурсов (можно задавать списком, например так: gif,jpeg,jpg,pdf)
- трассировка (verbose режим): показ на экране текущей обрабатываемой страницы/документа

# Рекомендации по реализации

В качестве основы можно взять следующие библиотеки:

- работа с HTTP
  - System.Net.Http.HttpClient – рекомендуемый вариант
    - Если вы работаете с .Net 4.5 + он включен в сам фреймворк. В более ранних версиях и для прочих платформ получите через [NuGet](https://www.nuget.org/packages/Microsoft.Net.Http)
    - Введение в работу с ним можно найти тут [https://blogs.msdn.microsoft.com/henrikn/2012/02/16/httpclient-is-here/](https://blogs.msdn.microsoft.com/henrikn/2012/02/16/httpclient-is-here/)
    - Обратите внимание – он весь построен на асинхронных операциях (но мы можем работать в синхронном режиме!)
  - Net.HttpWebRequest – legacy
- Работа с HTML
  - Можно воспользоваться одной из библиотек, перечисленных [тут](http://ru.stackoverflow.com/questions/420354/%D0%9A%D0%B0%D0%BA-%D1%80%D0%B0%D1%81%D0%BF%D0%B0%D1%80%D1%81%D0%B8%D1%82%D1%8C-html-%D0%B2-net/450586)
  - Самый популярный вариант HtmlAgilityPack, хотя он достаточно и старый и имеет свои проблемы.
  
  
  


# Задание #2 к модулю HTTPfundamentals

# Задание

Написать HTTPhandler, генерирующий отчет по заказам в базе Northwind.

Handler должен уметь:

- Принимать параметры:
  - в виде строки запроса (т.е. вида [http](http://host/Report?param1=value1&amp;param1)[://](http://host/Report?param1=value1&amp;param1)[host](http://host/Report?param1=value1&amp;param1)[/](http://host/Report?param1=value1&amp;param1)[Report](http://host/Report?param1=value1&amp;param1)[?](http://host/Report?param1=value1&amp;param1)[param](http://host/Report?param1=value1&amp;param1)[1=](http://host/Report?param1=value1&amp;param1)[value](http://host/Report?param1=value1&amp;param1)[1&amp;param1](http://host/Report?param1=value1&amp;param1)=...)
  - или в теле запроса (в формате application/x-www-form-urlencoded)
- Поддерживать параметры:
  - customer – ID заказчика
  - dateFrom / dateTo – период дат на которые нужно выдать заказы (заказы фильтруются по дате OrderDate включительно). Может быть указан только 1 параметер
  - take – сколько заказов вернуть.
  - skip – сколько заказов пропустить

**В отчете заказы упорядочиваются по**  **OrderID****!**

Любой из перечисленных параметров может быть пропущен, в этом случае определяемое им условие выборки не применяется.

- Анализировать заголовок Accept и возвращать отчет в следующих форматах

| Accept | Формат |
| --- | --- |
| application/vnd.openxmlformats-officedocument.spreadsheetml.sheet | Excel (.xlsx) |
| text/xmlapplication/xml | XML |
| Любой другой или вовсе отсутствует | Excel (.xlsx) |

- Указывать в ответе правильный Content-Type (так, чтобы при открытии из браузера – открывался нужный редактор/просмотрщик)

Для тестирования handler напишите набор интеграционных тестов, работающих поверх HttpClient и проверяющих различные варианты вызова handler.

# Замечания по реализации

Состав возвращаемых из базы Order полей, а также структуру XML – определяете сами.

Для формирования Excel можно использовать

- [OpenXML SDK](https://www.nuget.org/packages/DocumentFormat.OpenXml/)
- Но рекомендуется воспользоваться более простым [ClosedXML](https://github.com/closedxml/closedxml)