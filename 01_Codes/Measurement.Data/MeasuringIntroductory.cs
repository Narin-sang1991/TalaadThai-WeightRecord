using Cet.EntityFramework.Adaptor;
using Cet.Hw.Core.Data;
using Measurement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement.Data
{
    public class MeasuringIntroductory : Introductory<Measuring, Guid>, IMeasuringIntroductory, IHandles<MeasuringMoveItemRemoveEvent>
    {
        public MeasuringIntroductory(HwUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        public void Handle(MeasuringMoveItemRemoveEvent args)
        {
            Remove<MeasuringMoveItem>(args.MeasuringMoveItem);
        }


    }
}
