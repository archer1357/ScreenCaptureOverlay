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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LowLevelKeyboardListener listener;
        private float transparent_opacity = 0.5f;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Opacity = transparent_opacity;

            var hwnd = new WindowInteropHelper(this).Handle;
            Transparency.SetWindowExTransparent(hwnd);

            listener = new LowLevelKeyboardListener();
            listener.OnKeyPressed += listener_OnKeyPressed;
            listener.HookKeyboard();
        }

        void listener_OnKeyPressed(object sender, KeyPressedArgs e)
        { 
            if (e.KeyPressed == Key.F4)
            {
                Close();
            }
            else if (e.KeyPressed == Key.D1)
            {
                Opacity = 0.0;

                Task.Delay(500).ContinueWith(_ => {
                    this.Dispatcher.Invoke(() => {
                        Point location = MainControl.PointToScreen(new Point(0, 0));
                        Size size = MainControl.RenderSize;
                        ImageControl.Source = Screenshot.take((int)location.X,(int)location.Y,(int)size.Width,(int)size.Height);
                        Opacity = transparent_opacity;
                    });
                });
            }
            else if (e.KeyPressed == Key.D2)
            {
                ImageControl.Source = null;
            }
            else if (e.KeyPressed == Key.Add || e.KeyPressed == Key.OemPlus)
            {
                if (transparent_opacity < 1.0f) {
                    transparent_opacity += 0.1f;
                    Opacity = transparent_opacity;
                }
            }
            else if (e.KeyPressed == Key.Subtract || e.KeyPressed == Key.OemMinus)
            {
                if (transparent_opacity > 0.0f) {
                    transparent_opacity -= 0.1f;
                    Opacity = transparent_opacity;
                }
            }
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            //this.Activate();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listener.UnHookKeyboard();
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
