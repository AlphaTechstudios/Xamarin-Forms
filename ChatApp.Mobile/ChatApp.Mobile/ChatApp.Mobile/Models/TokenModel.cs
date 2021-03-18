using System;

namespace ChatApp.Mobile.Models
{
    public class TokenModel
    {
        /// <summary>
        /// Gets or sets the authentication token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the RefreshToken.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the Token ExpireTime
        /// </summary>
        public DateTime TokenExpireTime { get; set; }
    }
}
