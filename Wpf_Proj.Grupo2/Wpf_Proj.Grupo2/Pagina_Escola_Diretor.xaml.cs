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
    /// Lógica interna para Pagina_Escola_Diretor.xaml
    /// </summary>
    public partial class Pagina_Escola_Diretor : Window
    {
        private int id_doc;
        private int aut;
        Entities_Grupo2_Plataforma_Departamental database_Connection;
        public Pagina_Escola_Diretor(int id_doc, int aut)
        {
            InitializeComponent();
            this.id_doc = id_doc;
            this.aut = aut;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            database_Connection = new Entities_Grupo2_Plataforma_Departamental();
            var doc = database_Connection.Docentes.Where(d => d.id_utilizador == id_doc).FirstOrDefault();
            var dir = database_Connection.Diretors.Where(d => d.id_docente == doc.id).FirstOrDefault();
            var esc = database_Connection.Escolas.Where(d => d.id == dir.id_escola).ToList();
            dataGrid_escolas.ItemsSource = esc;


        }

        private void botao_editar_Click(object sender, RoutedEventArgs e)
        {

            var doc = database_Connection.Docentes.Where(d => d.id_utilizador == id_doc).FirstOrDefault();
            var dir = database_Connection.Diretors.Where(d => d.id_docente == doc.id).FirstOrDefault();
            var esc = database_Connection.Escolas.Where(d => d.id == dir.id_escola).ToList();
            var updaterow = database_Connection.Escolas.Where(d => d.id == dir.id_escola).FirstOrDefault();
            if (Txt_nome.Text == "")
            {
                Aviso.Text = "INTRODUZA TODAS AS INFORMAÇÕES";
            }
            else
            {
                updaterow.nome = Txt_nome.Text;
            }


            database_Connection.SaveChanges();
            dataGrid_escolas.ItemsSource = database_Connection.Escolas.Where(d => d.id == dir.id_escola).ToList();
            dataGrid_escolas.Visibility = Visibility.Visible;


        }
        private void B_Voltar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Paginadocente dir = new Paginadocente(id_doc, aut);
            dir.Show();
        }

        private void B_sair_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}
