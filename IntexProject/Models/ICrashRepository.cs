﻿using System;
using System.Linq;

namespace IntexProject.Models
{
    public interface ICrashRepository
    {
        IQueryable<Crash> mytable { get; }
    }
}
