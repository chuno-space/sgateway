using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHUNO.Framework.Infrastructure.Authentication
{
    public interface IUserProvider
    {
        public AuthUser? User { get; }
    }
}
