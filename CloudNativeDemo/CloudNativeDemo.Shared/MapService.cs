using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudNativeDemo.Shared.Model;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Documents.Spatial;

namespace CloudNativeDemo.Shared
{
    public class MapService
    {
		public const string databaseId = @"cloudnativemap";
		public const string collectionId = @"Pins";

		static MapService defaultInstance = new MapService();

		private Uri collectionLink = UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);

		private DocumentClient client;

        public MapService()
        {
            client = new DocumentClient(new System.Uri(Keys.accountURL), Keys.accountKey);
		}

		public static MapService Instance
		{
			get
			{
				return defaultInstance;
			}
			private set
			{
				defaultInstance = value;
			}
		}

		public async Task<List<PointOfInterest>> GetPointsOfInterestsNearby(Point p)
		{
            var items = new List<PointOfInterest>();

			var query = client.CreateDocumentQuery<PointOfInterest>(collectionLink,new FeedOptions{MaxItemCount = -1})
		                      .Where(poi => poi.Location.Distance(p) < 50000)
						      .AsDocumentQuery();

            while (query.HasMoreResults)
			{
                FeedResponse<PointOfInterest> res = await query.ExecuteNextAsync<PointOfInterest>();
				if (res.Any())
				{
                    items.AddRange(res.ToList());
					break;
				}
			}

            return items;
	
		}

        public async Task<PointOfInterest> InsertItemAsync(PointOfInterest p)
		{
			var result = await client.CreateDocumentAsync(collectionLink, p);
			p.Id = result.Resource.Id;

            return p;
		}


    }
}
