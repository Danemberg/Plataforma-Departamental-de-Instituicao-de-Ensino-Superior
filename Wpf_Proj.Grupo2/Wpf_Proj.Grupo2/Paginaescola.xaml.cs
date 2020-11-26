using System;
using System.Collections.Generic;
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
    /// Interaction logic for Paginaescola.xaml
    /// </summary>
    public partial class Paginaescola : Window
    {
        Entities_Grupo2_Plataforma_Departamental database_connection;
        string docenteativo = "nao";
        private int id_escola;
        public Paginaescola(int id)
        {
            InitializeComponent();
            this.id_escola = id;
            if (docenteativo == "nao")
            {
                inserir_utilizador.Visibility = Visibility.Visible;
                inserir_senha.Visibility = Visibility.Visible;
                Utilizador.Visibility = Visibility.Visible;
                senha.Visibility = Visibility.Visible;
                B_sair.Visibility = Visibility.Hidden;
            }
            else
            {
                inserir_utilizador.Visibility = Visibility.Hidden;
                inserir_senha.Visibility = Visibility.Hidden;
                Utilizador.Visibility = Visibility.Hidden;
                senha.Visibility = Visibility.Hidden;
                B_sair.Visibility = Visibility.Visible;
            }
        }
        public List<Departamento> departamento { set; get; }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            database_connection = new Entities_Grupo2_Plataforma_Departamental();
            database_connection.Departamentoes.Load();
            database_connection.Escolas.Load();
            int id_dir = -1;
            int id_doc = -1;
            var itemsource_escola = database_connection.Escolas.Where(d => d.id == id_escola).ToList();
            var itemsource_dir = database_connection.Diretors.Where(d => d.id_escola == id_escola).ToList();
            foreach (Diretor diretor in itemsource_dir)
                id_dir = diretor.id_docente;
            var itemsource_docente = database_connection.Docentes.Where(d => d.id == id_dir).ToList();
            foreach (Docente docente1 in itemsource_docente)
                id_doc = docente1.id_utilizador;
            var itemsource_user = database_connection.Utilizadors.Where(d => d.id == id_doc).ToList();
            var itemsource_dept = database_connection.Departamentoes.Where(d => d.id_escola == id_escola).ToList();
            departamento = itemsource_dept;
            DataContext = departamento;
            foreach (Escola escola in itemsource_escola)
                nome_escola.Text = escola.sigla;
            foreach (Docente docente in itemsource_docente)
                nome_dir.Text = docente.nome;
            foreach (Utilizador user in itemsource_user)
                email_dir.Text = user.email;

        }

        private void B_Voltar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow ss = new MainWindow();
            ss.Show();
        }

        private void Menu_DEPT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = Menu_DEPT.SelectedItem as Departamento;
            int id_dept = item.id;
            int id_escola = this.id_escola;
            this.Hide();
            Paginadepartamento paginadepartamento = new Paginadepartamento(id_dept, id_escola);
            paginadepartamento.Show();
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

        private void botão_inform_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Pagina_informac ss = new Pagina_informac();
            ss.Show();
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
