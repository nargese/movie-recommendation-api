using MediatR;
using domain.Commands;
using domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Handlers
{
    public class DeleteGenericHandler<T> : IRequestHandler<DeleteGenericCommand<T>, string> where T : class
    {

        private readonly IRepository<T> repository;
        public DeleteGenericHandler(IRepository<T> Repository)
        {
            repository = Repository;
        }

        public Task<string> Handle(DeleteGenericCommand<T> request, CancellationToken cancellationToken)
        {
            var result = repository.Remove(request.Id);
            return Task.FromResult(result);
        }

    }
}