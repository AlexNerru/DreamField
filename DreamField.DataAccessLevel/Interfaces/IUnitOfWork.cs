using System;

namespace DreamField.DataAccessLevel.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IFeedRepository FeedRepository { get; }
        IFarmRepository FarmRepository { get; }
        //INormRepository NormRepository { get; }
        IRationRepository RationRepository { get; }
        IUserRepository UserRepository { get; }

        void SaveChanges();
    }
}
