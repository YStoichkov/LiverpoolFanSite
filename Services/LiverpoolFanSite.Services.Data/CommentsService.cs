namespace LiverpoolFanSite.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using LiverpoolFanSite.Data.Common.Repositories;
    using LiverpoolFanSite.Data.Models;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task Create(int postId, string userId, string content, int? parentId = null)
        {
            var comment = new Comment
            {
                Content = content,
                ParentId = parentId,
                PostId = postId,
                UserId = userId,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public bool IsInSamePost(int commentId, int postId)
        {
            var commentPostId = this.commentsRepository.All()
                .Where(x => x.Id == commentId).Select(x => x.PostId).FirstOrDefault();

            return commentPostId == postId;
        }
    }
}
