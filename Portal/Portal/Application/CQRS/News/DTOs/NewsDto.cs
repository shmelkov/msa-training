namespace Portal.Application.CQRS.News.DTOs
{
    public class NewsDto
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public int ViewsCount { get; set; }
        public int LikesCount { get; set; }
    }
}
