using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTalaadThaiWg.AppServiceContract
{
    public enum MeasuringStatus
    {
        Draft = 0,
        Commit = 1,
        Cancelled = 2
    }

    public enum MeasuringMoveType
    {
        Into = 101,
        Transform = 102,
        Out = 103,
        Records = 104,
    }

    public enum GatewayItemType
    {
        Machine = 201,
        Human = 202
    }


}
