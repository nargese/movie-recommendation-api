using domain.Commands;
using domain.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Handlers
{
    public class DeleteObjectHandler<T> : IRequestHandler<DeleteObjectCommand<T>, string> where T : class
    {

        private readonly IRepository<T> repository;
        public DeleteObjectHandler(IRepository<T> Repository)
        {
            repository = Repository;
        }

        public Task<string> Handle(DeleteObjectCommand<T> request, CancellationToken cancellationToken)
        {
            var result = repository.Removeobject(request.Entity);
            return Task.FromResult(result);
        }
    }
}
