using WebApp.Model.Entities.Base;
using WebApp.Model.Extensions;

namespace WebApp.Model.Entities
{
    [BsonCollection("Samples")]
    public class Sample :  Entity
    {
        public string Title { get; set; }
    }
}
