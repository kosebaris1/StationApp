using Application.Features.Mediator.Commands.WeatherDataCommands;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.WeatherDataHandlers.Write
{
    public class UpdateWeatherDataCommandHandler : IRequestHandler<UpdateWeatherDataCommand>
    {
        private readonly IRepository<WeatherData> _repository;
        private readonly IMapper _mapper;
        public UpdateWeatherDataCommandHandler(IRepository<WeatherData> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateWeatherDataCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.GetByIdAsync(request.Id);
                _mapper.Map(request, result);
                await _repository.UpdateAsync(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateWeatherDataCommand Handler: {ex.Message}");
                throw;
            }
        }
    }
}
