using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace Lentelė
{
    class Program
    {
        public const int capacity = 100000;
        public const int keyLength = 5; // rakto ilgis
        static Hash table = new Hash(capacity); // sugeneruojama tusčia lentelė
        static int count = 10000; // sugeneruojamų elementų kiekis
        static int n = 100; // paieškų kiekis
        static List<int> rndList = new List<int>(29);

        private static System.Object lockThis = new System.Object(); 

        static void Main(string[] args)
        {
            Reading();
            ThreadStart ts1 = delegate { Containss(0, capacity / 3, 0); };
            ThreadStart ts2 = delegate { Containss(capacity / 3, (capacity /3) * 2, 10); };
            ThreadStart ts3 = delegate { Containss((capacity / 3) * 2, capacity, 20); };
            Thread th1 = new Thread(ts1);
            Thread th2 = new Thread(ts2);
            Thread th3 = new Thread(ts3);
            Stopwatch watch = new Stopwatch();
            watch.Start();
            th1.Start();
            th2.Start();
            th3.Start();
            watch.Stop();
           // th1.Abort();
           // th2.Abort();
            //th3.Abort();
            TimeSpan time = watch.Elapsed;
            Console.WriteLine("Užtruktas laikas gijose: " + time);
            Console.WriteLine("Įvyko operacijų: " + table.ReturnOperationsCount());
            Writing(table);
        }
        public static void Reading()
        {
            string alphabet = "qwertyuiopasdfghjklzxcvbnm0123456789";         
            Random ats = new Random();
            int value = 0;
            for (int i = 0; i < count; i++)
            {
                var key = new StringBuilder();
                for (int j = 0; j < keyLength; j++)
                {
                    var text = alphabet[ats.Next(0, alphabet.Length)];
                    key.Append(text);
                }
                value = ats.Next(1, 50);
                table.Add(key.ToString(), value);
            }
            for (int i = 0; i < 30; i++)
            {
                rndList.Add(ats.Next(1, 70));  // susikuriu atsitiktinių skaičių aibę
            }
        }
        private static void Containss(int a, int b, int c)
        {
            Random rnd = new Random();
            for (int i = 0; i < n / 3; i++)
            {
                int sk = rnd.Next(1, 70);
                lock (lockThis)
                {
                    bool ats = table.ContainsValue(a, b, rndList.ElementAt(c));
                }
                c++;
                //Console.WriteLine(ats);
            }
        }
        public static void Writing(Hash table)
        {
            //Console.WriteLine(table.ToString());
            ///int count = 0;
            //Console.WriteLine(new string('-', 50));
            table.Clear();
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Containss(0, capacity / 3, 0);
            Containss(capacity / 3, (capacity /3) * 2, 10);
            Containss((capacity / 3) * 2, capacity, 20);
            timer.Stop();
            //count = table.ReturnOperationsCount();
            //Console.WriteLine("Ar rado reikšmę lentelėje? " + ats);
            Console.WriteLine("Užtruktas laikas CPU " + timer.Elapsed);
            Console.WriteLine("Įvyko operacijų: " + table.ReturnOperationsCount());
        }
    }
}
