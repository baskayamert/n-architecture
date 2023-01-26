using Core.Entities.Concrete;
using Core.Utilities.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRefreshTokenService
    {
        void Add(RefreshToken refreshToken);
        void Update(RefreshToken refreshToken);
        RefreshToken GetById(int refreshTokenId);
        RefreshToken GetByToken(string token);
        List<RefreshToken> GetAll();
        List<RefreshToken> GetByUserId(int userId);
    }
}
