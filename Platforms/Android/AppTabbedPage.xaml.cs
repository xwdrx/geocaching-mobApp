namespace Geocaching.Pages;

public partial class AppTabbedPage  : TabbedPage
{
	public AppTabbedPage()
	{
        this.Children.Add(new ProfilePage { Title = "profil" , IconImageSource = "profile.svg"});
        this.Children.Add(new ChatPage { Title = "Chat", IconImageSource = "chat.svg" });
        this.Children.Add(new MapTypesPageCode { Title = "mapa", IconImageSource="map.svg" });
        this.Children.Add(new FavoritesPage { Title = "ulubione", IconImageSource="save.svg" });
        

        InitializeComponent();
	}
}