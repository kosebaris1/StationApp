using Application.Features.Mediator.Results.WeatherDataResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.WeatherDataQueries
{
    public class GetWeatherDataByWeatherStationIdQuery : IRequest<List<GetWeatherDataByWeatherStationIdQueryResult>>
    {
        public int Id { get; set; }

        public GetWeatherDataByWeatherStationIdQuery(int id)
        {
            Id = id;
        }
    }
}
