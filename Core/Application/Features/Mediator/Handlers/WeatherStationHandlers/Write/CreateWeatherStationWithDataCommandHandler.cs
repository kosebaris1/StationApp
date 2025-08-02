using Application.Features.Mediator.Commands.WeatherStationCommands;
using Application.Interfaces;
using Application.Interfaces.SignalRInterface;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.WeatherStationHandlers.Write
{
    public class CreateWeatherStationWithDataCommandHandler : IRequestHandler<CreateWeatherStationWithDataCommand>
    {
        private readonly IRepository<WeatherStation> _stationRepository;
        private readonly IRepository<WeatherData> _weatherDataRepository;
        private readonly IWeatherDataBroadcaster _weatherDataBroadcaster;
        private readonly IMapper _mapper;

        public CreateWeatherStationWithDataCommandHandler(IRepository<WeatherStation> stationRepository, IRepository<WeatherData> weatherDataRepository, IMapper mapper, IWeatherDataBroadcaster weatherDataBroadcaster)
        {
            _stationRepository = stationRepository;
            _weatherDataRepository = weatherDataRepository;
            _mapper = mapper;
            _weatherDataBroadcaster = weatherDataBroadcaster;
        }

        public async Task Handle(CreateWeatherStationWithDataCommand request, CancellationToken cancellationToken)
        {
            var station = _mapper.Map<WeatherStation>(request);
            await _stationRepository.CreateAsync(station);
            
            // WeatherData map ve kayıt
            var data = _mapper.Map<WeatherData>(request);
            data.WeatherStationId = station.Id;

            await _weatherDataRepository.CreateAsync(data);
            await _weatherDataBroadcaster.BroadcastWeatherDataAsync(data);

        }
    }


}
