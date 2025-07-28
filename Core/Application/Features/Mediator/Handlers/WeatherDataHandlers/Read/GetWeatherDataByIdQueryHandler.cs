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
    public class GetWeatherDataByIdQueryHandler : IRequestHandler<GetWeatherDataByIdQuery, GetWeatherDataByIdQueryResult>
    {
        private readonly IRepository<WeatherData> _repository;
        private readonly IMapper _mapper;
        public GetWeatherDataByIdQueryHandler(IRepository<WeatherData> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetWeatherDataByIdQueryResult> Handle(GetWeatherDataByIdQuery request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<GetWeatherDataByIdQueryResult>(value);
        }
    }
}
