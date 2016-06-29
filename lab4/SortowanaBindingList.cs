using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace lab4
{
    public class SortowanaBindingList<Car> : BindingList<Car>
    {
        private bool posortowana;
        ListSortDirection sortDirectionValue;
        PropertyDescriptor sortPropertyValue;

        public SortowanaBindingList()
        {
        }

        public SortowanaBindingList(IList<Car> lista)
        {
            foreach (Car samochod in lista)
            {
                this.Add(samochod);
            }
        }

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection kierunek)
        {

            if (prop.PropertyType.GetInterface("IComparable") != null)
            {
                sortPropertyValue = prop;
                sortDirectionValue = kierunek;

                IEnumerable<Car> query = base.Items;

                if (kierunek == ListSortDirection.Ascending)
                {
                    query = query.OrderBy(i => prop.GetValue(i));
                }
                else
                {
                    query = query.OrderByDescending(i => prop.GetValue(i));
                }

                int indeks = 0;
                foreach (Car samochod in query)
                {
                    this.Items[indeks] = samochod;
                    indeks++;
                }

                posortowana = true;
                //this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }

        }

        protected override PropertyDescriptor SortPropertyCore
        {
            get { return sortPropertyValue; }
        }

        protected override ListSortDirection SortDirectionCore
        {
            get { return sortDirectionValue; }
        }

        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        protected override bool IsSortedCore
        {
            get { return posortowana; }
        }
        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            List<Car> rzeczy = this.Items as List<Car>;
            foreach (Car rzecz in rzeczy)
            {
                if (prop.PropertyType == typeof(int))
                {
                    int liczba = (int)prop.GetValue(rzecz);
                    if (((int)key).Equals(liczba))
                        return IndexOf(rzecz);

                }
                if (prop.PropertyType == typeof(string))
                {
                    string nazwa = (string)prop.GetValue(rzecz);
                    if (((string)key).Equals(nazwa))
                        return IndexOf(rzecz);

                }

            }
            return -1;
        }
    }
}