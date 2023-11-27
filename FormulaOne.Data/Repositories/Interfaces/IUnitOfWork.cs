﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.Data.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IDriverRepository Drivers { get; }
        IAchievementRepository Achievements { get; }

        Task<bool> CompleteAsync();
    }
}
