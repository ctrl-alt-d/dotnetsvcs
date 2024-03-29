﻿using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.DtoResult.PostDtosResult;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.OtherRandomServices;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Projections.Abstractions.PostProjections;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Projections.PostProjections;

public class PostDefaultProjection : IPostDefaultProjection {
    public PostDefaultProjection(IRandomService1 randomService1) {
        RandomService1=randomService1;
    }

    protected virtual IRandomService1 RandomService1 { get; }

    public void Dispose() {
        RandomService1.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<Expression<Func<Post, PosTDtoData>>> GetToDtoData(IDbCtxWrapper dbCtxWrapper) {

        await Task.CompletedTask;
        var totalNumberOfBlogs = dbCtxWrapper.Set<Blog>().Count();
        var NumerTwo = RandomService1.Sum2(0);

        return Post => new PosTDtoData {
            Descripcio = Post.Descripcio,
            EsVisible = Post.EsVisible,
            BlogDisplay = Post.Blog.Titol,
            BlogKey = new object[] { Post.Blog!.Id },
            StatisticsTotalBlogs = totalNumberOfBlogs,
            NumberTwoFromRandomService = NumerTwo,
        };
    }
}
