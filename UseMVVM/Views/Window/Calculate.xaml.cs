using System.Windows;

namespace UseMVVM.Views.Window
{
    /// <summary>
    /// Логика взаимодействия для Calculate.xaml
    /// </summary>
    public partial class Calculate : System.Windows.Window
    {
        public Calculate()
        {
            InitializeComponent();
            
        }

        private void WindowHead_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
