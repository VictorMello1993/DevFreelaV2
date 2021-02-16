using DevFreela.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext
    {
        public DevFreelaDbContext()
        {
            Projects = new List<Project>
            {
                new Project("Meu projeto ASPNET Core", "Minha descrição do projeto", 1, 1, 10000),
                new Project("Cadastro de usuários", "Minha descrição do projeto de cadastro de usuários", 1, 1, 20000),
                new Project("Cadastro de habilidades", "Minha descrição do projeto", 1, 1, 30000)
            };

            Users = new List<User>
            {
                new User("Victor Mello", "emaildovictor@gmail.com", new DateTime(1993, 12, 15)),
                new User("João Pedro", "emaildojp@gmail.com", new DateTime(1989, 5, 12)),
                new User("Humberto Junior", "emaildohumberto@gmail.com", new DateTime(1998, 6, 1))
            };

            Skills = new List<Skill>
            {
                new Skill(".NET Core"),
                new Skill("C#"),
                new Skill("SQL"),
            };
        }
        public List<Project> Projects { get; set; }
        public List<User> Users { get; set; }
        public List<Skill> Skills { get; set; }
        public List<ProjectComment> ProjectComments { get; set; }
    }
}
