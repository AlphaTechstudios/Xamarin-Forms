using ChatApp.DL;
using ChatApp.Models;
using ChatApp.Repositories.Common;
using ChatApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Repositories
{
    public class ConnectionsRepository : GenericRepository<ConnectionModel>, IConnectionsRepository
    {
        public ConnectionsRepository(ChatAppContext context) 
            : base(context)
        {
        }
    }
}
