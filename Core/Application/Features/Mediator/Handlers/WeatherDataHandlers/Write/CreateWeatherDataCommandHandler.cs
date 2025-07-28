using Application.Features.Mediator.Commands.WeatherDataCommands;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.WeatherDataHandlers.Write
{
    public class CreateWeatherDataCommandHandler : IRequestHandler<CreateWeatherDataCommand>
    {
        private readonly IRepository<WeatherData> _repository;
        private readonly IMapper _mapper;
        public CreateWeatherDataCommandHandler(IRepository<WeatherData> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CreateWeatherDataCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _mapper.Map<WeatherData>(request);
                await _repository.CreateAsync(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateWeatherDataCommand Handler: {ex.Message}");
                throw;
            }
        }
    }
}
