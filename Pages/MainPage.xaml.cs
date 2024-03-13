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
        string filepath = @"geocaching-b6813-firebase-adminsdk-vau5d-54d05f7ffb.json";
        var localPath = Path.Combine(FileSystem.CacheDirectory, "credentials.json");

        using var json = await FileSystem.OpenAppPackageFileAsync("credentials.json");
        using var dest = File.Create(localPath);
        await json.CopyToAsync(dest);

        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", localPath);
        dest.Close();
        FirestoreDb db = FirestoreDb.Create("geocaching-b6813");

       // List<AttributeNotification> attribute = await DataController.GetInformationAboutGeocacheAttribute();
        List<Geocache> geocache = await DataController.GetInformationAboutGeocache();
       // List<Geocache> nearest =await DataController.GetInformationAboutNearestGeocache();

        try
        {

            foreach (var item in geocache)
            {
                DocumentReference docRef = db.Collection("Geocache").Document($"{item.code}");      //teraz ogarnac jak to dodac
                Dictionary<string,object> geocaches = new Dictionary<string, object>() ;
                 geocaches.Add($"{item.code}", item);
                //var attr_acodes = item.attr_acodes;
                //var country2 = item.country2;
                //var date_created = item.date_created;
                //var SHORT = item.short_descriptions;
                //geocaches.Add("attr_acodes", attr_acodes);
                //geocaches.Add("country2", country2);
                //geocaches.Add("SHORT", SHORT);
                //geocaches.Add("date_created", date_created);
              //  geocaches[item.name] = item;
                //Console.WriteLine(geocaches.ToString());
                await docRef.SetAsync(geocaches[item.name], SetOptions.MergeAll);

            }

            //foreach (var item in nearest)
            //{
            //    DocumentReference docRef = db.Collection("Geocache").Document($"{item.code}");

            //    Dictionary<string, object> nearests = new Dictionary<string, object>();
            //    nearests.Add($"{item.code}", item);
            //    await docRef.SetAsync(nearests, SetOptions.MergeAll);
            //}

            //foreach (var item in attribute)
            //{
            //    DocumentReference docRef = db.Collection("Attribute").Document($"{item.acode}");

            //    Dictionary<string, object> attributes = new Dictionary<string, object>();
            //    attributes.Add($"{item.acode}", item);
            //    await docRef.SetAsync(attributes, SetOptions.MergeAll);
            //}
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
    private void OnCounterClicked(object sender, EventArgs e)
	{
		//count++;

		//if (count == 1)
		//	CounterBtn.Text = $"Clicked {count} time";
		//else
		//	CounterBtn.Text = $"Clicked {count} times";

		//SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

