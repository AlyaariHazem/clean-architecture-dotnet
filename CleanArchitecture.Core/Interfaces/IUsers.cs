namespace CleanArchitecture.Core.Interfaces
{
    public interface IUsers
    {
        void AddUser();
        void GetUserById();
        void GetAllUsers();
    }

    public class Users : IUsers
    {
        public void AddUser()
        {
            throw new NotImplementedException();
        }

        public void GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public void GetUserById()
        {
            throw new NotImplementedException();
        }
    }
}
