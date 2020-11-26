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
    /// Lógica interna para Pagina_Departamento_Coordenador.xaml
    /// </summary>

    public partial class Pagina_Departamento_Coordenador : Window
    {
        Entities_Grupo2_Plataforma_Departamental database_Connection;
        
        private int id_doc ;
        
        public Pagina_Departamento_Coordenador(int id_doc)
        {
            InitializeComponent();
            this.id_doc = id_doc;
            
           
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           database_Connection = new Entities_Grupo2_Plataforma_Departamental();
            var doc = database_Connection.Docentes.Where(d => d.id_utilizador == id_doc).FirstOrDefault();
            var coor = database_Connection.Coordenadors.Where(d => d.id_docente == doc.id).FirstOrDefault();
            var dept = database_Connection.Departamentoes.Where(d => d.id == coor.id_departamento).ToList();
            departamentoDataGrid.ItemsSource = dept;
       

        }

        private void botao_editar_Click(object sender, RoutedEventArgs e)
        {

            var doc = database_Connection.Docentes.Where(d => d.id_utilizador == id_doc).FirstOrDefault();
            var coor = database_Connection.Coordenadors.Where(d => d.id_docente == doc.id).FirstOrDefault();
            var dept = database_Connection.Departamentoes.Where(d => d.id == coor.id_departamento).ToList();
            var updaterow = database_Connection.Departamentoes.Where(d => d.id == coor.id_departamento).FirstOrDefault();
            if (Txt_nome.Text == "")
            {
                Aviso.Text = "INTRODUZA TODAS AS INFORMAÇÕES";
            }
            else
            {
                updaterow.nome = Txt_nome.Text;
            }
            

                database_Connection.SaveChanges();
                departamentoDataGrid.ItemsSource = database_Connection.Departamentoes.Where(d => d.id == coor.id_departamento).ToList();
                departamentoDataGrid.Visibility = Visibility.Visible;
            

        }

        private void B_Voltar_Click(object sender, RoutedEventArgs e)
        {
            int aut = 2;
            this.Hide();
            Paginadocente paginadocente = new Paginadocente(id_doc,aut);
            paginadocente.Show();
        }

        private void B_sair_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}

