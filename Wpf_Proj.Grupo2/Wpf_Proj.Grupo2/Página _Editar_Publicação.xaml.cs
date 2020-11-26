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
    /// Lógica interna para Página__Editar_Publicação.xaml
    /// </summary>
    public partial class Página__Editar_Publicação : Window
    {
        Entities_Grupo2_Plataforma_Departamental database_Connection;
        CollectionViewSource publicacaoViewsource;
        private int id_doc;
        private int aut;
        private DateTime data;

        public Página__Editar_Publicação(int id_doc, int aut)
        {
            this.id_doc = id_doc;
            this.aut = aut;
            InitializeComponent();
            publicacaoViewsource = ((CollectionViewSource)(FindResource("publicacaoViewSource")));
            DataContext = this;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            database_Connection = new Entities_Grupo2_Plataforma_Departamental();
            var querry2 = from p in database_Connection.Publicacaos
                          join ap in database_Connection.Autoria_Publicacao on p.id equals ap.id_publicacao
                          where ap.id_autor == id_doc
                          select p;
            var querry2list = querry2.ToList();
            dataGrid_publicacoes.ItemsSource = querry2list.ToList();
            

        }

        private void botao_inserir_Click(object sender, RoutedEventArgs e)
        {


            dataGrid_publicacoes.Visibility = Visibility.Hidden;

            if (Txt_titulo.Text == "")
            {
                Aviso.Text = "INTRODUZA TODAS AS INFORMAÇÕES";
            }
            else
            {

                Aviso.Text = "";

                if (Txt_local_realizacao.Text == "")
                {
                    Aviso.Text = "INTRODUZA TODAS AS INFORMAÇÕES";
                }
                else
                {
                    Aviso.Text = "";

                    if (Txt_tipo.Text == "")
                    {
                        Aviso.Text = "INTRODUZA TODAS AS INFORMAÇÕES";
                    }
                    else
                    {

                        dataGrid_publicacoes.Visibility = Visibility.Hidden;
                        Publicacao pub1 = new Publicacao();
                        Autoria_Publicacao aut_pub1 = new Autoria_Publicacao();
                        pub1.titulo = Txt_titulo.Text;
                        pub1.local_realizacao = Txt_local_realizacao.Text;
                        pub1.tipo = Txt_tipo.Text;
                        pub1.data_finalizacao = data.Date;
                        database_Connection.Publicacaos.Add(pub1);
                        aut_pub1.id_autor = id_doc;
                        aut_pub1.id_publicacao = pub1.id;
                        database_Connection.Autoria_Publicacao.Add(aut_pub1);


                        dataGrid_publicacoes.Visibility = Visibility.Visible;

                    }

                }

                database_Connection.SaveChanges();
                dataGrid_publicacoes.Visibility = Visibility.Visible;
                dataGrid_publicacoes.ItemsSource = database_Connection.Publicacaos.ToList();


            }





        }

        private void botao_excluir_Click(object sender, RoutedEventArgs e)
        {
            dataGrid_publicacoes.Visibility = Visibility.Hidden;
            if (Txt_id.Text == "")
            {
                Aviso.Text = "INTRODUZA TODAS AS INFORMAÇÕES";
            }
            else
            {

                Aviso.Text = "";
                Publicacao pub1 = new Publicacao();


                int num = Convert.ToInt32(Txt_id.Text);
                pub1 = database_Connection.Publicacaos.Find(num);


                database_Connection.Publicacaos.Remove(pub1);
                database_Connection.SaveChanges();
                dataGrid_publicacoes.ItemsSource = database_Connection.Publicacaos.ToList();
                dataGrid_publicacoes.Visibility = Visibility.Visible;


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
                dataGrid_publicacoes.Visibility = Visibility.Hidden;
                Aviso.Text = "";
                var updateRow = database_Connection.Publicacaos.Where(w => w.id == num).FirstOrDefault();
                if (Txt_titulo.Text == "")
                {

                    updateRow.local_realizacao = Txt_local_realizacao.Text;
                    updateRow.tipo = Txt_tipo.Text;
                    updateRow.data_finalizacao = data;

                }
                else if (Txt_local_realizacao.Text == "")
                {

                    updateRow.titulo = Txt_titulo.Text;
                    updateRow.tipo = Txt_tipo.Text;
                    updateRow.data_finalizacao = data;
                }
                else if (Txt_tipo.Text == "")
                {

                    updateRow.titulo = Txt_titulo.Text;
                    updateRow.local_realizacao = Txt_local_realizacao.Text;
                    updateRow.data_finalizacao = data;

                }

                database_Connection.SaveChanges();


                dataGrid_publicacoes.ItemsSource = database_Connection.Publicacaos.ToList();
                dataGrid_publicacoes.Visibility = Visibility.Visible;

            }

        }

        private void B_Voltar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Paginadocente doc = new Paginadocente(id_doc, aut);
            doc.Show();
        }
        private void date_picker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime item = Convert.ToDateTime(data_picker.SelectedDate);
            this.data = item.Date;
            
        }
        private void B_sair_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}