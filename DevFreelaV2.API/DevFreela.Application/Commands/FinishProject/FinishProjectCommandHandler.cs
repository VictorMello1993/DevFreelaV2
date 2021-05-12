using DevFreela.Domain.DTOs;
using DevFreela.Domain.Repositories;
using DevFreela.Domain.Services;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.FinishProject
{
    public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, bool>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly IProjectRepository _projectRepository;
        private readonly IPaymentService _paymentService;
        private readonly string _connectionString;

        public FinishProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public FinishProjectCommandHandler(IProjectRepository projectRepository, IPaymentService paymentService)
        {
            _projectRepository = projectRepository;
            _paymentService = paymentService;
        }

        public FinishProjectCommandHandler(DevFreelaDbContext dbContext, IProjectRepository projectRepository, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _projectRepository = projectRepository;
            _connectionString = configuration.GetConnectionString("DevFreelaV2SQLServer");
        }

        public async Task<bool> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
        {
            //Padrão CQRS
            //var project = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);

            //project.Finish();

            //Entity Framework - Padrão CQRS
            //await _dbContext.SaveChangesAsync();

            //Dapper
            //using (var sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();

            //    var sql = "UPDATE Projects SET Status = @status, FinishedAt = @finishedat WHERE Id = @id";

            //    sqlConnection.Execute(sql, new { status = project.Status, finishedat = project.FinishedAt, Id = project.Id });
            //}

            //Padrão Repository
            var project = await _projectRepository.GetByIdAsync(request.Id);

            //Sem mensageria - Forma tradicional
            //project.Finish();
            
            //Com mensageria (RabbitMQ)
            var paymentInfoDto = new PaymentInfoDTO(request.Id, request.CreditCardNumber, request.Cvv, request.ExpiresAt, request.FullName, project.TotalCost);

            _paymentService.ProcessPayment(paymentInfoDto);

            project.SetPaymentPending();

            await _projectRepository.SaveChangesAsync();

            return true;
        }
    }
}
