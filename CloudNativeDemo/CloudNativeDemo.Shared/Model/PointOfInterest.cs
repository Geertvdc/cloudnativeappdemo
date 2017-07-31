using System;
using Microsoft.Azure.Documents.Spatial;
using Newtonsoft.Json;

namespace CloudNativeDemo.Shared.Model                         
{
    public class PointOfInterest
    {
        public PointOfInterest()
        {
        }

		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

        public string Name { get; set; }
        public Point Location { get; set; }
        public string Description { get; set; }
    }
}
