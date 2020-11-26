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
    /// Lógica interna para Página_Principal_erro_admin.xaml
    /// </summary>
    public partial class Página_Principal_erro_admin : Window
    {
        public Página_Principal_erro_admin()
        {
            InitializeComponent();
        }

        private void botão_voltar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Pagina_Admin_User ss = new Pagina_Admin_User();
            ss.Show();
        }
    }
}
