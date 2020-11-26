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
    /// Interaction logic for PaginaDepartamentoAdm.xaml
    /// </summary>
    public partial class PaginaDepartamentoAdm : Window
    {
        Entities_Grupo2_Plataforma_Departamental database_Connection;
        CollectionViewSource deptViewsource;
        private int id_doc;
        private int aut;
        public PaginaDepartamentoAdm(int id , int aut)
        {
            InitializeComponent();
            deptViewsource = ((CollectionViewSource)(FindResource("departamentoViewSource")));
            DataContext = this;
            this.aut = aut;
            this.id_doc = id;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            database_Connection = new Entities_Grupo2_Plataforma_Departamental();
            dataGrid_departamentos.ItemsSource = database_Connection.Departamentoes.ToList();

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
            dataGrid_departamentos.ItemsSource = database_Connection.Departamentoes.ToList();


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
                departamento  = database_Connection.Departamentoes.Find(num);
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
                dataGrid_departamentos.ItemsSource = database_Connection.Departamentoes.ToList();
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


                dataGrid_departamentos.ItemsSource = database_Connection.Departamentoes.ToList();
                dataGrid_departamentos.Visibility = Visibility.Visible;
                
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

    
