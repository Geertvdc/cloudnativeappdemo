using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace CloudNativeDemo
{
    public partial class CloudNativeDemoPage : ContentPage
    {
        void Handle_MapClicked(object sender, Xamarin.Forms.GoogleMaps.MapClickedEventArgs e)
        {
            Position p = new Position(e.Point.Latitude, e.Point.Longitude);
            ((Xamarin.Forms.GoogleMaps.Map)sender).Pins.Add(new Pin() { Position = p, Label = "bla" });
        }

        public CloudNativeDemoPage()
        {
            InitializeComponent();
        }
    }
}
