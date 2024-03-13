using Geocaching.Data;
using GSF.Collections;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace Geocaching.Pages
{
    public class MapTypesPageCode : ContentPage
    {
        public MapTypesPageCode()
        {

            Map map = new Map
            {
                IsShowingUser = true,
                IsTrafficEnabled = true,
                IsEnabled = true,
                IsScrollEnabled = true,
                IsZoomEnabled = true
            };
              PinList(map);
          //  Content = map;
        }
        public async Task PinList(Map map)
        {

            List<Geocache> geocaches = await DataController.GetInformationAboutNearestGeocache();
            //List<Pin> pins = new List<Pin>();
            //Pin pint = new Pin
            //{
            //    Label = "test",
            //    MarkerId = "123",
            //    Address = "Testowy",

            //    Type = PinType.Place,
            //    Location = new Location(50.047486, 19.927897)

            //};



            //map.Pins.Add(pint);
            foreach (var geocache in geocaches)
            {
                var img = ImageSource.FromUri(new Uri("https://pics.freeicons.io/uploads/icons/png/13433801461623323164-64.png"));

                switch (geocache.type)
                {
                    case "Traditional":
                        img = ImageSource.FromUri(new Uri("https://img.icons8.com/external-wanicon-lineal-wanicon/64/external-chest-fairytale-wanicon-lineal-wanicon.png"));
                        break;
                    case "Quiz":
                        img = ImageSource.FromUri(new Uri("https://pics.freeicons.io/premium/quiz-examination-answer-online-learning-icon-585881-64.png"));
                        break;
                    case "Virtual":
                        img = ImageSource.FromUri(new Uri("https://pics.freeicons.io/uploads/icons/png/12902554971596027133-64.png"));
                        break;
                    case "Multi":
                        img = ImageSource.FromUri(new Uri("https://pics.freeicons.io/uploads/icons/png/8573940621638168896-64.png"));
                        break;
                    case "Moving":
                        img = ImageSource.FromUri(new Uri("https://pics.freeicons.io/uploads/icons/png/7475741651600002657-64.png"));
                        break;
                    case "Webcam":
                        img = ImageSource.FromUri(new Uri("https://pics.freeicons.io/uploads/icons/png/14480973251594941581-64.png"));
                        break;
                    case "Event":
                        img = ImageSource.FromUri(new Uri("https://pics.freeicons.io/uploads/icons/png/18661471221608672866-64.png"));
                        break;
                    default:
                        break;

                }


                var customPin = new CustomPin()
                {

                    Label = geocache.name,
                    Location = new Location(geocache.latitiude, geocache.longitude),
                    Address = geocache.region,
                    ImageSource = img,
                    Map = map
                };

                //pins.Add(pin);
                map.Pins.Add(customPin);
            }
            Content = map;
            //map.Pins.Add(pins);

        }


    }

}
