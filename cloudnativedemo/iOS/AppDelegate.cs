using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace CloudNativeDemo.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            Xamarin.FormsGoogleMaps.Init("AIzaSyD7r2gaub59nVyD-9pY4ugWKm00AKbERLg");

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
