using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RLWPF
{
    /// <summary>
    /// Interaction logic for ArtitectTest.xaml
    /// </summary>
    public partial class ArtitectTest : Window
    {
        public ArtitectTester Tester { get; set; }

        public ArtitectTest()
        {
            InitializeComponent();

            Tester = new ArtitectTester();
            this.DataContext = Tester;
        }
    }
}
