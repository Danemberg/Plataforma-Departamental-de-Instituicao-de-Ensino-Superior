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
    /// Lógica interna para Pagina_Admin_User.xaml
    /// </summary>
    public partial class Pagina_Admin_User : Window
    {
        public Pagina_Admin_User()
        {
            InitializeComponent();
        }

        private void botao_Verificar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Pagina_Admin_User pagina_Admin_User = new Pagina_Admin_User();
            pagina_Admin_User.Show();
        }
    }
}
