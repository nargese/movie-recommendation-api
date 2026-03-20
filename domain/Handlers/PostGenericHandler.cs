using domain.Interface;
using domain.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Handlers
{
    public class PostGenericHandler<T> : IRequestHandler<PostGenericCommand<T>, string> where T : class
    {

        private readonly IRepository<T> repository;
        public PostGenericHandler(IRepository<T> Repository)
        {
            repository = Repository;
        }
        public Task<string> Handle(PostGenericCommand<T> request, CancellationToken cancellationToken)
        {
            var result = repository.Add(request.Obj);
            return Task.FromResult(result);
        }
    }
}


