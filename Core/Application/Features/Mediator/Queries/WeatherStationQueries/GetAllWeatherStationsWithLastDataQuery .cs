using Application.Features.Mediator.Results.WeatherStationResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.WeatherStationQueries
{
    public class GetAllWeatherStationsWithLastDataQuery : IRequest<List<GetAllWeatherStationsWithLastDataQueryResult>>
    {

    }
}
