using Application.Features.Mediator.Results.AdminResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.AdminQueries
{
    public class GetAllUserByIsApprovedQuery : IRequest<List<GetAllUserByIsApprovedQueryResult>>
    {
    }
}
