using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthApp.Core.Entities
{
    
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }

    public abstract class BaseEntity<T> : IEntity<T>
    {
        public T Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}
