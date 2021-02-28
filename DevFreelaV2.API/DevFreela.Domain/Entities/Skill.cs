using System;

namespace DevFreela.Domain.Entities
{
    public class Skill : BaseEntity
    {
        public Skill(string description)
        {
            Description = description;
            CreatedAt = DateTime.Now;
        }

        public Skill()
        {

        }

        public string Description { get; private set; }
        public DateTime CreatedAt { get; set; }
    }
}
