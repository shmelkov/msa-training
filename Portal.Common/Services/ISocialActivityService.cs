using Portal.Common.DTOs;
using Portal.Common.Requests;
using Refit;

namespace Portal.Common.Services
{
    [Headers("Authorization: Bearer")]
    public interface ISocialActivityService
    {
        [Get("/comments")]
        Task<BasePagedDto<Comment>> GetComments<T>(T query) where T : IPagingRequest, ISortingRequest, IFilteringRequest;

        [Post("/comments")]
        Task<Guid> AddComment([Body] CreateCommentCommand request);

        [Put("/comments/{id}")]
        Task UpdateComment([Body] UpdateCommentCommand request, Guid id);

        [Delete("/comments/{id}")]
        Task DeleteComment(Guid id);
    }

    public class Comment
    {
        public Guid Id { get; set; }
        public Guid? ModifiedById { get; set; }
        public DateTime Modified { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime Created { get; set; }
        public Guid ObjectId { get; set; }
        public string Content { get; set; }
        public Guid? ParentId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsModified { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorPhotoUrl { get; set; }
        public int Likes { get; set; }
        public bool IsLikedByCurrentUser { get; set; }
        public int ChildrenCount { get; set; }
    }

    public class CreateCommentCommand
    {
        public Guid? Id { get; set; }
        public Guid ObjectId { get; set; }
        public string Module { get; set; }
        public string Content { get; set; }
        public Guid? ParentId { get; set; }
    }

    public class UpdateCommentCommand
    {
        public string Content { get; set; }
    }
}
