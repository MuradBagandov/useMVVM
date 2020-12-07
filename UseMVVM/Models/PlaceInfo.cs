using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UseMVVM.Models
{
    internal abstract class PlaceInfo
    {
        public string Name { get; set; }

        public virtual Point Location { get; set; }

        public virtual IEnumerable<ConfirmedCount> Counts { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    internal class CountryInfo : PlaceInfo
    {
        private Point _Location;
        public override Point Location 
        {
            get 
            {
                if (ProvinceCount == null) return default(Point);

                var location_x = ProvinceCount.Average(i => i.Location.X);
                var location_y= ProvinceCount.Average(i => i.Location.Y);

                _Location = new Point(location_x, location_y);
                return _Location;
            }
            set => _Location = value; 
        }

        private IEnumerable<ConfirmedCount> _Counts;

        public override IEnumerable<ConfirmedCount> Counts 
        { 
            get 
            {
                if (ProvinceCount == null) 
                    return default(IEnumerable<ConfirmedCount>);

                var dates = ProvinceCount.First().Counts.Select(i => i.Date).ToList();

                var counts_provinces = ProvinceCount
                    .Select(i => i.Counts.Select(c=>c.Count).ToArray())
                    .ToArray();

                List<int> resultCounts = new List<int>(dates.Count);

                for (int i = 0; i < counts_provinces[0].Length; i++)
                {
                    int countSum = 0;
                    for (int j = 0; j < counts_provinces.Length; j++)
                    {
                        countSum += counts_provinces[j][i];
                    }
                    resultCounts.Add(countSum);
                }

                return resultCounts.Zip(dates, (count, date) => new ConfirmedCount(date, count));
            }
            set => _Counts = value; 
        }

        public IEnumerable<ProvinceInfo> ProvinceCount { get; set; }
    }

    internal class ProvinceInfo : PlaceInfo {}

    internal struct ConfirmedCount
    {
        public DateTime Date { get; set; }

        public int Count { get; set; }

        public ConfirmedCount(DateTime date, int count)
        {
            Date = date;
            Count = count;
        }
    }

}
