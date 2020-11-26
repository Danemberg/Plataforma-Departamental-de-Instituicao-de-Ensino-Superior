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
    /// Interaction logic for Paginadocente.xaml
    /// </summary>
    public partial class Paginadocente : Window
    {
        Entities_Grupo2_Plataforma_Departamental db;
        private int id_doc;
        private int aut;
        private int id_dept;
        private int id_esc;
        private int edit = 0;
        private int mudar = 0;
        CollectionViewSource projetoViewSource;
        CollectionViewSource publicacaoViewSource;
        public Paginadocente(int id_doc, int aut)
        {
            InitializeComponent();
            this.id_doc = id_doc;
            this.aut = aut;
            projetoViewSource = ((CollectionViewSource)(FindResource("projetoViewSource")));
            publicacaoViewSource = ((CollectionViewSource)(FindResource("publicacaoViewSource")));
            DataContext = this;
        }
        private void B_Voltar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Paginadepartamento ss = new Paginadepartamento(id_dept, id_esc);
            ss.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db = new Entities_Grupo2_Plataforma_Departamental();
            db.Projetoes.Load();
            db.Publicacaos.Load();
            db.Docentes.Load();
            int id_doc1 = id_doc;
            var user = db.Utilizadors.Where(d => d.id == id_doc1).FirstOrDefault();
            var doc = db.Docentes.Where(d => d.id_utilizador == user.id).FirstOrDefault();
            
            
            if(aut != 5)
            {
                if (aut != 0)
                {
                    var dept = db.Departamentoes.Where(d => d.id == doc.id_departamento).FirstOrDefault();
                    this.id_dept = Convert.ToInt32(dept.id);
                    this.id_esc = Convert.ToInt32(dept.id_escola);
                    inserir_utilizador.Visibility = Visibility.Hidden;
                    inserir_senha.Visibility = Visibility.Hidden;
                    Utilizador.Visibility = Visibility.Hidden;
                    senha.Visibility = Visibility.Hidden;
                    login.Visibility = Visibility.Hidden;
                    B_sair.Visibility = Visibility.Visible;
                    menu_proj.Visibility = Visibility.Visible;
                    menu_pub.Visibility = Visibility.Visible;
                    editar_perfil.Visibility = Visibility.Visible;

                }
                if(aut == 1)
                {
                    esc_dir.Visibility = Visibility.Visible;
                    dept_dir.Visibility = Visibility.Visible;
                }
                if(aut == 2)
                {
                    dept_coor.Visibility = Visibility.Visible;
                }
            }
            if(aut == 5)
            {
                var dept = db.Departamentoes.Where(d => d.id == doc.id_departamento).FirstOrDefault();
                this.id_dept = Convert.ToInt32(dept.id);
                this.id_esc = Convert.ToInt32(dept.id_escola);
            }

            var proj_itemsource = db.Projetoes.Local;
            var querry = from p in db.Projetoes
                    join ap in db.Autoria_Projeto on p.id equals ap.id_projeto
                    where ap.id_autor == id_doc
                    select p;
            var querrylist = querry.ToList();
            projetoViewSource.Source = querrylist.ToList();
            var querry2 = from p in db.Publicacaos
                          join ap in db.Autoria_Publicacao on p.id equals ap.id_publicacao
                          where ap.id_autor == id_doc
                          select p;
            var querry2list = querry2.ToList();
            publicacaoViewSource.Source = querry2list.ToList();
            nome_txt.Text = doc.nome;
            txt_Funcao.Text = user.tipo;
            txt_email.Text = user.email;
        }

        private void B_sair_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow main = new MainWindow();
            main.Show();
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
                var user = db.Utilizadors.Where(d => d.id == login).FirstOrDefault();
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

        private void proj_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Página_Editar_Projeto pagina_proj = new Página_Editar_Projeto(id_doc,aut) ;
            pagina_proj.Show();
        }

        private void menu_pub_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Página__Editar_Publicação pagina_pub = new Página__Editar_Publicação(id_doc, aut);
            pagina_pub.Show();
        }

        private void editar_perfil_Click(object sender, RoutedEventArgs e)
        {
            
            if (edit == 0)
            {
                nome_txt_edit.Visibility = Visibility.Visible;
                nome_txt.Visibility = Visibility.Hidden;
                txt_Funcao.Visibility = Visibility.Hidden;
                txt_email_edit.Visibility = Visibility.Visible;
                txt_email.Visibility = Visibility.Hidden;
                editar_perfil_confirm.Visibility = Visibility.Visible;
                var user_email = db.Utilizadors.Where(d => d.id == id_doc).FirstOrDefault();
                var doc_name  = db.Docentes.Where(d => d.id_utilizador == user_email.id).FirstOrDefault();
                nome_txt_edit.Text = Convert.ToString(doc_name.nome);
                txt_email_edit.Text = Convert.ToString(user_email.email);
                
                
                if (mudar == 1)
                {
                    edit = 1;
                    mudar = 0;
                    
                }
                if (mudar == 0)
                {
                    mudar = 1;
                }
                
            }
            if (edit == 1)
            {
                nome_txt_edit.Visibility = Visibility.Hidden;
                nome_txt.Visibility = Visibility.Visible;
                txt_Funcao.Visibility = Visibility.Visible;
                txt_email_edit.Visibility = Visibility.Hidden;
                txt_email.Visibility = Visibility.Visible;
                editar_perfil_confirm.Visibility = Visibility.Hidden;
                var user_email = db.Utilizadors.Where(d => d.id == id_doc).FirstOrDefault();
                var doc_name = db.Docentes.Where(d => d.id_utilizador == user_email.id).FirstOrDefault();
                mudar = 0;
                edit = 0;
                nome_txt.Text = Convert.ToString(doc_name.nome);
                txt_email.Text = Convert.ToString(user_email.email);
                txt_Funcao.Text = Convert.ToString(user_email.tipo);
            }

        }

        private void editar_perfil_confirm_Click(object sender, RoutedEventArgs e)
        {
          
            
            var user_updt = db.Utilizadors.Where(d => d.id == id_doc).FirstOrDefault();
            var updt_doc = db.Docentes.Where(d => d.id_utilizador == user_updt.id).FirstOrDefault();
            updt_doc.nome = nome_txt_edit.Text;
            user_updt.email = txt_email_edit.Text;
            db.SaveChanges();
            
             
        }

        private void dept_coor_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Pagina_Departamento_Coordenador dept_coor = new Pagina_Departamento_Coordenador(id_doc);
            dept_coor.Show();
        }

        private void dept_dir_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Pagina_Departamento_Diretor dept_dir = new Pagina_Departamento_Diretor(id_doc,aut);
            dept_dir.Show();
        }

        private void esc_dir_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Pagina_Escola_Diretor esc_dir = new Pagina_Escola_Diretor(id_doc,aut);
            esc_dir.Show();
        }
        private void botão_continuar_Click(object sender, RoutedEventArgs e)
        {
            erro_space.Visibility = Visibility.Hidden;
            erro_text.Visibility = Visibility.Hidden;
            botão_continuar.Visibility = Visibility.Hidden;
        }
    }
}
