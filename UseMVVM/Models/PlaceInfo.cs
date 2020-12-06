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

        public IEnumerable<ConfirmedCount> Counts { get; set; }

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
                if (_Location != null)
                    return _Location;
                if (ProvinceCount == null) return default(Point);

                var location_x = ProvinceCount.Average(i => i.Location.X);
                var location_y= ProvinceCount.Average(i => i.Location.Y);

                return new Point(location_x, location_y);
            }
            set => _Location = value; 
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
