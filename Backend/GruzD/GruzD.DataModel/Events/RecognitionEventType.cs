using System;
using System.Collections.Generic;
using System.Text;

namespace GruzD.DataModel.Events
{
    public enum ProcessEventType
    {
        IncomingArrived,
        OutgoingArrived,
        IncomingDispatched,
        OutgoingDispatched,
        IncomingDefect,
        OutgoingDefect,
        TransitiveDefect,
        IncomingGet,
        IncomingPut,
        TransitiveGet,
        TransitivePut,
        OutgoingGet,
        OutgoingPut
    }
}
