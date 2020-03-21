# Задание к модулю ADO.Net.

# Общее

Нашей задачей является разработка части DAL (DataAccessLayer) к базе Northwind. При этом необходимо соблюдать ряд требований:

- DAL предоставляет объектный интерфейс. Т.е. данные которые он принимает и выдает на вход являются обычными [POCO](https://en.wikipedia.org/wiki/Plain_Old_CLR_Object)[(](https://en.wikipedia.org/wiki/Plain_Old_CLR_Object)[plain](https://en.wikipedia.org/wiki/Plain_Old_CLR_Object)[old](https://en.wikipedia.org/wiki/Plain_Old_CLR_Object)[CLR](https://en.wikipedia.org/wiki/Plain_Old_CLR_Object)[object](https://en.wikipedia.org/wiki/Plain_Old_CLR_Object)[)](https://en.wikipedia.org/wiki/Plain_Old_CLR_Object) объектами
- DAL должен быть спроектирован таким образом, чтобы его можно было легко заменить на fakes или mocks при модульном тестировании бизнес-слоя
- DAL должен поддерживать подключение к произвольной базе (строки подключения нигде не хардкодятся).
- DAL должен быть спроектирован максимально в providerindependent стиле. При этом должна быть оставлена возможность заменить некоторые методы на специфические для конкретной СУБД

Кроме того, при работе с базой Northwind мы будем исходить из следующего:

- Заказ (таблица Orders) может находится в следующих состояниях:
  - Новый (не отправленный), если его поле OrderDate == NULL
  - В работе (не выполненный), если поле ShippedDate == NULL
  - Выполненный, если ShippedDate != NULL
- В тестовых данных, которые поставляются с дистрибутивом Northwind, в поле Picture таблицы Categories, изображения хранятся в формате BMP, но первые 78 байт мусор, который надо удалить.

## О самом задании

Наше гипотетическое приложение – это рабочее место оператора, однако никакого UI мы создавать не будем, а для выполнения работы требуется:

- Создать сам DAL
- Создать набор тестов, демонстрирующих его (DAL) работу. Тесты работают непосредственно с базой, но отката базы на исходное состояние не требуется.

# Задание 1. Управление заказами

Реализуйте возможность через DAL управлять заказами:

1. Показывать список введенных заказов (таблица [Orders]). Помимо основных полей должны возвращаться:
  1. Статус заказа в виде Enum поля
2. Показывать подробные сведения о конкретном заказе, включая номенклатуру заказа (таблицы [Orders], [Order Details], и [Products], требуется извлекать как Id, так и название продукта)
3. Создавать новые заказы
4. Менять существующие заказы. При этом:
  1. Напрямую нельзя менять следующие поля
    1. Даты OrderDate и ShippedDate
    2. Статус заказа
  2. В заказе со статусом «Новый» можно менять любые поля и состав заказа, за исключением полей, перечисленных в пункте «а».
  3. В заказе со статусами «В работе» и «Выполненный» нельзя менять ничего
5. Удалять заказы со статусом «Новый» и «В работе».
6. Менять статус заказа. Для реализации этого пункта предлагается сделать специальные методы (именование выбирайте сами):
  1. Передать в работу: устанавливает дату заказа
  2. Пометить как выполненный: устанавливает ShippedDate
7. Получать статистику по заказам, используя готовые хранимые процедуры
  1. CustOrderHist
  2. CustOrdersDetail