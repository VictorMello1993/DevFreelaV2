using DevFreela.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<UserViewModel>>
    {
        public GetAllUsersQuery(string query)
        {
            Query = query;
        }

        public GetAllUsersQuery()
        {

        }

        public string Query { get; private set; }
    }
}
