using Application.Features.Mediator.Queries.AdminQueries;
using Application.Features.Mediator.Results.AdminResults;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.AdminHandlers.Read
{
    public class GetAllUserByIsApprovedQueryHandler : IRequestHandler<GetAllUserByIsApprovedQuery, List<GetAllUserByIsApprovedQueryResult>>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public GetAllUserByIsApprovedQueryHandler(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllUserByIsApprovedQueryResult>> Handle(GetAllUserByIsApprovedQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllUserByIsApproved();
            return _mapper.Map<List<GetAllUserByIsApprovedQueryResult>>(result);
        }
    }
}
