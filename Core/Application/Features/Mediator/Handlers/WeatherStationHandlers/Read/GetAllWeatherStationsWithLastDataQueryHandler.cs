using Application.Features.Mediator.Queries.WeatherStationQueries;
using Application.Features.Mediator.Results.WeatherStationResults;
using Application.Interfaces.WeatherStationInterface;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.WeatherStationHandlers.Read
{
    public class GetAllWeatherStationsWithLastDataQueryHandler : IRequestHandler<GetAllWeatherStationsWithLastDataQuery, List<GetAllWeatherStationsWithLastDataQueryResult>>
    {
        private readonly IWeatherStationRepository _repository;
        private readonly IMapper _mapper;
        public GetAllWeatherStationsWithLastDataQueryHandler(IWeatherStationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllWeatherStationsWithLastDataQueryResult>> Handle(GetAllWeatherStationsWithLastDataQuery request, CancellationToken cancellationToken)
        {
            var stationWithLastDataList = await _repository.GetStationsWithLastDataAsync();

            var result = stationWithLastDataList.Select(pair =>
            {
                var dto = _mapper.Map<GetAllWeatherStationsWithLastDataQueryResult>(pair.Station);
                dto.LastWeatherData = pair.LastData != null ? _mapper.Map<WeatherDataResult>(pair.LastData) : null;
                return dto;
            }).ToList();

            return result;
        }
    }
}
