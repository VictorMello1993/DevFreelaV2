using DevFreela.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class ProjectConfigurations : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            //Chave primária
            builder.HasKey(p => p.Id);

            //Configuração de chave estrangeira
            //Projeto - Usuário de freelancer (1 - N)
            builder.HasOne(p => p.Freelancer)
                   .WithMany(f => f.FreelanceProjects)
                   .HasForeignKey(p => p.IdFreelancer)
                   .OnDelete(DeleteBehavior.Restrict);

            //Projeto - Usuário cliente (1 - N)
            builder.HasOne(p => p.Client)
                   .WithMany(c => c.OwndedProjects)
                   .HasForeignKey(p => p.IdClient)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
