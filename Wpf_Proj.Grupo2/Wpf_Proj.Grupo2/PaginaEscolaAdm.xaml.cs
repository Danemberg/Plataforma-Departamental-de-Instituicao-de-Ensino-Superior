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
    /// Interaction logic for PaginaEscolaAdm.xaml
    /// </summary>
    public partial class PaginaEscolaAdm : Window
    {
        Entities_Grupo2_Plataforma_Departamental database_Connection;
        CollectionViewSource escViewsource;
        private int id_doc;
        private int aut;
        public PaginaEscolaAdm(int id, int aut)
        {
            this.id_doc = id;
            this.aut = aut;
            InitializeComponent();
            escViewsource = ((CollectionViewSource)(FindResource("escolaViewSource")));
            DataContext = this;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            database_Connection = new Entities_Grupo2_Plataforma_Departamental();
            dataGrid_escolas.ItemsSource = database_Connection.Escolas.ToList();

        }

        private void botao_inserir_Click(object sender, RoutedEventArgs e)
        {


            var escola3 = database_Connection.Escolas.ToList();
            dataGrid_escolas.Visibility = Visibility.Hidden;

            if (Txt_id_instituicao.Text == "")
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
                    Aviso.Text = "";



                    if (Txt_sigla.Text == "")
                    {
                        Aviso.Text = "INTRODUZA TODAS AS INFORMAÇÕES";
                    }
                    else
                    {

                        dataGrid_escolas.Visibility = Visibility.Hidden;
                        Escola escola1 = new Escola();
                        escola1.id_instituição = Convert.ToInt32(Txt_id_instituicao.Text);
                        escola1.nome = Txt_nome.Text;
                        escola1.sigla = Txt_sigla.Text;
                        database_Connection.Escolas.Add(escola1);
                       
                        dataGrid_escolas.Visibility = Visibility.Visible;
                    }
                    dataGrid_escolas.Visibility = Visibility.Visible;



                }
               
            }
        
            database_Connection.SaveChanges();
            dataGrid_escolas.Visibility = Visibility.Visible;
            dataGrid_escolas.ItemsSource = database_Connection.Escolas.ToList();


        }

        private void botao_excluir_Click(object sender, RoutedEventArgs e)
        {
            dataGrid_escolas.Visibility = Visibility.Hidden;
            if (Txt_id.Text == "")
            {
                Aviso.Text = "INTRODUZA TODAS AS INFORMAÇÕES";
            }
            else
            {
                
                Aviso.Text = "";
                Escola escola = new Escola();
                Departamento departamentos = new Departamento();
                Coordenador coordenadores = new Coordenador();
                Instituicao instituições = new Instituicao();
                Diretor diretores = new Diretor();
                

                int dept = -1;
                int num = Convert.ToInt32(Txt_id.Text);
                escola = database_Connection.Escolas.Find(num);
              
                var departamento2 = database_Connection.Departamentoes.Where(d => d.id_escola == num).ToList();
                foreach (Departamento departamento1 in departamento2)
                    dept = departamento1.id;
                var departamento = database_Connection.Departamentoes.Where(d => d.id == dept).FirstOrDefault();
                var coordenador = database_Connection.Coordenadors.Where(d => d.id_departamento == dept).FirstOrDefault();
                var diretor = database_Connection.Diretors.Where(d => d.id_escola == num).FirstOrDefault();

                database_Connection.Escolas.Remove(escola);
                if (dept > 0)
                {
                    database_Connection.Departamentoes.Remove(departamento);
                    database_Connection.Coordenadors.Remove(coordenador);
                    database_Connection.Diretors.Remove(diretor);

                }
                
    
                database_Connection.SaveChanges();
                dataGrid_escolas.ItemsSource = database_Connection.Escolas.ToList();
                dataGrid_escolas.Visibility = Visibility.Visible;

                
      


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
                dataGrid_escolas.Visibility = Visibility.Hidden;
                Aviso.Text = "";
                var updateRow = database_Connection.Escolas.Where(w => w.id == num).FirstOrDefault();
                if (Txt_id_instituicao.Text == "")
                {
                    updateRow.nome = Txt_nome.Text;
                    updateRow.sigla = Txt_sigla.Text;
                }
                else if (Txt_nome.Text == "")
                {
                    updateRow.id_instituição = Convert.ToInt32(Txt_id_instituicao.Text);
                    updateRow.sigla = Txt_sigla.Text;
                }
                else if (Txt_sigla.Text == "")
                {
                    updateRow.nome = Txt_nome.Text;
                    updateRow.id_instituição = Convert.ToInt32(Txt_id_instituicao.Text);
                }
                else
                {
                    updateRow.id_instituição = Convert.ToInt32(Txt_id_instituicao.Text);
                    updateRow.nome = Txt_nome.Text;
                    updateRow.sigla = Txt_sigla.Text;

                }

                database_Connection.SaveChanges();

                
                dataGrid_escolas.ItemsSource = database_Connection.Escolas.ToList();
                dataGrid_escolas.Visibility = Visibility.Visible;
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
