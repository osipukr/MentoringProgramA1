using System;
using System.Collections.Generic;
using System.Linq;
using SampleQueries.Data;

namespace SampleQueries
{
    [Title("06 LINQ")]
    [Prefix("Linq")]
    public class LinqSamples : SampleHarness
    {
        private const string METHOD_SYNTAX = "Method Syntax";
        private const string QUERY_SYNTAX = "Query Syntax";

        private readonly DataSource _dataSource;

        public LinqSamples()
        {
            _dataSource = new DataSource();
        }

        #region Task 1

        private const string TASK1_CATEGORY = "Task 1";
        private const string TASK1_DESCRIPTION = "Displays all customers with sum of orders total greater than X.";

        [Category(TASK1_CATEGORY)]
        [Title(METHOD_SYNTAX)]
        [Description(TASK1_DESCRIPTION)]
        public void Linq1()
        {
            IEnumerable<Customer> GetCustomers(decimal x)
            {
                return _dataSource.Customers.Where(customer => customer.Orders.Sum(order => order.Total) > x);
            }

            Task1HelperInternal(GetCustomers);
        }

        [Category(TASK1_CATEGORY)]
        [Title(QUERY_SYNTAX)]
        [Description(TASK1_DESCRIPTION)]
        public void Linq2()
        {
            IEnumerable<Customer> GetCustomers(decimal x)
            {
                return from customer in _dataSource.Customers
                       where customer.Orders.Sum(order => order.Total) > x
                       select customer;
            }

            Task1HelperInternal(GetCustomers);
        }

        private void Task1HelperInternal(Func<decimal, IEnumerable<Customer>> func)
        {
            var param = new[] { 10000, 60000 };

            foreach (var x in param)
            {
                ObjectDumper.Write($"The result for X = {x}");
                ObjectDumper.Write(func(x));
            }
        }

        #endregion

        #region Task 2

        private const string TASK2_CATEGORY = "Task 2";

        private const string TASK2_DESCRIPTION = "For each customer displays a list of suppliers from the same city and country.";

        [Category(TASK2_CATEGORY)]
        [Title(METHOD_SYNTAX)]
        [Description(TASK2_DESCRIPTION)]
        public void Linq3()
        {
            var resultFirst = _dataSource.Customers.Select(customer => new
            {
                Customer = customer,
                Suppliers = _dataSource.Suppliers.Where(supplier => supplier.City == customer.City &&
                                                                    supplier.Country == customer.Country)
            });

            ObjectDumper.Write("Without grouping:");
            ObjectDumper.Write(resultFirst, 2);


            var resultSecond = _dataSource.Customers.GroupJoin(_dataSource.Suppliers,
                customer => new
                {
                    customer.City,
                    customer.Country,
                },
                supplier => new
                {
                    supplier.City,
                    supplier.Country
                },
                (customer, supplier) => new
                {
                    Customer = customer,
                    Suppliers = supplier
                });

            ObjectDumper.Write(Environment.NewLine);
            ObjectDumper.Write("Without grouping:");
            ObjectDumper.Write(resultSecond, 2);
        }

        [Category(TASK2_CATEGORY)]
        [Title(QUERY_SYNTAX)]
        [Description(TASK2_DESCRIPTION)]
        public void Linq4()
        {
            var resultFirst = from customer in _dataSource.Customers
                              select new
                              {
                                  Customer = customer,
                                  Suppliers = from supplier in _dataSource.Suppliers
                                              where supplier.City == customer.City &&
                                                    supplier.Country == customer.Country
                                              select supplier
                              };

            ObjectDumper.Write("Without grouping:");
            ObjectDumper.Write(resultFirst, 2);


            var resultSecond = from customer in _dataSource.Customers
                               join supplier in _dataSource.Suppliers
                                   on new
                                   {
                                       customer.City,
                                       customer.Country
                                   }
                                   equals new
                                   {
                                       supplier.City,
                                       supplier.Country
                                   }
                                   into suppliers
                               select new
                               {
                                   Customer = customer,
                                   Suppliers = suppliers
                               };

            ObjectDumper.Write(Environment.NewLine);
            ObjectDumper.Write("Without grouping:");
            ObjectDumper.Write(resultSecond, 2);
        }

        #endregion

        #region Task 3

        private const string TASK3_CATEGORY = "Task 3";
        private const string TASK3_DESCRIPTION = "Displays all customers who has order with total greater than X.";

        [Category(TASK3_CATEGORY)]
        [Title(METHOD_SYNTAX)]
        [Description(TASK3_DESCRIPTION)]
        public void Linq5()
        {
            const decimal ordersTotal = 6000;

            var customers = _dataSource.Customers.Where(customer => customer.Orders.Any(order => order.Total > ordersTotal));

            ObjectDumper.Write(customers);
        }

        [Category(TASK3_CATEGORY)]
        [Title(QUERY_SYNTAX)]
        [Description(TASK3_DESCRIPTION)]
        public void Linq6()
        {
            const decimal ordersTotal = 6000;

            var customers = from customer in _dataSource.Customers
                            where customer.Orders.Any(order => order.Total > ordersTotal)
                            select customer;

            ObjectDumper.Write(customers);
        }

        #endregion

        #region Task 4

        private const string TASK4_CATEGORY = "Task 4";
        private const string TASK4_DESCRIPTION = "Displays all customers with their first orders month and year.";

        [Category(TASK4_CATEGORY)]
        [Title(METHOD_SYNTAX)]
        [Description(TASK4_DESCRIPTION)]
        public void Linq7()
        {
            var customers = _dataSource.Customers
                .Where(customer => customer.Orders.Any())
                .Select(customer => new
                {
                    CustomerId = customer.CustomerID,
                    StartDate = customer.Orders.Select(order => order.OrderDate).OrderBy(date => date).First()
                });

            ObjectDumper.Write(customers);
        }

        [Category(TASK4_CATEGORY)]
        [Title(QUERY_SYNTAX)]
        [Description(TASK4_DESCRIPTION)]
        public void Linq8()
        {
            var customers = from customer in _dataSource.Customers
                            where customer.Orders.Any()
                            select new
                            {
                                CustomerId = customer.CustomerID,
                                StartDate = (from order in customer.Orders
                                             orderby order.OrderDate
                                             select order.OrderDate).First()
                            };

            ObjectDumper.Write(customers);
        }

        #endregion

        #region Task 5

        private const string TASK5_CATEGORY = "Task 5";
        private const string TASK5_DESCRIPTION = "Displays all customers with their first orders month and year ordered by year, month, sum of orders total, clientName";

        [Category(TASK5_CATEGORY)]
        [Title(METHOD_SYNTAX)]
        [Description(TASK5_DESCRIPTION)]
        public void Linq9()
        {
            var customers = _dataSource.Customers
                .Where(customer => customer.Orders.Any())
                .Select(customer => new
                {
                    CustomerId = customer.CustomerID,
                    StartDate = customer.Orders.Select(order => order.OrderDate).OrderBy(date => date).First(),
                    TotalSum = customer.Orders.Sum(order => order.Total)
                })
                .OrderByDescending(customer => customer.StartDate.Year)
                .ThenByDescending(customer => customer.StartDate.Month)
                .ThenByDescending(customer => customer.TotalSum)
                .ThenByDescending(customer => customer.CustomerId)
                .Select(customer => customer);

            ObjectDumper.Write(customers);
        }

        [Category(TASK5_CATEGORY)]
        [Title(QUERY_SYNTAX)]
        [Description(TASK5_DESCRIPTION)]
        public void Linq10()
        {
            var customers = from customer in _dataSource.Customers
                            where customer.Orders.Any()
                            select new
                            {
                                CustomerId = customer.CustomerID,
                                StartDate = (from order in customer.Orders
                                             orderby order.OrderDate
                                             select order.OrderDate).First(),
                                TotalSum = customer.Orders.Sum(order => order.Total)
                            }
                            into customer
                            orderby customer.StartDate.Year descending,
                                customer.StartDate.Month descending,
                                customer.TotalSum descending,
                                customer.CustomerId descending
                            select customer;

            ObjectDumper.Write(customers);
        }

        #endregion

        #region Task 6

        private const string TASK6_CATEGORY = "Task 6";
        private const string TASK6_DESCRIPTION = "Displays all customers with not number postal code or without region or whithout operator's code.";

        [Category(TASK6_CATEGORY)]
        [Title(METHOD_SYNTAX)]
        [Description(TASK6_DESCRIPTION)]
        public void Linq11()
        {
            var customers = _dataSource.Customers.Where(customer =>
                string.IsNullOrWhiteSpace(customer.PostalCode) ||
                customer.PostalCode.Any(symbol => !char.IsDigit(symbol)) ||
                string.IsNullOrWhiteSpace(customer.Region) ||
                customer.Phone.StartsWith("("));

            ObjectDumper.Write(customers);
        }

        [Category(TASK6_CATEGORY)]
        [Title(QUERY_SYNTAX)]
        [Description(TASK6_DESCRIPTION)]
        public void Linq12()
        {
            var customers = from customer in _dataSource.Customers
                            where string.IsNullOrWhiteSpace(customer.PostalCode) ||
                                  customer.PostalCode.Any(symbol => !char.IsDigit(symbol)) ||
                                  string.IsNullOrWhiteSpace(customer.Region) ||
                                  customer.Phone.StartsWith("(")
                            select customer;

            ObjectDumper.Write(customers);
        }

        #endregion

        #region Task 7

        private const string TASK7_CATEGORY = "Task 7";
        private const string TASK7_DESCRIPTION = "Groups products by categories then by units in stock > 0  then order by unitPrice.";

        [Category(TASK7_CATEGORY)]
        [Title(METHOD_SYNTAX)]
        [Description(TASK7_DESCRIPTION)]
        public void Linq13()
        {
            var result = _dataSource.Products
                .GroupBy(product => product.Category,
                        (category, products) => new
                        {
                            Category = category,
                            Products = products.GroupBy(product => product.UnitsInStock > 0)
                                .Select(group => new
                                {
                                    InStock = group.Key,
                                    Products = group.OrderBy(product => product.UnitPrice).Select(product => product)
                                })
                        });

            ObjectDumper.Write(result, 2);
        }

        [Category(TASK7_CATEGORY)]
        [Title(QUERY_SYNTAX)]
        [Description(TASK7_DESCRIPTION)]
        public void Linq14()
        {
            var result = from product in _dataSource.Products
                         group product by product.Category
                         into categoryProductsGroup
                         select new
                         {
                             Category = categoryProductsGroup.Key,
                             Products = from product in categoryProductsGroup
                                        group product by product.UnitsInStock > 0
                                        into existProductGroup
                                        select new
                                        {
                                            InStock = existProductGroup.Key,
                                            Products = from product in existProductGroup
                                                       orderby product.UnitPrice
                                                       select product
                                        }
                         };

            ObjectDumper.Write(result, 2);
        }

        #endregion

        #region Task 8

        private const string TASK8_CATEGORY = "Task 8";
        private const string TASK8_DESCRIPTION = "Groups products by price: Cheap, Average price, Expensive.";

        [Category(TASK8_CATEGORY)]
        [Title(METHOD_SYNTAX)]
        [Description(TASK8_DESCRIPTION)]
        public void Linq15()
        {
            const decimal lowAverageBoundary = 20;
            const decimal averageExpensiveBoundary = 50;

            var categories = _dataSource.Products
                .GroupBy(product => product.UnitPrice switch
                {
                    var unitPrice when unitPrice < lowAverageBoundary => "Cheap",
                    var unitPrice when unitPrice < averageExpensiveBoundary => "Average price",
                    _ => "Expensive"
                })
                .Select(productGroups => new
                {
                    Category = productGroups.Key,
                    Products = productGroups.OrderBy(product => product.UnitPrice).Select(product => product)
                });

            ObjectDumper.Write(categories, 1);
        }

        [Category(TASK8_CATEGORY)]
        [Title(QUERY_SYNTAX)]
        [Description(TASK8_DESCRIPTION)]
        public void Linq16()
        {
            const decimal lowAverageBoundary = 20;
            const decimal averageExpensiveBoundary = 50;

            var categories = from product in _dataSource.Products
                             group product by product.UnitPrice switch
                             {
                                 var unitPrice when unitPrice < lowAverageBoundary => "Cheap",
                                 var unitPrice when unitPrice < averageExpensiveBoundary => "Average price",
                                 _ => "Expensive"
                             }
                             into productGroups
                             select new
                             {
                                 Category = productGroups.Key,
                                 Products = from product in productGroups
                                            orderby product.UnitPrice
                                            select product
                             };


            ObjectDumper.Write(categories, 1);
        }

        #endregion

        #region Task 9

        private const string TASK9_CATEGORY = "Task 9";
        private const string TASK9_DESCRIPTION = "Counts average order sum for and average client's intensity for every city.";

        [Category(TASK9_CATEGORY)]
        [Title(METHOD_SYNTAX)]
        [Description(TASK9_DESCRIPTION)]
        public void Linq17()
        {
            var cities = _dataSource.Customers.GroupBy(
                customer => customer.City,
                (city, customers) => new
                {
                    City = city,
                    AverageIncome = customers.SelectMany(customer => customer.Orders).Average(order => order.Total),
                    AverageIntensity = customers.Average(customer => customer.Orders.Length)
                });

            ObjectDumper.Write(cities);
        }

        [Category(TASK9_CATEGORY)]
        [Title(QUERY_SYNTAX)]
        [Description(TASK9_DESCRIPTION)]
        public void Linq18()
        {
            var cities = from customer in _dataSource.Customers
                         group customer by customer.City
                         into customersGroup
                         select new
                         {
                             City = customersGroup.Key,
                             AverageIncome = (from customer in customersGroup
                                              from order in customer.Orders
                                              select order).Average(order => order.Total),
                             AverageIntensity = customersGroup.Average(customer => customer.Orders.Length)
                         };

            ObjectDumper.Write(cities);
        }

        #endregion

        #region Task 10

        private const string TASK10_CATEGORY = "Task 10";
        private const string TASK10_DESCRIPTION = "Displays clients activity statistic by month (without year), by year and by year and month.";

        [Category(TASK10_CATEGORY)]
        [Title(METHOD_SYNTAX)]
        [Description(TASK10_DESCRIPTION)]
        public void Linq19()
        {
            var statistic = _dataSource.Customers.Select(customer => new
            {
                CustomerId = customer.CustomerID,
                MonthsStatistic = customer.Orders.GroupBy(
                    order => order.OrderDate.Month,
                    (month, orders) => new
                    {
                        Month = month,
                        OrdersCount = orders.Count()
                    }),
                YearsStatistic = customer.Orders.GroupBy(
                    order => order.OrderDate.Year,
                    (year, orders) => new
                    {
                        Year = year,
                        OrdersCount = orders.Count()
                    }),
                YearAndMonthStatistic = customer.Orders.GroupBy(
                    order => new
                    {
                        order.OrderDate.Year,
                        order.OrderDate.Month
                    },
                    (yearAndMonth, orders) => new
                    {
                        yearAndMonth.Year,
                        yearAndMonth.Month,
                        OrdersCount = orders.Count()
                    })
            });

            ObjectDumper.Write(statistic, 2);
        }

        [Category(TASK10_CATEGORY)]
        [Title(QUERY_SYNTAX)]
        [Description(TASK10_DESCRIPTION)]
        public void Linq20()
        {
            var statistic = from customer in _dataSource.Customers
                            select new
                            {
                                CustomerId = customer.CustomerID,
                                MonthsStatistic = from order in customer.Orders
                                                  group order by order.OrderDate.Month
                                                  into ordersGroup
                                                  select new
                                                  {
                                                      Month = ordersGroup.Key,
                                                      OrdersCount = ordersGroup.Count()
                                                  },
                                YearsStatistic = from order in customer.Orders
                                                 group order by order.OrderDate.Year
                                                 into ordersGroup
                                                 select new
                                                 {
                                                     Year = ordersGroup.Key,
                                                     OrdersCount = ordersGroup.Count()
                                                 },
                                YearAndMonthStatistic = from order in customer.Orders
                                                        group order by new
                                                        {
                                                            order.OrderDate.Year,
                                                            order.OrderDate.Month
                                                        }
                                                        into ordersGroup
                                                        select new
                                                        {
                                                            ordersGroup.Key.Year,
                                                            ordersGroup.Key.Month,
                                                            OrdersCount = ordersGroup.Count()
                                                        }
                            };

            ObjectDumper.Write(statistic, 2);
        }

        #endregion
    }
}