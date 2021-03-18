using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
