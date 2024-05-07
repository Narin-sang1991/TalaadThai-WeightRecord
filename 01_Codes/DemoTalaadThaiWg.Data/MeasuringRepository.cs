using Cet.EntityFramework.Adaptor;
using DemoTalaadThaiWg.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTalaadThaiWg.Data
{
    public class MeasuringRepository : Introductory<Measuring, Guid>, IMeasuringRepository, IHandles<MeasuringMoveItemRemoveEvent>
    {
        public MeasuringRepository(IntsUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        public void Handle(MeasuringMoveItemRemoveEvent args)
        {
            Remove<MeasuringMoveItem>(args.MeasuringMoveItem);
        }
    }
}