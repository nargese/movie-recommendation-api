using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Commands
{
    public class DeleteObjectCommand<T> : IRequest<string> where T : class
    {
        public DeleteObjectCommand(T entity)
        {
            Entity = entity;
        }

        public T Entity { get; }

    }
}
