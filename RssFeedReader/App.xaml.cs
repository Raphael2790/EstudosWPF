using RssFeedReader.Helpers;
using RssFeedReader.Helpers.Interfaces;
using System.Windows;

namespace RssFeedReader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DependencyInjector.RegisterTypes<IRssFeedHelper, FakeRssFeedHelper>();
            MainWindow = DependencyInjector.Resolve<MainWindow>();
            MainWindow.Show();
        }
    }
}
