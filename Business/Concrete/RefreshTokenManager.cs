using Business.Abstract;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RefreshTokenManager : IRefreshTokenService
    {
        IRefreshTokenDal _refreshTokenDal;

        public RefreshTokenManager(IRefreshTokenDal refreshTokenDal)
        {
            _refreshTokenDal = refreshTokenDal;
        }

        public void Add(RefreshToken refreshToken)
        {
            _refreshTokenDal.Add(refreshToken);
        }

        public List<RefreshToken> GetAll()
        {
            return _refreshTokenDal.GetAll();
        }

        public RefreshToken GetById(int refreshTokenId)
        {
            return _refreshTokenDal.Get(rt => rt.Id == refreshTokenId);
        }

        public RefreshToken GetByToken(string token)
        {
            return _refreshTokenDal.Get(rt => rt.Token == token);
        }

        public List<RefreshToken> GetByUserId(int userId)
        {
            return _refreshTokenDal.AsNoTracking(rf => rf.UserId == userId);
        }

        public void Update(RefreshToken refreshToken)
        {
            _refreshTokenDal.Update(refreshToken);
        }
    }
}
