using DevFreela.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {        
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Chave primária
            builder.HasKey(u => u.Id);

            //Configuração de chave estrangeira
            //Usuário - Skills (1 - N)
            builder.HasMany(us => us.Skills)
                   .WithOne()
                   .HasForeignKey(us => us.IdSkill)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
