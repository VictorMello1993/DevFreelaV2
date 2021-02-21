using DevFreela.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Queries.GetSkillById
{
    public class GetSkillByIdQuery : IRequest<SkillViewModel>
    {
        public GetSkillByIdQuery(int id)
        {
            this.id = id;
        }

        public int id { get; private set; }
    }
}
