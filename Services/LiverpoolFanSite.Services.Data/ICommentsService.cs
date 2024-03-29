﻿namespace LiverpoolFanSite.Services.Data
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task Create(int postId, string userId, string content, int? parentId = null);

        bool IsInSamePost(int commentId, int postId);
    }
}
