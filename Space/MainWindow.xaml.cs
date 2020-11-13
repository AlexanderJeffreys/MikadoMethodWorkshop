using System;
using System.Windows.Input;

namespace Space
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var runner = new SpaceRunner(GameArea, false, true);
            runner.Run();
        }

        private void GameArea_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            SpaceRunner.Zoom(-Math.Sign(e.Delta));
        }

        private void GameArea_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                SpaceRunner.Drag(e.GetPosition(GameArea));
            }
        }

        private void GameArea_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            SpaceRunner.StopDragging();
        }
    }
}