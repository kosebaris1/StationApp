﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.JwtService
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
