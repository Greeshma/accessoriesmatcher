using Microsoft.Azure.Mobile.Server;

namespace AccessoriesMatcherService.DataObjects
{
    public class Dress : EntityData
    {
        public int userid { get; set; }

        public string colour { get; set; }

        public string image { get; set; }
    }
}