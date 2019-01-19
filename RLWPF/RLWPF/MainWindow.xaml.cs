using Nucleus.Game;
using RL;
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
using System.Windows.Threading;

namespace RLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<Key, InputFunction> _KeyMapping =
            new Dictionary<Key, InputFunction>();

        /// <summary>
        /// Timer used to trigger updates
        /// </summary>
        DispatcherTimer _Timer = new DispatcherTimer();

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
            _KeyMapping.Add(Key.D1, InputFunction.Ability_1);
            _KeyMapping.Add(Key.D2, InputFunction.Ability_2);
            _KeyMapping.Add(Key.D3, InputFunction.Ability_3);
            _KeyMapping.Add(Key.D4, InputFunction.Ability_4);
            _KeyMapping.Add(Key.D5, InputFunction.Ability_5);
            _KeyMapping.Add(Key.D6, InputFunction.Ability_6);
            _KeyMapping.Add(Key.D7, InputFunction.Ability_7);
            _KeyMapping.Add(Key.D8, InputFunction.Ability_8);
            _KeyMapping.Add(Key.D9, InputFunction.Ability_9);
            _KeyMapping.Add(Key.G, InputFunction.PickUp);

            _Timer.Interval = new TimeSpan(100000);
            _Timer.Tick += _Timer_Tick;
            _Timer.Start();
        }

        private void _Timer_Tick(object sender, EventArgs e)
        {
            GameEngine.Instance.Update();
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
