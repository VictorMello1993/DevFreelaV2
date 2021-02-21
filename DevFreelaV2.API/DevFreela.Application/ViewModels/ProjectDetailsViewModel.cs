using System;

namespace DevFreela.Application.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public ProjectDetailsViewModel(int id, string title, string description, DateTime? startedAt, DateTime? finishedAt, string clientName, string freelancerName)
        {
            Id = id;
            Title = title;
            Description = description;
            StartedAt = startedAt;
            FinishedAt = finishedAt;
            ClientName = clientName;
            FreelancerName = freelancerName;
        }

        public ProjectDetailsViewModel()
        {

        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public string ClientName { get; set; }
        public string FreelancerName { get; set; }
    }
}