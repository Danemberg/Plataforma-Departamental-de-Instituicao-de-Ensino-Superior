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

namespace Wpf_Proj.Grupo2
{
    /// <summary>
    /// Lógica interna para Pagina_principal_erro.xaml
    /// </summary>
    public partial class Pagina_principal_erro : Window
    {
        public Pagina_principal_erro()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Pagina__principal_erro_doc ss = new Pagina__principal_erro_doc();
                ss.Show();

        }
    }
}
