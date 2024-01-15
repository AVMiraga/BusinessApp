using BusinessApp.Core.Entities.Common;

namespace BusinessApp.Core.Entities
{
    public class Blog : BaseAuditableEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
