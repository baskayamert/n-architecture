using Core.DataAccess;
using Core.Utilities.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IRefreshTokenDal : IEntityRepository<RefreshToken>
    {
    }
}
