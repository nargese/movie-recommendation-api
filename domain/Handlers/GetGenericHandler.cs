using domain.Interface;
using domain.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Handlers
{
    public class GetGenericHandler<T> : IRequestHandler<GetGenericQuery<T>, T> where T : class
    {

        private readonly IRepository<T> repository;

        public GetGenericHandler(IRepository<T> Repository)
        {
            repository = Repository;
        }
        public Task<T> Handle(GetGenericQuery<T> request, CancellationToken cancellationToken)
        {
            var result = repository.Get(request.Condition, request.Includes);
            return Task.FromResult(result);
        }
    }
}
