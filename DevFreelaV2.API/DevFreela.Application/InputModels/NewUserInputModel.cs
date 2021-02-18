using System;

namespace DevFreela.Application.InputModels
{
    public class NewUserInputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}