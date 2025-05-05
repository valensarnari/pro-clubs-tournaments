using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> IsEmailInUseAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> IsUsernameInUseAsync(string username, CancellationToken cancellationToken = default);
    }
}
