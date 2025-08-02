using Application.Features.Mediator.Queries.WeatherDataQueries;
using Application.Features.Mediator.Results.WeatherDataResults;
using Application.Interfaces;
using Application.Interfaces.WeatherDataInterface;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.WeatherDataHandlers.Read
{
    public class GetWeatherDataByWeatherStationIdQueryHandler : IRequestHandler<GetWeatherDataByWeatherStationIdQuery, List<GetWeatherDataByWeatherStationIdQueryResult>>
    {
        private readonly IWeatherDataRepository _repository;
        private readonly IMapper _mapper;
        public GetWeatherDataByWeatherStationIdQueryHandler(IWeatherDataRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetWeatherDataByWeatherStationIdQueryResult>> Handle(GetWeatherDataByWeatherStationIdQuery request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetAllWeatherDataByStationId(request.Id);
            return _mapper.Map<List<GetWeatherDataByWeatherStationIdQueryResult>>(value);
        }
    }
}
