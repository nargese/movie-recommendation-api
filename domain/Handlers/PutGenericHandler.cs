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
    public class PutGenericHandler<T> : IRequestHandler<PutGenericCommand<T>, string> where T : class
    {
        private readonly IRepository<T> repository;

        public PutGenericHandler(IRepository<T> Repository)
        {
            repository = Repository;
        }
        public Task<string> Handle(PutGenericCommand<T> request, CancellationToken cancellationToken)
        {
            var result = repository.Update(request.Obj);
            return Task.FromResult(result);
        }
    }
}


