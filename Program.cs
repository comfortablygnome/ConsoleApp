using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.Serialization;
using Newtonsoft.Json;


namespace CSConsoleApp {

    public class OpenWeather {
        public class Coord {
            public double Lon { get; set; }
            public double Lat { get; set; }
        }

        public class Sys {
            public int Type { get; set; }
            public int Id { get; set; }
            public double Message { get; set; }
            public string Country { get; set; }
            public int Sunrise { get; set; }
            public int Sunset { get; set; }
        }

        public class Weather {
            public int Id { get; set; }
            public string Main { get; set; }
            public string Description { get; set; }
            public string Icon { get; set; }
        }

        public class Main {
            public double Temp { get; set; }
            public int Humidity { get; set; }
            public double Pressure { get; set; }
            public double Temp_min { get; set; }
            public double Temp_max { get; set; }
        }

        public class Wind {
            public double Speed { get; set; }
            public double Gust { get; set; }
            public int Deg { get; set; }
        }



        public class Clouds {
            public int All { get; set; }
        }

        public class Root {
            public Coord Coord { get; set; }
            public Sys Sys { get; set; }
            public List<Weather> Weather { get; set; }
            public string @Base { get; set; }
            public Main Main { get; set; }
            public Wind Wind { get; set; }
            public Dictionary<string, double> Rain { get; set; }
            public Clouds Clouds { get; set; }
            public int Dt { get; set; }
            public int Id { get; set; }
            public string Name { get; set; }
            public int Cod { get; set; }
        }
    }
    class Program {
        public void Main(string[] args) {
            //Building these console programs into a switch statement for easier viewing

            var progOn = "";
            var progChoice = "";

            Console.WriteLine("Start program? Enter a value and press enter.");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            progOn = Console.ReadLine();

            int exitCode = 0;

            switch (progOn) {

                case "1":

                    Console.WriteLine("Thank you. Press any key to continue...");
                    Console.ReadKey();
                    Console.WriteLine("Please enter a value and press enter.");
                    Console.WriteLine("1. Weather");
                    Console.WriteLine("2. Fibonacci Sequence");
                    Console.WriteLine("3. Prime Numbers");
                    Console.WriteLine("4. Exit Program");

                    progChoice = Console.ReadLine();

                    switch (progChoice) {
                        case "1":

                            Console.WriteLine("You have chosen Weather.");
                            Console.WriteLine("How would you like to search?");
                            Console.WriteLine("1. Zip Code");
                            Console.WriteLine("2. City");

                            var searchQuery = "";
                            string searchTerm = Console.ReadLine();
                            string search = "";
                            string place = "";

                            bool valid = false;

                            switch (searchTerm) {
                                case "1":

                                    Console.WriteLine("Weather search by zip. Please enter a zip code.");
                                    string zip = Console.ReadLine();

                                    //input validation for zip code
                                    while (!valid) {
                                        if (zip != "") {
                                            if (!System.Text.RegularExpressions.Regex.IsMatch(zip, "^[0-9]*$")) {
                                                Console.WriteLine("Numeric values only. (0-9). Try again.");
                                                zip = Console.ReadLine();
                                            }
                                            else if (zip.Length != 5) {
                                                Console.WriteLine("Please enter a 5-digit zip code.");
                                                zip = Console.ReadLine();
                                            }
                                            else {
                                                searchQuery = "zip=" + zip;
                                                search = "zip";
                                                place = zip;
                                                valid = true;
                                                break;
                                            }
                                        }
                                        else {
                                            Console.WriteLine("Please enter a zip code.");
                                            zip = Console.ReadLine();
                                            continue;
                                        }
                                    }
                                    break;
                                case "2":
                                    Console.WriteLine("Weather search by city. Please enter a city.");
                                    string city = Console.ReadLine();

                                    while (!valid) {
                                        if (city != "") {
                                            if (!System.Text.RegularExpressions.Regex.IsMatch(city, @"^[a-zA-Z]+$")) {
                                                Console.WriteLine("Entry contains invalid characters. Try again.");
                                                city = Console.ReadLine();
                                            }
                                            else {
                                                searchQuery = "q=" + city;
                                                search = "city";
                                                place = city;
                                                valid = true;
                                                break;
                                            }
                                        }
                                        else {
                                            Console.WriteLine("Please enter a city.");
                                            city = Console.ReadLine();
                                        }
                                    }
                                    break;
                                default:
                                    Console.WriteLine("Invalid input. Try again.");
                                    Console.WriteLine("How would you like to search?");
                                    Console.WriteLine("1. Zip Code");
                                    Console.WriteLine("2. City");
                                    searchTerm = Console.ReadLine();
                                    break;
                            }

                            Console.WriteLine("Searching by " + search + ". Displaying weather for " + place);
                            Console.WriteLine(DateTime.Now.ToString("dddd, MMMM dd yyyy HH:mm:ss"));

                            var url = "http://api.openweathermap.org/data/2.5/weather?" + searchQuery + "&appid=930a9fce6626185c639168eed88d09e2&units=imperial";

                            using (WebClient wc = new WebClient()) {

                                var json = wc.DownloadString(url);
                                var obj = JsonConvert.DeserializeObject<OpenWeather.Root>(json);
                                string str = JsonConvert.SerializeObject(obj, Formatting.Indented);
                                Console.WriteLine(str);

                            }

                            break;

                        case "2":

                            Console.WriteLine("You have chosen Fibonacci Sequence.");
                            Console.WriteLine("How many numbers would you like to display? Enter a number greater than or equal to 3.");

                            string num1 = Console.ReadLine();

                            //avoids crash from NaN/Null
                            bool done = false;
                            while (!done) {
                                if (num1 != "") {
                                    if (!System.Text.RegularExpressions.Regex.IsMatch(num1, "^[0-9]*$")) {
                                        Console.WriteLine("Numeric values only. (0-9). Try again.");
                                        num1 = Console.ReadLine();
                                    }
                                    else if (Convert.ToInt32(num1) < 3) {
                                        Console.WriteLine("Number must be greater than or equal to 3. Try again");
                                        num1 = Console.ReadLine();
                                        continue;
                                    }
                                    else {
                                        done = true;
                                    }
                                }
                                else {
                                    Console.WriteLine("Please enter a numeric value.");
                                    num1 = Console.ReadLine();
                                    continue;
                                }
                            }

                            int fibNum = Convert.ToInt32(num1);

                            Console.WriteLine("You chose " + fibNum + ".");
                            Console.WriteLine("Here is your Fibonacci Sequence...");

                            int a = 0;
                            int b = 1;

                            //building fib sequence
                            for (int x = 0; x < fibNum; x++) {
                                int y = a;
                                a = b;
                                b = y + b;
                                Console.WriteLine(b);
                            }

                            break;


                        case "3":
                            Console.WriteLine("You have chosen Prime Numbers.");
                            Console.WriteLine("Please enter a value. Must be greater than or equal to 3.");
                            string num = Console.ReadLine();
                            bool fin = false;
                            bool isPrime = false;

                            Console.WriteLine("You chose " + num + ".");

                            //avoids crash from Nan/Null
                            while (!fin) {
                                if (num != "") {
                                    if (!System.Text.RegularExpressions.Regex.IsMatch(num, "^[0-9]*$")) {
                                        Console.WriteLine("Numeric values only. (0-9). Try again.");
                                        num = Console.ReadLine();
                                    }
                                    else if (Convert.ToInt32(num) < 3) {
                                        Console.WriteLine("Number must be greater than or equal to 3. Try again");
                                        num = Console.ReadLine();
                                        continue;
                                    }
                                    else {
                                        fin = true;
                                    }
                                }
                                else {
                                    Console.WriteLine("Please enter a numeric value.");
                                    num = Console.ReadLine();
                                    continue;
                                }
                            }

                            int primeNum = Convert.ToInt32(num);
                            List<int> PrimeList = new List<int>();

                            //won't populate from for loop
                            PrimeList.Add(2);

                            for (int x = 3; x <= primeNum; x += 2) {
                                isPrime = true;
                                int factor = Convert.ToInt32(Math.Ceiling(Math.Sqrt(x)));

                                for (int i = 2; i <= factor; i++) {
                                    if (x % i == 0) {
                                        isPrime = false;
                                        break;
                                    }
                                }

                                if (isPrime) {
                                    PrimeList.Add(x);
                                }
                            }

                            if (isPrime) {
                                Console.WriteLine("Yes, " + primeNum + " is prime.");
                            }
                            else {
                                Console.WriteLine("No, " + primeNum + " is not prime.");
                            }

                            Console.WriteLine("Here are the prime numbers that precede the value you entered.");
                            Console.WriteLine(string.Join(", ", PrimeList));
                            Console.WriteLine(" ");
                            Console.WriteLine(" ");
                            break;

                        case "4":
                            Console.Write("Thank you.");
                            System.Environment.Exit(exitCode);
                            break;

                        default:
                            Console.WriteLine("Please enter a valid input. Press enter to continue.");
                            Console.WriteLine("Please select a program. Enter a value and press enter.");
                            Console.WriteLine("1. Weather");
                            Console.WriteLine("2. Fibonacci Sequence");
                            Console.WriteLine("3. Prime Numbers");
                            Console.WriteLine("4. Exit");
                            break;
                    }
                    goto case "1";

                case "2":

                    Console.WriteLine("Thank you. Press any key to return to main...");
                    Console.ReadKey();
                    break;

                default:

                    Console.WriteLine("Please enter a value and press enter.");
                    Console.WriteLine("Return to main?");
                    Console.WriteLine("1. Yes");
                    Console.WriteLine("2. No");
                    progChoice = Console.ReadLine();
                    break;
            }
        }
    }
}