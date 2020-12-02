using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        private static string parsIp = @"http://ipwhois.app/json/";

        private static async Task<Stream> GetDataStream()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(data_url, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }

        private static IEnumerable<string> GetDataLines()
        {
            using var data_stream = GetDataStream().Result;
            using var data_reader = new StreamReader(data_stream);

            while (!data_reader.EndOfStream)
            {
                var line = data_reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                line = Regex.Replace(line, "\"([\\w\\s]*)\\,([\\w\\s]*)\"", 
                    (m) => m.Groups[1].Value + " -" + m.Groups[2].Value);

                yield return line;
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(d => DateTime.Parse(d, CultureInfo.InvariantCulture))
            .ToArray();

        private static IEnumerable<(string Country, string Province, int[] Counts)> GetData()
        {
            var line = GetDataLines().Skip(1).Select(s => s.Split(','));

            foreach (var row in line)
            {
                string province = row[0] ?? string.Empty;
                string country = row[1] ?? string.Empty;
                int[] counts = row.Skip(4).Select(i => int.Parse(i)).ToArray();
                yield return (country, province, counts);
            }
        }

        static void Main(string[] args)
        {
            //var data = GetData();
            //foreach ((string Country, string Province, int[] Counts) item in data)
            //{
            //    Console.WriteLine(item.Country + " " + item.Province);
            //}
            //Console.ReadLine();

            var russia_data = GetData().First(s => s.Country.Equals("Russia", StringComparison.OrdinalIgnoreCase));

            var russia_counts = russia_data.Counts.Zip(GetDates(), (value, date) => $"{date:dd.MM} {value}");

            foreach (string i in russia_counts)
                Console.WriteLine(i);
            Console.ReadLine();
        }

        private static void HostInfoTest()
        {
            WebClient client = new WebClient();
            string hostName = "metanit.com";
            IPHostEntry host;
            try
            {
                host = Dns.GetHostEntry(hostName);
            }
            catch (Exception ex)
            {
                host = null;
                Console.WriteLine(ex.Message);
            }

            if (host != null)
            {
                var mayIP = host.AddressList[0];

                parsIp += mayIP.ToString();

                var data = client.DownloadString(parsIp);

                string latitude = Regex.Match(data, "\"latitude\":\"(.*?)\",").Groups[1].Value;
                string longitude = Regex.Match(data, "\"longitude\":\"(.*?)\",").Groups[1].Value;
                string continent = Regex.Match(data, "\"continent\":\"(.*?)\",").Groups[1].Value;
                string country = Regex.Match(data, "\"country\":\"(.*?)\",").Groups[1].Value;
                string city = Regex.Match(data, "\"city\":\"(.*?)\",").Groups[1].Value;
                string org = Regex.Match(data, "\"org\":\"(.*?)\",").Groups[1].Value;

                Console.WriteLine($"org - {org}");
                Console.WriteLine($"continent - {continent}");
                Console.WriteLine($"country - {country}");
                Console.WriteLine($"city - {city}");
                Console.WriteLine($"latitude - longitude : {latitude} - {longitude}");
            }
        }
    }
}
