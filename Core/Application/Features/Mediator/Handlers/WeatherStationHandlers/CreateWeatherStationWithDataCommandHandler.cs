using Application.Features.Mediator.Commands.WeatherStationCommands;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.WeatherStationHandlers
{
    public class CreateWeatherStationWithDataCommandHandler : IRequestHandler<CreateWeatherStationWithDataCommand>
    {
        private readonly IRepository<WeatherStation> _stationRepository;
        private readonly IRepository<WeatherData> _weatherDataRepository;
        private readonly IMapper _mapper;

        public CreateWeatherStationWithDataCommandHandler(IRepository<WeatherStation> stationRepository, IRepository<WeatherData> weatherDataRepository, IMapper mapper)
        {
            _stationRepository = stationRepository;
            _weatherDataRepository = weatherDataRepository;
            _mapper = mapper;
        }

        public async Task Handle(CreateWeatherStationWithDataCommand request, CancellationToken cancellationToken)
        {
            var station = _mapper.Map<WeatherStation>(request);
            await _stationRepository.CreateAsync(station);

            // WeatherData map ve kayıt
            var data = _mapper.Map<WeatherData>(request);
            data.WeatherStationId = station.Id;

            await _weatherDataRepository.CreateAsync(data);
        }
    }
    
    
}
