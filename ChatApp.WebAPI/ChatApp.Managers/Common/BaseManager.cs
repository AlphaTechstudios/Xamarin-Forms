using ChatApp.Repositories.Interfaces;

namespace ChatApp.Managers.Common
{
    public class BaseManager
    {
        private readonly IUnitOfWork unitOfWork;

        public BaseManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork => unitOfWork;
    }
}
