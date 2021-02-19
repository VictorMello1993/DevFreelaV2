using DevFreela.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class ProjectCommentConfigurations : IEntityTypeConfiguration<ProjectComment>
    {
        public void Configure(EntityTypeBuilder<ProjectComment> builder)
        {
            //Chave primária
            builder.HasKey(pc => pc.Id);

            //Configuração de chave estrangeira
            //Projeto - Comments (1 - N)
            builder.HasOne(pc => pc.Project)
                   .WithMany(p => p.Comments)
                   .HasForeignKey(p => p.IdProject)
                   .OnDelete(DeleteBehavior.Restrict);

            //Usuário - Comments (1 - N)
            builder.HasOne(pc => pc.User)
                   .WithMany(u => u.Comments)
                   .HasForeignKey(u => u.IdUser)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
