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
    /// Lógica interna para Pagina_Departamento_Diretor.xaml
    /// </summary>
    public partial class Pagina_Departamento_Diretor : Window
    {
        Entities_Grupo2_Plataforma_Departamental database_Connection;
        private int id_doc;
        private int aut;
        public Pagina_Departamento_Diretor(int id_doc, int aut)
        { 
            InitializeComponent();
            this.id_doc = id_doc;
            this.aut = aut;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            database_Connection = new Entities_Grupo2_Plataforma_Departamental();
            var doc = database_Connection.Docentes.Where(d => d.id_utilizador == id_doc).FirstOrDefault();
            var diretor = database_Connection.Diretors.Where(d => d.id_docente == doc.id).FirstOrDefault();
            var dept = database_Connection.Departamentoes.Where(d => d.id_escola == diretor.id_escola).ToList();
           // var query = from d in database_Connection.Departamentoes join dr in database_Connection.Diretors on d.id_escola equals dr.id_escola select d;
           // var querylist = query.ToList();
            dataGrid_departamentos.ItemsSource = dept.ToList() ;
        }
        private void botao_inserir_Click(object sender, RoutedEventArgs e)
        {


            var escola3 = database_Connection.Escolas.ToList();
            dataGrid_departamentos.Visibility = Visibility.Hidden;

            if (Txt_nome.Text == "")
            {
                Aviso.Text = "INTRODUZA TODAS AS INFORMAÇÕES";
            }
            else
            {

                Aviso.Text = "";

                if (Txt_id_escola.Text == "")
                {
                    Aviso.Text = "INTRODUZA TODAS AS INFORMAÇÕES";
                }
                else
                {

                    dataGrid_departamentos.Visibility = Visibility.Hidden;
                    Departamento departamento1 = new Departamento();
                    departamento1.id_escola = Convert.ToInt32(Txt_id_escola.Text);
                    departamento1.nome = Txt_nome.Text;
                    database_Connection.Departamentoes.Add(departamento1);


                    dataGrid_departamentos.Visibility = Visibility.Visible;

                }

            }

            database_Connection.SaveChanges();
            dataGrid_departamentos.Visibility = Visibility.Visible;
            var diretor = database_Connection.Diretors.Where(d => d.id_docente == id_doc).FirstOrDefault();
            var dept = database_Connection.Departamentoes.Where(d => d.id_escola == diretor.id_escola).ToList();
            dataGrid_departamentos.ItemsSource = dept.ToList();


        }

        private void botao_excluir_Click(object sender, RoutedEventArgs e)
        {
            dataGrid_departamentos.Visibility = Visibility.Hidden;
            if (Txt_id.Text == "")
            {
                Aviso.Text = "INTRODUZA TODAS AS INFORMAÇÕES";
            }
            else
            {

                Aviso.Text = "";
                Departamento departamento = new Departamento();
                Coordenador coordenadores = new Coordenador();



                int num = Convert.ToInt32(Txt_id.Text);
                departamento = database_Connection.Departamentoes.Find(num);
                coordenadores = database_Connection.Coordenadors.Find(num);




                if (departamento != null)
                {
                    database_Connection.Departamentoes.Remove(departamento);
                }
                if (coordenadores != null)
                {
                    database_Connection.Coordenadors.Remove(coordenadores);
                }




                database_Connection.SaveChanges();
                var diretor = database_Connection.Diretors.Where(d => d.id_docente == id_doc).FirstOrDefault();
                var dept = database_Connection.Departamentoes.Where(d => d.id_escola == diretor.id_escola).ToList();
                dataGrid_departamentos.ItemsSource = dept.ToList();
                dataGrid_departamentos.Visibility = Visibility.Visible;




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
                dataGrid_departamentos.Visibility = Visibility.Hidden;
                Aviso.Text = "";
                var updateRow = database_Connection.Departamentoes.Where(w => w.id == num).FirstOrDefault();
                if (Txt_nome.Text == "")
                {

                    updateRow.id_escola = Convert.ToInt32(Txt_id_escola.Text);
                }
                else if (Txt_id_escola.Text == "")
                {

                    updateRow.nome = Txt_nome.Text;
                }
                else
                {
                    updateRow.id_escola = Convert.ToInt32(Txt_id_escola.Text);
                    updateRow.nome = Txt_nome.Text;


                }

                database_Connection.SaveChanges();


                var diretor = database_Connection.Diretors.Where(d => d.id_docente == id_doc).FirstOrDefault();
                var dept = database_Connection.Departamentoes.Where(d => d.id_escola == diretor.id_escola).ToList();
                dataGrid_departamentos.ItemsSource = dept.ToList();
                dataGrid_departamentos.Visibility = Visibility.Visible;

            }
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
