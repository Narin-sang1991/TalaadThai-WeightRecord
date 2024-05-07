using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Domain
{
    public class UserToken : HwEntity
    {
        protected UserToken() { }
        public UserToken(Guid iUserId)
        {
            this.UserId = iUserId;
        }

        private Guid userId;
        public Guid UserId
        {
            get { return userId; }
            private set { userId = value; }
        }

        private string token;
        public string Token
        {
            get { return token; }
            private set { token = value; }
        }

        public void SetToken(string iToken)
        {
            this.Token = iToken;
        }
    }

}
