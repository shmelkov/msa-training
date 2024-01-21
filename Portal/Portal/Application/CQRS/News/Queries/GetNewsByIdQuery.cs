using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Portal.Common.CQRS.Queries;
using Portal.Application.CQRS.News.DTOs;
using Portal.Core;

namespace Portal.Application.CQRS.News.Queries
{
    public class GetNewsByIdQuery : BaseGetEntityByIdQuery<NewsDto>
    {
    }

    public class GetNewsByIdQueryHandler : BaseGetEntityByIdQueryHandler<GetNewsByIdQuery, NewsDto, Core.Entities.News>
    {
        private readonly INewsDbContext _dbContext;
        public GetNewsByIdQueryHandler(INewsDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
        }

        public override Task<Core.Entities.News?> GetEntity(BaseGetEntityByIdQuery<NewsDto> request)
        {
            return _dbContext.News.FirstOrDefaultAsync(i => i.Id == request.Id);
        }

        public class GetNewsByIdQueryValidator : AbstractValidator<GetNewsByIdQuery>
        {
            public GetNewsByIdQueryValidator()
            {
                RuleFor(i => i.Id).NotEmpty();
            }
        }
    }
}
