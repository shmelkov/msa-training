using System.Net.Mail;
using Portal.Common.Entities.Base;

namespace Portal.Core.Entities
{
    public class News : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
    
        public int ViewsCount { get; set; }
        public int LikesCount { get; set; }

    }
}
