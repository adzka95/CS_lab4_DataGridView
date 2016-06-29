using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab4
{
    public class Car
    {
        public Car(){}

        public Car(string nazwa, Engine silnik, int rok)
        {
            model = nazwa;
            motor = silnik;
            year = rok;
        }

        public string model { get; set; }

        public Engine motor { get; set; }
        
        public int year
        {
            get;
            set;
        }
        public override string ToString()
        {
            return string.Format("{0} ({1}, {2})", model, year, motor);
        }
    }
}
