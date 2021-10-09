using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Semana08
{

    class Program
    {
        public static DataClasses1DataContext context = new DataClasses1DataContext();

        static void Main(string[] args)
        {
            /*Console.WriteLine("Intro to linq");
            IntroToLINQ();
            Console.WriteLine("==========================================");

            Console.WriteLine("Datasource");
            DataSource();
            Console.WriteLine("==========================================");

            Console.WriteLine("Filtering");
            Filtering();
            Console.WriteLine("==========================================");

            Console.WriteLine("Ordering");
            Ordering();
            Console.WriteLine("==========================================");

            Console.WriteLine("Grouping");
            Grouping();
            Console.WriteLine("==========================================");

            Console.WriteLine("Grouping 2");
            Grouping2();
            Console.WriteLine("==========================================");

            Console.WriteLine("Joining");
            Joining();
            Console.WriteLine("==========================================");
            */
            Console.WriteLine("Intro to lambda");
            IntroToLambda();
            Console.WriteLine("==========================================");

            Console.WriteLine("Datasource");
            LambdaDataSource();
            Console.WriteLine("==========================================");

            Console.WriteLine("Filtering");
            FilteringLambda();
            Console.WriteLine("==========================================");

            Console.WriteLine("Ordering");
            OrderingLambda();
            Console.WriteLine("==========================================");

            Console.WriteLine("Grouping");
            GroupingLambda();
            Console.WriteLine("==========================================");

            Console.WriteLine("Grouping 2");
            Grouping2Lambda();
            Console.WriteLine("==========================================");

            Console.WriteLine("Joining");
            JoiningLambda();
            Console.WriteLine("==========================================");
            Console.ReadLine();

        }

        static void IntroToLINQ()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery = from num in numbers where (num % 2) == 0 select num;

            foreach (var num in numQuery)
            {
                Console.WriteLine("{0,1}", num);
            }
        }
        static void IntroToLambda()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var newN = numbers.Where(x => x % 2 == 0).ToList();
            Console.WriteLine(string.Join(" ", newN));

        }

        static void DataSource() {
            var queryAllCustomers = from cust in context.clientes select cust;
            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void LambdaDataSource()
        {
            var c = context.clientes;
            var result = c.Select(x => x);
            foreach (var item in result)
            {
                Console.WriteLine(item.NombreCompañia);
            }

        }

        static void Filtering()
        {
            var queryLondonCustomer = from cust in context.clientes where
                                    cust.Ciudad == "Londres" select cust;
            foreach (var item in queryLondonCustomer)
            {
                Console.WriteLine(item.Ciudad);
            }
        }

        static void FilteringLambda() {
            var c = context.clientes;
            var result = c.Where(x => x.Ciudad == "Londres").ToList();
            foreach (var item in result)
            {
                Console.WriteLine(item.Ciudad);
            }
        }
        static void Ordering()
        {
            var queryLondonCustomer3 = from cust in context.clientes
                                    where cust.Ciudad == "London"
                                    orderby cust.NombreCompañia ascending
                                    select cust;
            foreach (var item in queryLondonCustomer3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void OrderingLambda() { 
            var c = context.clientes;
            var result = c.Where(x => x.Ciudad == "Londres").OrderBy(x =>x.NombreCompañia).ToList();
            foreach (var item in result)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void Grouping()
        {
            var queryCustomerByCity = from cust in context.clientes
                                       group cust by cust.Ciudad;
            foreach (var customerGroup in queryCustomerByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine("{0}", customer.NombreCompañia);
                }
            }
        }
        static void GroupingLambda (){
            var c = context.clientes;
            var result = c.GroupBy(x => x.Ciudad).ToList();
            foreach (var customerGroup in result)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine("{0}", customer.NombreCompañia);
                }
            }
        }

        static void Grouping2()
        {
            var custQuery = from cust in context.clientes
                            group cust by cust.Ciudad into custGroup
                            where custGroup.Count() > 2
                            orderby custGroup.Key
                            select custGroup;

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }

        static void Grouping2Lambda()
        {
            var c = context.clientes;
            var result = c.GroupBy(x => x.Ciudad).Where(x => x.Count() > 2).OrderBy(x => x.Key).ToList();

            foreach (var item in result)
            {
                Console.WriteLine(item.Key);
            }
        }

        static void Joining()
        {
            var innerJoinQuery = from cust in context.clientes
                                 join dist in context.Pedidos on cust.idCliente
                                 equals dist.IdCliente
                                 select new { CustomerName = cust.NombreCompañia, 
                                     DistribuidorName = dist.PaisDestinatario };
            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }

        static void JoiningLambda()
        {

            var c = context.clientes;
            var result = c.Join(context.Pedidos,
                                cust => cust.idCliente,
                                dist => dist.IdCliente,
                                (cust,dist) => new
                                {
                                    CustomerName = cust.NombreCompañia,
                                    DistribuidorName = dist.PaisDestinatario
                                });
            foreach (var item in result)
            {
                Console.WriteLine(item.CustomerName);
            }
        }
    }
}
