using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.GetById
{
    public class GetUserByIdHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User?> ExecuteAsync(GetUserByIdQuery query, CancellationToken cancellationToken = default)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(query.Id, cancellationToken);
            if (user == null)
            {
                throw new Exception($"User with ID {query.Id} not found");
            }

            return user;
        }
    }
}
