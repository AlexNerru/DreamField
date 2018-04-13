using DreamField.Model;

namespace DreamField.ServiceLayer
{
    public interface IUserService
    {
        User LoggedUser { get; set; }

        bool Login(string login, string password);
    }
}