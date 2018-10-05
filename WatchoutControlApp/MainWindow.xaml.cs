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

namespace WatchoutControlApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] timelines = new string[] {"puzzle1", "puzzle2", "puzzle3", "puzzle4", "puzzle5", "puzzle6", "puzzle7", "puzzle8", "puzzle9", "puzzle10", "puzzle11", "puzzle12", };
        
        public MainWindow()
        {
            InitializeComponent();
            timelineChooseComboBox.ItemsSource = timelines;
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.timeline))
                timelineChooseComboBox.SelectedItem = Properties.Settings.Default.timeline;
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.ipaddress))
                IPTextBox.Text = Properties.Settings.Default.ipaddress;
        }

        private void timelineChooseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.timeline = (sender as ComboBox).SelectedItem.ToString();
            Properties.Settings.Default.Save();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (timelineChooseComboBox.SelectedItem != null && !string.IsNullOrEmpty(IPTextBox.Text))
            {
                ButtonWindow window = new ButtonWindow();
                window.ShowDialog();
            }
            else
                MessageBox.Show("choose timeline and enter IP address");
        }

        private void IPTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default.ipaddress = IPTextBox.Text;
        }

        private void IPTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
