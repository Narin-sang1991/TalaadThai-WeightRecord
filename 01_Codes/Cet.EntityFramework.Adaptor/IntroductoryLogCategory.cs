using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.EntityFramework.Adaptor
{
    public class IntroductoryLogCategory
    {
        public static string General = "InceptionLogCategory";
    }

    public enum IntroductoryGeneralLogCategoryEvent
    {
        CannotAddNullEntity = 0,
        CannotModifyNullEntity,
        CannotRemoveNullEntity,
        CannotTrackNullEntity
    }
}
