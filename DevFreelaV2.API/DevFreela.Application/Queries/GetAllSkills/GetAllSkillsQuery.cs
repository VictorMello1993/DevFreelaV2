using DevFreela.Application.ViewModels;
using DevFreela.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Queries.GetAllSkills
{
    public class GetAllSkillsQuery : IRequest<List<SkillDTO>>
    {
        public GetAllSkillsQuery(string query)
        {
            Query = query;
        }

        public GetAllSkillsQuery()
        {

        }

        public string Query { get; private set; }
    }
}
