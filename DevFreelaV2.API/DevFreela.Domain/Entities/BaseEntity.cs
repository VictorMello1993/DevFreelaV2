using System;

namespace DevFreela.Domain.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity() { }        
        public int Id { get; private set; }
    }
}
