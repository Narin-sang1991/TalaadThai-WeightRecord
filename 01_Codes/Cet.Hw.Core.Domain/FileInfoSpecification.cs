using Cet.EntityFramework.Adaptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Domain
{
    public static class FileInfoSpecification
    {
        public static ISpecification<CoreFileInfo> GetSpecification01(FileInfoCriteria criteria)
        {
            Specification<CoreFileInfo> spec = new TrueSpecification<CoreFileInfo>();

            if (criteria.Id.HasValue)
                spec &= (t => t.Id == criteria.Id.Value);

            if (criteria.RelationId.HasValue)
                spec &= (t => t.RelationId == criteria.RelationId);

            if (criteria.RelationType.HasValue)
                spec &= (t => t.RelationTypeValue == (int)criteria.RelationType.Value);

            if (!string.IsNullOrWhiteSpace(criteria.FileName))
            {
                var criteriaFileName = criteria.FileName.Trim();
                spec &= (t => t.FileName == criteriaFileName);
            }

            if (criteria.StartRelationType.HasValue)
                spec &= (t => t.RelationTypeValue >= (int)criteria.StartRelationType.Value);

            if (criteria.EndRelationType.HasValue)
                spec &= (t => t.RelationTypeValue <= (int)criteria.EndRelationType.Value);

            return spec;
        }

    }
}

