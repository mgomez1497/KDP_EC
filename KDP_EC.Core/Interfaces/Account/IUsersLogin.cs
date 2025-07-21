using KDP_EC.Core.Models;
using KDP_EC.Core.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDP_EC.Core.Interfaces.Account
{
    public interface IUsersLogin
    {
        bool Login(string username, string password, out Guid? userId);
        List<UserInfoViewModel> GetUsersInfo(Guid userId);

        bool CreateUser(Guid Id,string username, string password);


    }
}
