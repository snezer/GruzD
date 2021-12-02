using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GruzD.DataModel.Events;

namespace GruzD.DAL
{
    public interface IProcessEventRegistrationRepository
    {
        Task<ProcessEvent> CreateEventAsync(ProcessEvent prEvent);

    }
}
