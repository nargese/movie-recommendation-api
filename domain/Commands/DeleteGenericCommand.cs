using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Commands
{
    public class DeleteGenericCommand<T> : IRequest<string> where T : class
    {
        public DeleteGenericCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }

    }

}
