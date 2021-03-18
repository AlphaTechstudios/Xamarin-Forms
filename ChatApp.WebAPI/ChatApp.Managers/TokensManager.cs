using ChatApp.Managers.Common;
using ChatApp.Managers.Interfaces;
using ChatApp.Models;
using ChatApp.Repositories.Interfaces;
using System;
using System.Linq;

namespace ChatApp.Managers
{
    public class TokensManager : BaseManager, ITokensManager
    {
        private readonly ITokensRepository tokensRepository;

        public TokensManager(IUnitOfWork unitOfWork, ITokensRepository tokensRepository)
              : base(unitOfWork)
        {
            this.tokensRepository = tokensRepository;
        }

        public RefreshTokenModel GetRefreshToken(string refreshToken, string userAgent)
        {
            string incomeSource = string.IsNullOrEmpty(userAgent) ? "Mobile" : "Web";

            return tokensRepository.Get(x => x.RefreshToken == refreshToken && x.UserAgent == incomeSource).SingleOrDefault();
        }

        public RefreshTokenModel GetRefreshTokenByEmail(string email, string userAgent)
        {
            string incomeSource = string.IsNullOrEmpty(userAgent) ? "Mobile" : "Web";
            return tokensRepository.Get(x => x.UserEmail == email && x.UserAgent == incomeSource).SingleOrDefault();
        }

        public RefreshTokenModel UpdateToken(string refreshToekn, string userAgent)
        {
            var tokenModel = GetRefreshToken(refreshToekn, userAgent);
            if (tokenModel == null)
            {
                return null;
            }

            tokenModel.RefreshToken = Guid.NewGuid().ToString();
            tokenModel.ModificationDate = DateTime.Now;
            tokensRepository.Update(tokenModel);
            UnitOfWork.Commit();
            return tokenModel;
        }

        public RefreshTokenModel AddToken(string userEmail, string ipAddress, string userAgent)
        {
            var now = DateTime.Now;
            RefreshTokenModel tokenModel = new RefreshTokenModel
            {
                UserEmail = userEmail,
                RefreshToken = Guid.NewGuid().ToString(),
                ModificationDate = now,
                CreationDate = now,
                IpAddress = ipAddress,
                UserAgent = string.IsNullOrEmpty(userAgent) ? "Mobile" : "Web"
            };
            tokensRepository.Insert(tokenModel);
            UnitOfWork.Commit();

            return tokenModel;
        }
    }
}
