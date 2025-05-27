using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Services.Interfaces
{
    public interface IEntityMapper<TEntity, TDto, TSummaryDto>
    {
        TDto MapToDto(TEntity entity);
        TSummaryDto MapToSummaryDto(TEntity entity);

        List<TDto> MapToDtoList(IEnumerable<TEntity> entities);
        List<TSummaryDto> MapToSummaryDtoList(IEnumerable<TEntity> entities);
    }
}
