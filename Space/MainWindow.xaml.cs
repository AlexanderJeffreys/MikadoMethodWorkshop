using System;
using System.Windows.Input;

namespace Space
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private SpaceRunner _runner;
        
        public MainWindow()
        {
            InitializeComponent();
            _runner = new SpaceRunner(GameArea, false, true);
            _runner.Run();
        }

        private void GameArea_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            SpaceRunner.Zoom(Math.Sign(e.Delta));
        }

        private void GameArea_OnMouseMove(object sender, MouseEventArgs e)
        {
        }

        private void GameArea_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
        }
    }
}