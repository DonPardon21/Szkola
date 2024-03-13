using LosowanieOU.Views;

namespace LosowanieOU
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}