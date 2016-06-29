using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab4
{
    public class Engine : IComparable<Engine>
    {
        public Engine()
        {
        }

        public Engine(double przesuniecie, double konie, string nazwa)
        {
            displacement = przesuniecie;
            horsePower = konie;
            model = nazwa;
        }

        public double displacement { get; set; }

        public double horsePower { get; set; }

        public string model { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ({1}, {2})", model, horsePower, displacement);
        }
        public int CompareTo(Engine drugi)
        {
            return horsePower.CompareTo(drugi.horsePower);

        }
    }
}
