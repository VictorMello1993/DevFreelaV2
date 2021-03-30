using DevFreela.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Queries.GetProjectById
{
    public class GetProjectByIdQuery : IRequest<ProjectDetailsViewModel>
    {
        public GetProjectByIdQuery(int id)
        {
            Id = id;
        }

        public GetProjectByIdQuery(string query)
        {
            Query = query;
        }

        public int Id { get; private set; }
        public string Query { get; private set; }
    }
}
