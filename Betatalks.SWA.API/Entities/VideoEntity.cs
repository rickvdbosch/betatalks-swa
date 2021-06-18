using Microsoft.Azure.Cosmos.Table;

namespace Betatalks.SWA.API.Entities
{
    public class VideoEntity : TableEntity
    {
        [IgnoreProperty]
        public string Name
        { 
            get => RowKey;
            set => RowKey = value;
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }
    }
}
