using Nucleus.Game;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<Key, InputFunction> _KeyMapping =
            new Dictionary<Key, InputFunction>();

        public MainWindow()
        {
            InitializeComponent();

            GameEngine.Instance.LoadModule(new RLModule());
            GameEngine.Instance.StartUp();

            this.DataContext = GameEngine.Instance;

            _KeyMapping.Add(Key.Up, InputFunction.Up);
            _KeyMapping.Add(Key.Down, InputFunction.Down);
            _KeyMapping.Add(Key.Left, InputFunction.Left);
            _KeyMapping.Add(Key.Right, InputFunction.Right);
            _KeyMapping.Add(Key.Space, InputFunction.Wait);
        }

        private void Key_Down(object sender, KeyEventArgs e)
        {
            if (_KeyMapping.ContainsKey(e.Key))
                GameEngine.Instance.Input.InputPress(_KeyMapping[e.Key]);
            else if (e.Key == Key.T)
            {
                var testWin = new ArtitectTest();
                testWin.Show();
            }
        }

        private void Key_Up(object sender, KeyEventArgs e)
        {
            if (_KeyMapping.ContainsKey(e.Key))
                GameEngine.Instance.Input.InputRelease(_KeyMapping[e.Key]);
        }
    }
}
