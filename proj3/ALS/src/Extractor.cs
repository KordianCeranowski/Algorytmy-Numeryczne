using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace RecommenderSystem
{
    internal class Extractor
    {
        private readonly bool CONSOLE_OUTPUT = false;
        public static readonly int DAWKA_PLIKU = 550000;
        private readonly List<ItemFAT> All;

        public static RMatrix createR(int iloscProduktow, int iloscUserow)
        {
            Extractor e = new Extractor();

            return e.extractRfromBooks(iloscProduktow, iloscUserow);
        }

        public Extractor()
        {
            All = AmazonReader.ReadFile(DAWKA_PLIKU);
            if (CONSOLE_OUTPUT)
            {
                Console.WriteLine("Successfully parsed " + All.Count + " items");
            }
        }

        public RMatrix extractRfromBooks(int iloscProduktow, int iloscUserow)
        {
            Pigeonhole books = new Pigeonhole();

            foreach (ItemFAT item in All)
            {
                if (item.Group == "Book")
                {
                    books.items.Add(new Item(item.ItemID, item.Rates));
                }
            }
            if (CONSOLE_OUTPUT)
            {
                Console.WriteLine("Successfully extracted " + books.items.Count + " Books");
            }

            books.extractBestItems(iloscProduktow);
            books.extractBestUsers(iloscUserow);

            if (CONSOLE_OUTPUT)
            {
                Console.WriteLine("\t" + books.items.Count + " Products");
                Console.WriteLine("\t" + books.users.Count + " Users");
                Console.WriteLine("\t{0:N2} % Matrix Filled", books.GetCovering() * 100);
            }

            return books.extractToR();
        }


        public void showCoverageByCategory(int iloscProduktowMacierzyR)
        {
            Dictionary<string, Pigeonhole> Groups = new Dictionary<string, Pigeonhole>();

            foreach (ItemFAT item in All)
            {
                if (item.Group != null)
                {
                    if (!Groups.ContainsKey(item.Group))
                    {
                        Groups.Add(item.Group, new Pigeonhole());
                    }
                    Groups[item.Group].items.Add(new Item(item.ItemID, item.Rates));
                }
            }
            Console.WriteLine("Found " + Groups.Count + " groups within items");

            foreach (KeyValuePair<string, Pigeonhole> category in Groups)
            {
                category.Value.extractBestItems(iloscProduktowMacierzyR);
            }

            foreach (KeyValuePair<string, Pigeonhole> category in Groups)
            {
                Console.WriteLine(category.Key + ":");
                Console.WriteLine("\t" + category.Value.items.Count + " Products");
                Console.WriteLine("\t" + category.Value.users.Count + " Users");
                Console.WriteLine("\t{0:N2} % Matrix Filled", category.Value.GetCovering() * 100);
            }
        }
    }

    internal class Pigeonhole
    {
        public List<Item> items;
        public Dictionary<string, int> users;

        //UWAGA NIE WYPEŁNIA DICTIONARY
        public Pigeonhole()
        {
            items = new List<Item>();
        }

        public Pigeonhole(List<Item> list)
        {
            items = list;
            FillDictionary();
        }

        public void FillDictionary()
        {
            users = null;
            users = new Dictionary<string, int>();
            int counter = 0;

            foreach (Item item in items)
            {
                foreach (Tuple<string, int> rating in item.Rates)
                {
                    string userASIN = rating.Item1;
                    if (!users.ContainsKey(userASIN))
                    {
                        users.Add(userASIN, counter);
                        counter++;
                    }
                }
            }
        }

        private void Sort()
        {
            items.Sort();
            items.Reverse();
        }

        //TO NIE SĄ PROCENTY
        //0.01 = 1%
        //goddamnit
        public double GetCovering()
        {
            double ratings = 0;
            foreach (Item item in items)
            {
                ratings += item.Rates.Count;
            }

            return ratings / ((double)items.Count * (double)users.Count);
        }

        public void extractBestItems(int itemCount)
        {
            Sort();
            if (items.Count > itemCount)
            {
                items = items.GetRange(0, itemCount);
                FillDictionary();
            }
            return;
        }

        public RMatrix extractToR()
        {
            RMatrix r = new RMatrix();



            int standardItemID = 0;

            foreach (Item item in items)
            {
                foreach (Tuple<string, int> ratingTuple in item.Rates)
                {
                    r.Add(users[ratingTuple.Item1], standardItemID, ratingTuple.Item2);
                }
                standardItemID++;
            }
            r.setSize(users.Count, items.Count);

            Console.WriteLine("Pobrano R");
            return r;
        }


        public void extractBestUsers(int usersCount)
        {
            Dictionary<string, int> countOfRatesOfUser = new Dictionary<string, int>();

            foreach (Item item in items)
            {
                foreach (Tuple<string, int> rating in item.Rates)
                {
                    string userASIN = rating.Item1;
                    if (!countOfRatesOfUser.ContainsKey(userASIN))
                    {
                        countOfRatesOfUser.Add(userASIN, 0);
                    }
                    else
                    {
                        countOfRatesOfUser[userASIN]++;
                    }
                }
            }
            // ilość ocen została podliczona


            List<Tuple<string, int>> countOfRatesOfUserLIST = new List<Tuple<string, int>>();
            foreach (KeyValuePair<string, int> keyValuePair in countOfRatesOfUser)
            {
                countOfRatesOfUserLIST.Add(Tuple.Create(keyValuePair.Key, keyValuePair.Value));
            }
            countOfRatesOfUserLIST.Sort((x, y) => y.Item2.CompareTo(x.Item2));

            List<string> validASINs = new List<string>();
            for (int i = 0; i < usersCount; i++)
            {
                validASINs.Add(countOfRatesOfUserLIST[i].Item1);
            }

            foreach (Item item in items)
            {
                for (int i = 0; i < item.Rates.Count; i++)
                {
                    string userASIN = item.Rates[i].Item1;
                    if (!validASINs.Contains(userASIN))
                    {
                        item.Rates.RemoveAt(i);
                        i--;
                    }
                }
            }
            // bezużyteczne ludzkie śmiecie zniknęły

            FillDictionary();

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Rates.Count == 0)
                {
                    items.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    internal class Item : IComparable<Item>
    {
        public int ItemID { get; set; }
        public List<Tuple<string, int>> Rates { get; set; }


        public Item()
        {
            Rates = new List<Tuple<string, int>>();
        }

        public Item(int itemID)
        {
            ItemID = itemID;
            Rates = new List<Tuple<string, int>>();
        }

        public Item(int itemID, List<Tuple<string, int>> rates)
        {
            ItemID = itemID;
            Rates = rates;
        }

        public int CompareTo(Item other)
        {
            if (other == null) return 1;

            return Rates.Count.CompareTo(other.Rates.Count);
        }
    }

    internal class ItemFAT
    {
        public List<Tuple<string, int>> Rates { get; set; }
        public string Group { get; set; }
        public int ItemID { get; set; }
        public ItemFAT(int itemID, string group)
        {
            ItemID = itemID;
            Group = group;
            Rates = new List<Tuple<string, int>>();
        }

        public ItemFAT()
        {
            Rates = new List<Tuple<string, int>>();
        }
    }

    internal class AmazonReader
    {

        public static List<ItemFAT> ReadFile(int maxItems)
        {
            List<ItemFAT> productsList = new List<ItemFAT>();

            try
            {
                ItemFAT item = new ItemFAT();
                using StreamReader sr = new StreamReader("../../../data/amazon-meta.txt");
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();

                    if (line.StartsWith("Id:"))
                    {
                        if (item.Group != null)
                        {
                            productsList.Add(item);
                        }

                        item = new ItemFAT
                        {
                            //if(productsList.Count > 480000)
                            //    Console.WriteLine(line);  //w razie znalezienia Id w np. nazwie produku
                            ItemID = Int32.Parse(line.Trim().Substring(3).Trim())
                        };

                        if (item.ItemID > maxItems)
                        {
                            break;
                        }
                    }

                    if (line.Contains("group:"))
                    {
                        item.Group = line.Trim().Substring(6).Trim();
                    }

                    if (line.Contains("cutomer:"))
                    {
                        string customerASIN = CarveUserId(line);
                        int rating = CarveRating(line);
                        var newRating = Tuple.Create(customerASIN, rating);
                        item.Rates.Add(newRating);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

            return productsList;
        }

        private static string CarveUserId(string line)
        {
            string pattern = "cutomer:.*rating:";
            string output = Regex.Match(line, pattern).Value;
            output = output[8..^7].Trim();

            return output;
        }

        private static int CarveRating(string line)
        {
            string pattern = "rating:.*votes:";
            string output = Regex.Match(line, pattern).Value;
            int rate = Int32.Parse(output[7..^7].Trim());

            return rate;
        }

    }
}
