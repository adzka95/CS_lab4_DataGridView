using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace lab4
{
    public partial class Form1 : Form
    {
        public List<Car> myCars = new List<Car>(){
            new Car("E250", new Engine(1.8, 204, "CGI"), 2009),
            new Car("E350", new Engine(3.5, 292, "CGI"), 2009),
            new Car("A6", new Engine(2.5, 187, "FSI"), 2012),
            new Car("A6", new Engine(2.8, 220, "FSI"), 2012),
            new Car("A6", new Engine(3.0, 295, "TFSI"), 2012),
            new Car("A6", new Engine(2.0, 175, "TDI"), 2011),
            new Car("A6", new Engine(3.0, 309, "TDI"), 2011),
            new Car("S6", new Engine(4.0, 414, "TFSI"), 2012),
            new Car("S8", new Engine(4.0, 513, "TFSI"), 2012)
            };

        public Form1()
        {
            InitializeComponent();
            myMain();
        }
        private void myMain() {
            Console.WriteLine("Witaj!");
            SortowanaBindingList<Car> myCarsBindingList = new SortowanaBindingList<Car>() { 
                new Car("E250", new Engine(1.8, 204, "CGI"), 2009),
                new Car("E350", new Engine(3.5, 292, "CGI"), 2009),
                new Car("A6", new Engine(2.5, 187, "FSI"), 2012),
                new Car("A6", new Engine(2.8, 220, "FSI"), 2012),
                new Car("A6", new Engine(3.0, 295, "TFSI"), 2012),
                new Car("A6", new Engine(2.0, 175, "TDI"), 2011),
                new Car("A6", new Engine(3.0, 309, "TDI"), 2011),
                new Car("S6", new Engine(4.0, 414, "TFSI"), 2012),
                new Car("S8", new Engine(4.0, 513, "TFSI"), 2012)          
            
            };
            BindingSource carBindingSource = new BindingSource(myCarsBindingList, null);
            carBindingSource.DataSource = myCarsBindingList;
            //Drag a DataGridView control from the Toolbox to the form.
            dataGridView1.DataSource = myCarsBindingList;
            zad1();
            zad2();
        
        
        }
        void zad1() {
            //method-based query syntax.
            var wyniki = myCars
                      .Where(samochod => samochod.model.Equals("A6")) 
                      .Select(samochod => new
                      {
                          hppl = samochod.motor.horsePower / samochod.motor.displacement,
                          engineType = samochod.motor.model.Equals("TDI")  ? "diesel" : "petrol"
                      })
                      .OrderByDescending(samochod=> samochod.hppl);

            foreach (var sam in wyniki)
            {
                Console.WriteLine("{0} {1}", sam.engineType, sam.hppl);
            }
           
            Console.WriteLine("");
            var statystyki = wyniki.GroupBy(okaz => okaz.engineType)
                .Select(cos => new { typ = cos.Key, srednia = cos.Average(a => a.hppl) });
            foreach (var sam in statystyki)
            {
                Console.WriteLine("Typ: {0} Srednia: {1}", sam.typ,sam.srednia);
            }

            // query expression syntax.

            var wyniki2 = from samochod in myCars
                          where samochod.model.Equals("A6")
                          orderby samochod.motor.horsePower / samochod.motor.displacement descending
                          select new
                          {
                              hppl = samochod.motor.horsePower / samochod.motor.displacement,
                              engineType = samochod.motor.model.Equals("TDI") ? "diesel" : "petrol"
                          };

            foreach (var sam in wyniki2)
            {
                Console.WriteLine("{0} {1}", sam.engineType, sam.hppl);
            }

            var statystyki2 = from wynik in wyniki2
                              group wynik by wynik.engineType into cos
                              select new
                              {
                                  typ = cos.Key,
                                  srednia = cos.Average(a => a.hppl)
                              };

            foreach (var sam in statystyki2)
            {
                Console.WriteLine("Typ: {0} Srednia: {1}", sam.typ, sam.srednia);
            }
        
        }

        void zad2() {
            Func<Car, Car, int> arg1 = delegate(Car a, Car b)
            {
                if (a.motor.horsePower > b.motor.horsePower)
                    return -1;
                else
                    return 1;           
            
            }; 
            
            myCars.Sort(new Comparison<Car>(arg1));
            foreach (var sam in myCars)
            {
                Console.WriteLine("{0} {1}", sam.model, sam.motor.horsePower);
            }

            Predicate<Car> arg2 =jestTDI;
            Action<Car> arg3 = pokarz;
            myCars.FindAll(arg2).ForEach(arg3);

        }
           
        public static bool jestTDI(Car a)
        {
            if(a.motor.model.Equals("TDI"))
                return true;
            else 
                return false;
        }
        public static void pokarz(Car a) {
            MessageBox.Show(a.ToString());        
        
        }
     
        
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        

     

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            int indeks = -1;
            if (toolStripComboBox1.Text.Equals("year"))
            {
                int rok = Int32.Parse(toolStripTextBox1.Text);
                foreach (DataGridViewRow wiersz in dataGridView1.Rows)
                {
                    if (wiersz.Cells["year"].Value != null)
                    {
                        if (wiersz.Cells["year"].Value.Equals(rok))
                        {
                            indeks = wiersz.Index;
                            break;
                        }
                    }
                }

            }
            else
            {
                string nazwa = toolStripTextBox1.Text;
                foreach (DataGridViewRow wiersz in dataGridView1.Rows)
                {
                    if (wiersz.Cells["model"].Value != null)
                    {
                        if (wiersz.Cells["model"].Value.ToString().Equals(nazwa))
                        {
                            indeks = wiersz.Index;
                            break;
                        }
                    }
                }

            }
            foreach (DataGridViewCell row in dataGridView1.SelectedCells)
            {
                row.Selected = false;
            }
            if (indeks != -1)
                dataGridView1.Rows[indeks].Selected = true;
        }

        private void toolStrip1_Enter(object sender, EventArgs e)
        {
            if(toolStripComboBox1.Items.Count==0)
            foreach (PropertyDescriptor wlasciwosc in TypeDescriptor.GetProperties(typeof(Car)))
            {
                if (wlasciwosc.PropertyType == typeof(int) || wlasciwosc.PropertyType == typeof(string))
                {
                    toolStripComboBox1.Items.Add(wlasciwosc.Name);
                }
            }
        }

        private void dataGridView1_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (e.ColumnIndex == 1) {
                string nazwa = e.Value.ToString();
                string pattern = @"(\w*) \(([0-9]+([0-9]+)?), ([0-9]+([0-9]+)?)\)";
                var dopasuj = Regex.Match(nazwa, pattern);

                if (dopasuj.Success == false)
                {
                    e.Value = new Engine(0, 0, "");

                }
                else
                {
                   
                    e.Value = new Engine(double.Parse(dopasuj.Groups[2].Value), double.Parse(dopasuj.Groups[4].Value), dopasuj.Groups[1].Value);

                }
            
            
            
            }
            e.ParsingApplied = true;

        }


    }
}
