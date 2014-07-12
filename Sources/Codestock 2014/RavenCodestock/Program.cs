using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Demo.Models.Commerce;
using Raven.Abstractions.Data;
using Raven.Client.Document;

namespace RavenCodestock
{
    class Program
    {
        static void Main(string[] args)
        {
            //Save();
            //Load();
            //Save2();
            //SaveABunchofStuff();
            //Query();
            QueryGroup();
            Console.ReadLine();
        }

        private static void Save()
        {
            using (var store = new DocumentStore() {Url = "http://localhost:8080", DefaultDatabase = "Codestock"})
            {
                store.Initialize(); // equivalent to connection.Open()
                using (var session = store.OpenSession())
                {
                    var prod = new Product() {Name = "Product", Category = "Product", Price = 100};
                    session.Store(prod);
                    session.SaveChanges();
                }
            }
        }

        private static void Save2()
        {
            using (var store = new DocumentStore() {Url = "http://localhost:8080", DefaultDatabase = "Codestock"})
            {
                store.Initialize(); // equivalent to connection.Open()
                using (var session = store.OpenSession())
                {
                    var prod = new Product() {Name = "Desk", Category = "Product", Price = 40};
                    prod.Id = string.Format("Products/{0}", prod.Name);
                    session.Store(prod, "products/notdesk");
                    session.SaveChanges();
                }
            }
        }

        private static void SaveABunchofStuff()
        {
            using (var store = new DocumentStore() {Url = "http://localhost:8080", DefaultDatabase = "Codestock"})
            {
                store.Initialize(); // equivalent to connection.Open()
                using (var session = store.OpenSession())
                {
                    var productList = new List<Product> {
                        new Product()
                        {
                            Name = "Stapler",
                            Category = "Office Supplies",
                            Price = 10,
                            Features = new [] {"Dangerous", "Shiny", "Staples"}.ToList()
                        },
                        new Product()
                        {
                            Name = "Tape",
                            Category = "Office Supplies",
                            Price = 2,
                            Features = new [] {"Shiny", "Sticky", "Dangerous"}.ToList()
                        },
                        new Product()
                        {
                            Name = "Paper",
                            Category = "Office Supplies",
                            Price = 20,
                            Features = new [] {"You can print on it", "Dangerous"}.ToList()
                        },
                        new Product()
                        {
                            Name = "Chair",
                            Category = "Furniture",
                            Price = 40,
                            Features = new [] {"Soft", "Dangerous", "You can sit in it"}.ToList()
                        },
                        new Product()
                        {
                            Name = "Desk",
                            Category = "Furniture",
                            Price = 750,
                            Features = new [] {"Shiny", "You can put stuff on it"}.ToList()
                        }
                    };

                    foreach (var product in productList)
                    {
                        session.Store(product, string.Format("products/{0}", product.Name));
                    }
                    session.SaveChanges();
                }
            }
        }

        private static void Load()
        {
            using (var store = new DocumentStore() {Url = "http://localhost:8080", DefaultDatabase = "Codestock"})
            {
                store.Initialize(); // equivalent to connection.Open()
                using (var session = store.OpenSession())
                {
                    var prod = session.Load<Product>(1);
                    Console.WriteLine(prod);
                }
            }
        }

        private static void Query()
        {
            using (var store = new DocumentStore() {Url = "http://localhost:8080", DefaultDatabase = "Codestock"})
            {
                store.Initialize(); // equivalent to connection.Open()
                using (var session = store.OpenSession())
                {
                    var products = session.Query<Product>("Auto/Products/ByCategoryAndPriceSortByPrice")
                        //.Customize(x => x.)
                        .Where(p => p.Category == "Office Supplies")
                        .OrderBy(p => p.Price);

                    foreach (var product in products)
                    {
                        Console.WriteLine(product);
                    }
                }
            }
        }

        private static void QueryGroup()
        {
            using (var store = new DocumentStore() {Url = "http://localhost:8080", DefaultDatabase = "Codestock"})
            {
                store.Initialize(); // equivalent to connection.Open()
                using (var session = store.OpenSession())
                {
                    var features = session.Query<FeatureQueryPoco>("Products/Features");
                    foreach (var feature in features)
                    {
                        Console.WriteLine("{0} - {1}", feature.Feature, feature.Count);
                    }
                }
            }
        }

        static void Stream()
        {
            using (var store = new DocumentStore() {Url = "http://localhost:8080", DefaultDatabase = })
            {
                store.Initialize();
                using (var session = store.OpenSession())
                {
                    var query = session.Query<Product>("Auto/Products/ByCategory") // or ProductsByCategory Index
                                       .Where(p => p.Category == "Bulk Goods");

                    var streamedResults = new List<Product>();

                    using (var stream = session.Advanced.Stream(query))
                    {
                        while (stream.MoveNext())
                        {
                            streamedResults.Add(stream.Current.Document);
                        }
                    }

                    Console.WriteLine("Found {0} products", streamedResults.Count);
                }
            }
        }


        public class FeatureQueryPoco

    {
            public string Feature { get; set; }
            public int Count { get; set; }
    }
    }
}
