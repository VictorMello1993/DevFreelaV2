namespace DevFreela.Application.InputModels
{
    public class UpdateProjectInputModel
    {
        public UpdateProjectInputModel(string title, string description, decimal totalCost)
        {            
            Title = title;
            Description = description;
            TotalCost = totalCost;
        }
        
        public string Title { get; set; }
        public string Description { get; set; }        
        public decimal TotalCost { get; set; }
    }
}