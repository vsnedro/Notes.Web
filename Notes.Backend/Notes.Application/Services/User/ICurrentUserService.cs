﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Services.User
{
    public interface ICurrentUserService
    {
        public Guid UserId { get; }
    }
}
