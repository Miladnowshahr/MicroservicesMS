using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exceptions
{
    internal class NotFoundException:ApplicationException
    {
        public string Name { get; }
        public object Key { get; }
        public NotFoundException(string name,object key) : base($"Entity \"{name}\" was not found. " )
        {
            Name = name;
            Key = key;
        }

        
    }
}
