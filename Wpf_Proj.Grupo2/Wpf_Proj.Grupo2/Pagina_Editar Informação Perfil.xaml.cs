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
    /// Lógica interna para Pagina_Editar_Informação_Perfil.xaml
    /// </summary>
    public partial class Pagina_Editar_Informação_Perfil : Window
    {
        public Pagina_Editar_Informação_Perfil()
        {
            InitializeComponent();
        }

        private void botao_salvar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Pagina_Editar_Informação_perfil_2 ss = new Pagina_Editar_Informação_perfil_2();
            ss.Show();
        }

        private void botao_sair_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow ss = new MainWindow();
            ss.Show();

        }
    }
}
