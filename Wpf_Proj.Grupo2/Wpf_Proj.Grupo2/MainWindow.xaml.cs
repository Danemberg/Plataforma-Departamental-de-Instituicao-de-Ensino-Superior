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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_Proj.Grupo2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Entities_Grupo2_Plataforma_Departamental database_connection;
        

        string docenteativo = "nao";
        public MainWindow()
        {
            InitializeComponent();
            
            

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
        public List<Escola> escolas { set; get; }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            database_connection = new Entities_Grupo2_Plataforma_Departamental();
            database_connection.Escolas.Load();
            var itemsource = database_connection.Escolas.ToList();
            escolas = itemsource;
            DataContext = escolas;
            erro_space.Visibility = Visibility.Hidden;
            erro_text.Visibility = Visibility.Hidden;
            botão_continuar.Visibility = Visibility.Hidden;
            


        }
       

        private void Menu_Escolas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = Menu_Escolas.SelectedItem as Escola ;
            int id = item.id;
            this.Hide();
            Paginaescola paginaescola = new Paginaescola(id);
            paginaescola.Show();
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
                            Pagina_Admin admin = new Pagina_Admin(user.id, aut);
                            admin.Show();
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
