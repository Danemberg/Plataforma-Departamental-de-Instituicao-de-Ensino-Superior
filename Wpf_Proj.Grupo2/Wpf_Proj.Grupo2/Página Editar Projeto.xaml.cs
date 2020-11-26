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
    /// Lógica interna para Página_Editar_Projeto.xaml
    /// </summary>
    public partial class Página_Editar_Projeto : Window
    {
        Entities_Grupo2_Plataforma_Departamental database_Connection;
        CollectionViewSource projetoViewsource;   //coleção
        private int id_doc;
        private int aut;
        private DateTime data_fim;
        private DateTime data_inicio;
        public Página_Editar_Projeto(int id_doc, int aut)
        {
            this.id_doc = id_doc;
            this.aut = aut;
            InitializeComponent();
            projetoViewsource = ((CollectionViewSource)(FindResource("projetoViewSource")));
            DataContext = this;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            database_Connection = new Entities_Grupo2_Plataforma_Departamental();
            var querry = from p in database_Connection.Projetoes
                         join ap in database_Connection.Autoria_Projeto on p.id equals ap.id_projeto
                         where ap.id_autor == id_doc
                         select p;
            var querrylist = querry.ToList();
            dataGrid_projetos.ItemsSource = querrylist.ToList();

        }

        private void botao_inserir_Click(object sender, RoutedEventArgs e)
        {


            dataGrid_projetos.Visibility = Visibility.Hidden;

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

                    if (Txt_valor_financiado.Text == "")
                    {
                        Aviso.Text = "INTRODUZA TODAS AS INFORMAÇÕES";
                    }
                    else
                    {

                        dataGrid_projetos.Visibility = Visibility.Hidden;
                        Projeto proj = new Projeto();
                        proj.titulo = Txt_titulo.Text;
                        proj.local_realizacao = Txt_local_realizacao.Text;
                        proj.valor_financiado = Convert.ToInt32(Txt_valor_financiado.Text);
                        proj.data_inicio =data_inicio;
                        proj.data_fim =data_fim;
                        database_Connection.Projetoes.Add(proj);
                        Autoria_Projeto aut_proj = new Autoria_Projeto();
                        aut_proj.id_autor = id_doc;
                        aut_proj.id_projeto = proj.id;
                        database_Connection.Autoria_Projeto.Add(aut_proj);
                        dataGrid_projetos.Visibility = Visibility.Visible;

                    }

                }

                database_Connection.SaveChanges();
                dataGrid_projetos.Visibility = Visibility.Visible;
                dataGrid_projetos.ItemsSource = database_Connection.Projetoes.ToList();


            }





        }

        private void botao_excluir_Click(object sender, RoutedEventArgs e)
        {
            dataGrid_projetos.Visibility = Visibility.Hidden;
            if (Txt_id.Text == "")
            {
                Aviso.Text = "INTRODUZA TODAS AS INFORMAÇÕES";
            }
            else
            {

                Aviso.Text = "";
                Projeto proj = new Projeto();


                int num = Convert.ToInt32(Txt_id.Text);
                proj = database_Connection.Projetoes.Find(num);

                database_Connection.Projetoes.Remove(proj);
                database_Connection.SaveChanges();
                dataGrid_projetos.ItemsSource = database_Connection.Projetoes.ToList();
                dataGrid_projetos.Visibility = Visibility.Visible;


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
                dataGrid_projetos.Visibility = Visibility.Hidden;
                Aviso.Text = "";
                var updateRow = database_Connection.Projetoes.Where(w => w.id == num).FirstOrDefault();
                if (Txt_titulo.Text == "")
                {

                    updateRow.local_realizacao = Txt_local_realizacao.Text;
                    updateRow.valor_financiado = Convert.ToInt32(Txt_valor_financiado.Text);
                    updateRow.data_fim =data_fim;

                }
                else if (Txt_local_realizacao.Text == "")
                {

                    updateRow.titulo = Txt_titulo.Text;
                    updateRow.valor_financiado = Convert.ToInt32(Txt_valor_financiado.Text);
                    updateRow.data_fim = data_fim;
                }
                else if (Txt_valor_financiado.Text == "")
                {

                    updateRow.titulo = Txt_titulo.Text;
                    updateRow.local_realizacao = Txt_local_realizacao.Text;
                    updateRow.data_fim = data_fim;

                }

                database_Connection.SaveChanges();


                dataGrid_projetos.ItemsSource = database_Connection.Projetoes.ToList();
                dataGrid_projetos.Visibility = Visibility.Visible;

            }

        }

        private void B_Voltar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Paginadocente doc = new Paginadocente(id_doc, aut);
            doc.Show();
        }
        private void date_picker_fim_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime item = Convert.ToDateTime(date_picker_fim.SelectedDate);
            this.data_fim = item.Date;

        }
        private void date_picker_inicio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime item = Convert.ToDateTime(date_picker_inicio.SelectedDate);
            this.data_inicio = item.Date;

        }
        private void B_sair_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}
