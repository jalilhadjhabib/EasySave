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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Threading;
using System.Resources;
using System.Globalization;

namespace EasySave_V2._0
{
   
    /// <summary>
    /// Logique d'interaction pour welcome.xaml
    /// </summary>
    public partial class welcome : Page
    {
        
        public welcome()
        {
            InitializeComponent();
        }

        private void btn_fra_Click(object sender, RoutedEventArgs e)
        {
           /* Color myRgbColor = new Color();
            myRgbColor = Color.FromRgb(103, 58, 183);
            SolidColorBrush brush = new SolidColorBrush(myRgbColor);
            

           fra_btn.Background = Brushes.Green;
            eng_btn.Background = brush;*/
        }
        private void btn_eng_Click(object sender, RoutedEventArgs e)
        {
           /* Color myRgbColor = new Color();
            myRgbColor = Color.FromRgb(103, 58, 183);
            SolidColorBrush brush = new SolidColorBrush(myRgbColor);
            eng_btn.Background = Brushes.Green;
            fra_btn.Background = brush;*/

        }
        
        /*private void changer_langue_FRA()
        {

            addnewtask p = new addnewtask();
            p.label1.Content = "Ajouter une nouvelle tâche de sauvegarde";
            p.label2.Content = "Entrer un nom pour la sauvegarde";
            p.label3.Content = "Choisisez un type de sauvegarde";
            p.label4.Content = "Selectionnez un répertoire source";
            p.label5.Content = "Selectionnez un répertoire cible";
            

        }*/

    }
}
