using DevFreela.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Domain.Entities
{
    public class User : BaseEntity
    {
        public User(string name, string email, DateTime birthDate, string password, string role)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Active = true;
            CreatedAt = DateTime.Now;

            Password = password;
            Role = role;

            Skills = new List<UserSkill>();
            OwndedProjects = new List<Project>();
            FreelanceProjects = new List<Project>();
        }

        public User()
        {

        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Active { get; set; }
        public List<UserSkill> Skills { get; private set; }
        public List<Project> OwndedProjects { get; private set; }
        public List<Project> FreelanceProjects { get; private set; }
        public List<ProjectComment> Comments { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }

        public void Update(string email)
        {
            if (!Active)
            {
                throw new UserIsInactiveException();
            }

            Email = email;
        }

        public void Delete()
        {
            if (Active)
            {
                Active = false;
            }
        }

        public void UpdatePassword(string newPassword)
        {
            if (!Active)
            {
                throw new UserIsInactiveException();
            }

            Password = newPassword;
        }
    }
}
