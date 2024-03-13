using Geocaching.Pages;

namespace Geocaching;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        //MainPage = new AppShell();
        MainPage = new AppTabbedPage();
    }
}
