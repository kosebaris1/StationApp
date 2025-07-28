using Application.Features.Mediator.Results.WeatherDataResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.WeatherDataQueries
{
    public class GetAllWeatherDataQuery : IRequest<List<GetAllWeatherDataQueryResult>>
    {
    }
}
