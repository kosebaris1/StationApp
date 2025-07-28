using Application.Features.Mediator.Queries.WeatherDataQueries;
using Application.Features.Mediator.Results.WeatherDataResults;
using Application.Interfaces;
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
    public class GetAllWeatherDataQueryHandler : IRequestHandler<GetAllWeatherDataQuery, List<GetAllWeatherDataQueryResult>>
    {
        private readonly IRepository<WeatherData> _repository;
        private readonly IMapper _mapper;
        public GetAllWeatherDataQueryHandler(IRepository<WeatherData> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllWeatherDataQueryResult>> Handle(GetAllWeatherDataQuery request, CancellationToken cancellationToken)
        { 
            var values =await _repository.GetAllAsync();
            return _mapper.Map<List<GetAllWeatherDataQueryResult>>(values);
        }
    }
}
