using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Commands
{
    public class PutGenericCommand<T> : IRequest<string> where T : class

    {
        public PutGenericCommand(T obj)
        {
            Obj = obj;
        }

        public T Obj { get; set; }
    }

}
