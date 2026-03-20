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
    public class GetListGenericHandler<T> : IRequestHandler<GetListGenericQuery<T>, IEnumerable<T>> where T : class
    {
        private readonly IRepository<T> repository;

        public GetListGenericHandler(IRepository<T> Repository)
        {
            repository = Repository;
        }
        //public Task<IEnumerable<T>> Handle(GetListGenericQuery<T> request, CancellationToken cancellationToken)
        //{
        //    var result = repository.GetList(request.Condition, request.Includes);
        //    return Task.FromResult(result);
        //}
        public async Task<IEnumerable<T>> Handle(GetListGenericQuery<T> request, CancellationToken cancellationToken)
        {
            // Utiliser la méthode asynchrone de repository
            return await repository.GetListAsync(request.Condition, request.Includes);
        }
    }
}


