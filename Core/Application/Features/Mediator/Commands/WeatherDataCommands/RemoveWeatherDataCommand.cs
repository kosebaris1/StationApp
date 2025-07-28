using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.WeatherDataCommands
{
    public class RemoveWeatherDataCommand : IRequest
    {
        public int Id { get; set; }

        public RemoveWeatherDataCommand(int id)
        {
            Id = id;
        }
    }
}
