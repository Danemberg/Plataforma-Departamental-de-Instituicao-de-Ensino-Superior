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
    /// Interaction logic for PaginaDocenteAdm.xaml
    /// </summary>
    
    public partial class PaginaDocenteAdm : Window
    {
        Entities_Grupo2_Plataforma_Departamental database_Connection;
        CollectionViewSource docViewsource;
        private int id_doc;
        private int aut;

        public object Txt_id_utilizador { get; private set; }

        public PaginaDocenteAdm(int id, int aut)
        {
            InitializeComponent();
            docViewsource = ((CollectionViewSource)(FindResource("docenteViewSource")));
            DataContext = this;
            this.id_doc = id;
            this.aut = aut;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            database_Connection = new Entities_Grupo2_Plataforma_Departamental();
            dataGrid_docentes.ItemsSource = database_Connection.Docentes.ToList();

        }

        private void botao_inserir_Click(object sender, RoutedEventArgs e)
        {


            var docente3 = database_Connection.Docentes.ToList();
            dataGrid_docentes.Visibility = Visibility.Hidden;

            if (Txt_Id_utilizador.Text == "")
            {
                Aviso.Text = "INTRODUZA TODAS AS INFORMAÇÕES";
            }
            else
            {

                Aviso.Text = "";

                if (Txt_nome.Text == "")
                {
                    Aviso.Text = "INTRODUZA TODAS AS INFORMAÇÕES";
                }
                else
                {

                    if (Txt_id_departamento.Text == "")
                    {
                        Aviso.Text = "INTRODUZA TODAS AS INFORMAÇÕES";
                    }
                    else
                    {

                        dataGrid_docentes.Visibility = Visibility.Hidden;
                        Docente docente1 = new Docente();
                        docente1.id_departamento = Convert.ToInt32(Txt_id_departamento.Text);
                        docente1.nome = Txt_nome.Text;
                        database_Connection.Docentes.Add(docente1);


                        dataGrid_docentes.Visibility = Visibility.Visible;
                    }

                }

            }

            database_Connection.SaveChanges();
            dataGrid_docentes.Visibility = Visibility.Visible;
            dataGrid_docentes.ItemsSource = database_Connection.Docentes.ToList();


        }

        private void botao_excluir_Click(object sender, RoutedEventArgs e)
        {
            dataGrid_docentes.Visibility = Visibility.Hidden;
            if (Txt_id.Text == "")
            {
                Aviso.Text = "INTRODUZA TODAS AS INFORMAÇÕES";
            }
            else
            {

                Aviso.Text = "";
                Docente docentes = new Docente();
                Diretor diretor = new Diretor();
                Coordenador coordenadores = new Coordenador();
                Autoria_Projeto autorproj = new Autoria_Projeto();
                Autoria_Publicacao autorpub = new Autoria_Publicacao();


           
                int num = Convert.ToInt32(Txt_id.Text);
                docentes = database_Connection.Docentes.Find(num);
                diretor = database_Connection.Diretors.Find(num);
                coordenadores = database_Connection.Coordenadors.Find(num);
                autorproj = database_Connection.Autoria_Projeto.Find(num);
                autorpub = database_Connection.Autoria_Publicacao.Find(num);

       

                database_Connection.Docentes.Remove(docentes);
                if (diretor != null)
                {
                    database_Connection.Diretors.Remove(diretor);
                }
                if (coordenadores != null)
                {
                    database_Connection.Coordenadors.Remove(coordenadores);
                }
                if(autorproj != null)
                {
                    database_Connection.Autoria_Projeto.Remove(autorproj);
                }
                if (autorpub != null)
                {
                    database_Connection.Autoria_Publicacao.Remove(autorpub);

                }



                database_Connection.SaveChanges();
                dataGrid_docentes.ItemsSource = database_Connection.Docentes.ToList();
                dataGrid_docentes.Visibility = Visibility.Visible;




            }

        }

        private void botao_editar_Click(object sender, RoutedEventArgs e)
        {
            int num = Convert.ToInt32(Txt_id.Text);
            if (Txt_id.Text == "")
            {
                Aviso.Text = "INTRODUZA TODAS AS INFORMAÇÕES";
            }
            else
            {
                dataGrid_docentes.Visibility = Visibility.Hidden;
                Aviso.Text = "";
                var updateRow = database_Connection.Docentes.Where(w => w.id == num).FirstOrDefault();
                if (Txt_Id_utilizador.Text == "")
                {
                    updateRow.nome = Txt_nome.Text;
                    updateRow.id_departamento = Convert.ToInt32(Txt_id_departamento.Text);

                }
                else if (Txt_nome.Text == "")
                {
                    updateRow.id_utilizador = Convert.ToInt32(Txt_Id_utilizador.Text);
                    updateRow.id_departamento = Convert.ToInt32(Txt_id_departamento.Text);
                }
                else if (Txt_id_departamento.Text == "")
                {
                    updateRow.id_utilizador = Convert.ToInt32(Txt_Id_utilizador.Text);
                    updateRow.nome = Txt_nome.Text;

                }
                else
                {
                    updateRow.id_utilizador = Convert.ToInt32(Txt_Id_utilizador.Text);
                    updateRow.nome = Txt_nome.Text;
                    updateRow.id_departamento = Convert.ToInt32(Txt_id_departamento.Text);

                }
                database_Connection.SaveChanges();
                dataGrid_docentes.ItemsSource = database_Connection.Docentes.ToList();
                dataGrid_docentes.Visibility = Visibility.Visible;

            }
        }
        private void B_Voltar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Pagina_Admin admin = new Pagina_Admin(id_doc, aut);
            admin.Show();
        }

        private void B_sair_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow main = new MainWindow();
            main.Show();
        }

    }
}



