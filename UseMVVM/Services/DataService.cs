using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using UseMVVM.Models;

namespace UseMVVM.Services
{
    internal enum RecievedData
    {
        Confirmed,
        Death,
        Recovered
    }

    internal class DataService
    {
        //https://github.com/CSSEGISandData/COVID-19

        private string _adressSourceData;

        private async Task<Stream> GetDataStream()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(_adressSourceData, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        }

        private  IEnumerable<string> GetDataLines()
        {
            using var data_stream = Task.Run(GetDataStream).Result;
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

        private DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(d => DateTime.Parse(d, CultureInfo.InvariantCulture))
            .ToArray();

        private IEnumerable<(string Country, string Province, Point Location, int[] Counts)> GetCountriesData()
        {
            var line = GetDataLines().Skip(1).Select(s => s.Split(','));

            foreach (var row in line)
            {
                string province = row[0] ?? string.Empty;
                string country = row[1] ?? string.Empty;
                Point location = new Point
                    (
                        double.Parse(String.IsNullOrEmpty(row[2]) ? "0" : row[2], CultureInfo.GetCultureInfo("en-US")),
                        double.Parse(String.IsNullOrEmpty(row[3]) ? "0" : row[3], CultureInfo.GetCultureInfo("en-US"))
                    );
                int[] counts = row.Skip(4).Select(i => int.Parse(i)).ToArray();
                yield return (country, province, location, counts);
            }
        }

        private void SetSourceData(RecievedData value)
        {
            var setting = new Properties.Settings();
            switch (value)
            {
                case RecievedData.Confirmed: _adressSourceData = setting.URI_CV19_Confirmed; break;
                case RecievedData.Recovered: _adressSourceData = setting.URI_CV19_Recovered; break;
                case RecievedData.Death: _adressSourceData = setting.URI_CV19_Death; break;
            }
        }

        public IEnumerable<CountryInfo> GetData(RecievedData value)
        {
            SetSourceData(value);

            var dates = GetDates();

            var data_countries = GetCountriesData().GroupBy(i=>i.Country);

            foreach(var item in data_countries)
            {

                CountryInfo resultItem = new CountryInfo();
                resultItem.Name = item.Key;
                resultItem.ProvinceCount = item.Select(i =>
                    new ProvinceInfo()
                    {
                        Name = i.Province,
                        Location = i.Location,
                        Counts = i.Counts.Zip(dates, (count, date) => new ConfirmedCount(date, count))
                    });
                yield return resultItem;
            }
        }
    }
}
