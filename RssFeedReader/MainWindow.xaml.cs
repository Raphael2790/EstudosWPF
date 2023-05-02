using RssFeedReader.ViewModel;
using System.Windows;
using Unity;

namespace RssFeedReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Dependency]
        public MainViewModel MainViewModel 
        { 
            set 
            { 
                DataContext = value; 
            } 
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
