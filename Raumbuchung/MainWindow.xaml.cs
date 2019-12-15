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
using Xceed.Wpf.Toolkit;

namespace Raumbuchung
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Raumverwaltung raumVerwaltung;

        public MainWindow()
        {
            InitializeComponent();
            pickerStart.Value = DateTime.Now;
            pickerEnd.Value = DateTime.Now;
            raumVerwaltung = new Raumverwaltung();
        }

        private void searchRoomButton(object sender, RoutedEventArgs e)
        {
            roomResults.Content = raumVerwaltung.searchRoom((DateTime)pickerStart.Value, (DateTime)pickerEnd.Value);
        }
    }
}
