using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Entity;
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
    /// Interaction logic for Paginadepartamento.xaml
    /// </summary>
    public partial class Paginadepartamento : Window
    {
        Entities_Grupo2_Plataforma_Departamental database_connection;
        CollectionViewSource docViewsource;
        private int id_escola;
        private int id_dept;
        private int aut = 5;
        public Paginadepartamento(int id, int id_escola)
        {
            InitializeComponent();
            this.id_escola = id_escola;
            this.id_dept = id;
            docViewsource = ((CollectionViewSource)(FindResource("docenteViewSource")));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            database_connection = new Entities_Grupo2_Plataforma_Departamental();
            database_connection.Departamentoes.Load();
            database_connection.Escolas.Load();
            int dept = id_dept;
            


            var itemsource_deptart = database_connection.Departamentoes.Where(d => d.id == dept).FirstOrDefault();
            var itemsource_coor = database_connection.Coordenadors.Where(d => d.id_departamento == dept).FirstOrDefault();
            int id_dir = itemsource_coor.id_docente;
            var itemsource_docente = database_connection.Docentes.Where(d => d.id == id_dir).FirstOrDefault();
            int id_doc = itemsource_docente.id_utilizador;
            var itemsource_user = database_connection.Utilizadors.Where(d => d.id == id_doc).FirstOrDefault();
                nome_dept.Text = itemsource_deptart.nome;
                nome_coor.Text = itemsource_docente.nome;
                email_coor.Text = itemsource_user.email;
            var itemsource_doc_dept = database_connection.Docentes.Where(d => d.id_departamento == id_dept).ToList();

            var query = from o in itemsource_doc_dept
                        join c in database_connection.Utilizadors on o.id_utilizador equals c.id
                        select new ViewModel
                        {
                            Nome = o.nome,
                            Email = c.email,
                        };
            dataGrid_docentes.ItemsSource = query.ToList();
        }
        private void ScrollBar_TargetUpdated(object sender, DataTransferEventArgs e)
        {

        }

        private void B_Voltar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            int id = this.id_escola;
            Paginaescola paginaescola = new Paginaescola(id);
            paginaescola.Show();

        }
        struct mostrarDocente
        {
            public string Nome { get; set; }
            public string Email { get; set; }
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            if (inserir_utilizador.Text == "")
            {
                erro_space.Visibility = Visibility.Visible;
                erro_text.Visibility = Visibility.Visible;
                botão_continuar.Visibility = Visibility.Visible;
            }
            else
            {
                int login = Convert.ToInt32(inserir_utilizador.Text);
                int aut = -1;
                var user = database_connection.Utilizadors.Where(d => d.id == login).FirstOrDefault();
                Utilizador user2 = new Utilizador();

               
                    string pass = inserir_senha.Password;
                    if (login > 0)
                    {

                        if (user != null)
                        {
                            if (pass == "")
                            {
                                erro_space.Visibility = Visibility.Visible;
                                erro_text.Visibility = Visibility.Visible;
                                botão_continuar.Visibility = Visibility.Visible;
                            }
                            if (pass == user.password)
                            {
                                if (user.tipo == "diretor")
                                    aut = 1;
                                if (user.tipo == "coordenador")
                                    aut = 2;
                                if (user.tipo == "docente")
                                    aut = 3;
                                this.Hide();
                                Paginadocente paginadocente = new Paginadocente(login, aut);
                                paginadocente.Show();
                            }
                            else
                            {
                                erro_space.Visibility = Visibility.Visible;
                                erro_text.Visibility = Visibility.Visible;
                                botão_continuar.Visibility = Visibility.Visible;
                            }
                        }
                        else
                        {
                            erro_space.Visibility = Visibility.Visible;
                            erro_text.Visibility = Visibility.Visible;
                            botão_continuar.Visibility = Visibility.Visible;
                        }
                    }
                    if (login == 0)
                    {
                        if (pass == user.password)
                        {
                            aut = 0;
                            this.Hide();
                            Paginadocente paginadocente = new Paginadocente(user.id, aut);
                            paginadocente.Show();
                        }
                        else
                        {
                            erro_space.Visibility = Visibility.Visible;
                            erro_text.Visibility = Visibility.Visible;
                            botão_continuar.Visibility = Visibility.Visible;
                        }
                    }
                

                else
                {
                    erro_space.Visibility = Visibility.Visible;
                    erro_text.Visibility = Visibility.Visible;
                    botão_continuar.Visibility = Visibility.Visible;
                }
            }
        }

        private void grid_click (object sender, RoutedEventArgs e)
            {
                DataGrid dataGrid = dataGrid_docentes;
                DataGridRow Row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
                DataGridCell RowAndColumn = (DataGridCell)dataGrid.Columns[0].GetCellContent(Row).Parent;
                string nome_cell = ((TextBlock)RowAndColumn.Content).Text;
                int id_doc = -1;
                aut = 5;
                var doc = database_connection.Docentes.Where(d => d.nome == nome_cell).FirstOrDefault();
                var user = database_connection.Utilizadors.Where(d => d.id == doc.id_utilizador).FirstOrDefault();
            id_doc = user.id;    
            this.Hide();
                Paginadocente paginadocente = new Paginadocente(id_doc, aut);
            paginadocente.Show();
            }
        private void B_sair_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow main = new MainWindow();
            main.Show();
        }
        private void botão_continuar_Click(object sender, RoutedEventArgs e)
        {
            erro_space.Visibility = Visibility.Hidden;
            erro_text.Visibility = Visibility.Hidden;
            botão_continuar.Visibility = Visibility.Hidden;
        }
    }
}
