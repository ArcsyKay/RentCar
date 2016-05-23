using RentCarService.Models;
using System.ServiceModel;

namespace RentCarService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRentCarService" in both code and config file together.
    [ServiceContract]
    public interface IRentCarService
    {
        [OperationContract]
        bool CheckLogin(User user);

        [OperationContract]
        bool CheckLoggedIn();

        [OperationContract]
        bool CheckRegister(User user);

        [OperationContract]
        void Register(User user);
    }
}
