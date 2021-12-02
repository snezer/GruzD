using System;
using System.Collections.Generic;
using System.Text;

namespace GruzD.DataModel.Recognition
{
    public enum ProcessEventType
    {
        IncomingNumber,
        OutgoingNumber,
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
