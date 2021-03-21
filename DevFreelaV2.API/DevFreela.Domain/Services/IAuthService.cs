using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Domain.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(string email, string role);
        string ComputeSha256Hash(string password);
    }
}
