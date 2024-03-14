using Geocaching.Data;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using GSF.IO;
using System.Threading.Channels;

namespace Geocaching.Pages;

public partial class MainPage : ContentPage
{    
	int count = 0;
 
    public MainPage()
	{
		InitializeComponent();
      //  FireStoreSeed();
        //  CollectionReference collection = db.Collection("Geocache");


    }

    public async Task FireStoreSeed()
    {
        string filepath = @".json";
        var localPath = Path.Combine(FileSystem.CacheDirectory, "credentials.json");

        using var json = await FileSystem.OpenAppPackageFileAsync("credentials.json");
        using var dest = File.Create(localPath);
        await json.CopyToAsync(dest);

        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", localPath);
        dest.Close();
        FirestoreDb db = FirestoreDb.Create("geocaching-b6813");

        List<Geocache> geocache = await DataController.GetInformationAboutGeocache();
        try
        {

            foreach (var item in geocache)
            {
                DocumentReference docRef = db.Collection("Geocache").Document($"{item.code}");      //teraz ogarnac jak to dodac
                Dictionary<string,object> geocaches = new Dictionary<string, object>() ;
                 geocaches.Add($"{item.code}", item);
        
                await docRef.SetAsync(geocaches[item.name], SetOptions.MergeAll);

            }

        }
        catch (Exception e)
        {
            Console.WriteLine("\n\n\n\n\n\nError \n" + e.Message + "\n" + e.InnerException + "\n\n\n\n\n");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
      //  firebaseClient.Child("Attribute").PostAsync(attribute);
    }
   
}

