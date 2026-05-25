using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Tester
{
    internal class TheStuff : Grid
    {

        public List<string> Stuff { get; set; } = new List<string>();
        public string Name { get; set; } = "The Stuff";
        public string Somestuff { get; set; } = "";
        public int Fafafela { get; set; } = 0;

        Label Label = new Label()
        {
           Height = 90,
           Width = 90,
           HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
              VerticalAlignment = System.Windows.VerticalAlignment.Center,
              Content = NameProperty.Name
        };
    public TheStuff()
        {
            Height = 100;
            Width = 100;
            Children.Add(Label);
        }
    }
}
