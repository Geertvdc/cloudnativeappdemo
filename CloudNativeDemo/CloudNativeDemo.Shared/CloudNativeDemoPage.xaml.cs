using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using CloudNativeDemo.Shared;
using CloudNativeDemo.Shared.Model;
using System;

namespace CloudNativeDemo
{
    public partial class CloudNativeDemoPage : ContentPage
    {
        MapService mapService;

        public CloudNativeDemoPage()
        {
            InitializeComponent();
            mapService = MapService.Instance;

        }

		async void Handle_MapClicked(object sender, Xamarin.Forms.GoogleMaps.MapClickedEventArgs e)
		{

            PointOfInterest poi = new PointOfInterest();
            poi.Location = new Microsoft.Azure.Documents.Spatial.Point(e.Point.Longitude,e.Point.Latitude);
            poi.Name = e.Point.Latitude.ToString() +"-"+ e.Point.Longitude.ToString();
            poi.Description = DateTime.Now.ToString("R");
                
            await mapService.InsertItemAsync(poi);

			Position p = new Position(e.Point.Latitude, e.Point.Longitude);
            Map.Pins.Add(new Pin() { Position = p, Label = poi.Description });

		}

		async void Handle_Clicked(object sender, System.EventArgs e)
		{

            Microsoft.Azure.Documents.Spatial.Point p = new Microsoft.Azure.Documents.Spatial.Point(Map.CameraPosition.Target.Longitude, Map.CameraPosition.Target.Latitude);
            var pois = await mapService.GetPointsOfInterestsNearby(p);

            foreach (PointOfInterest poi in pois)
            {
                Position pos = new Position(poi.Location.Position.Latitude, poi.Location.Position.Longitude);
                Map.Pins.Add(new Pin() { Position = pos, Label = poi.Description });
            }
		}

        void Handle_PinClicked(object sender, System.EventArgs e)
        {
            
            Pin pin = ((PinClickedEventArgs)e).Pin;
            DescriptionLabel.Text = pin.Label;

        }

    }
}
