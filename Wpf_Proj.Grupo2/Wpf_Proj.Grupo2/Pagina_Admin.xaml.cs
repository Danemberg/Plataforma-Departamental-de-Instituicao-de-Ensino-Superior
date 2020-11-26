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
    /// Lógica interna para Pagina_Admin.xaml
    /// </summary>
    public partial class Pagina_Admin : Window
    {
        private int id_doc;
        private int aut;
        public Pagina_Admin(int id, int aut)
        {
            InitializeComponent();
            this.id_doc = id;
            this.aut = aut;
        }

        private void botão_escolas_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            PaginaEscolaAdm escadm = new PaginaEscolaAdm(id_doc, aut);
            escadm.Show();
        }

        private void botão_Departamentos_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            PaginaDepartamentoAdm deptadm = new PaginaDepartamentoAdm(id_doc, aut);
            deptadm.Show();
        }

        private void botão_Docentes_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            PaginaDocenteAdm docadm = new PaginaDocenteAdm(id_doc, aut);
            docadm.Show();
        }

        private void B_sair_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}
