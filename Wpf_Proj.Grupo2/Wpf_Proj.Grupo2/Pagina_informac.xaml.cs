﻿using System;
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
    /// Lógica interna para Pagina_informac.xaml
    /// </summary>
    public partial class Pagina_informac : Window
    {
        public Pagina_informac()
        {
            InitializeComponent();
        }

        private void B_Voltar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow ss = new MainWindow();
            ss.Show();
        }
    }
}