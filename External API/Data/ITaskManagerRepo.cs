using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlusDashData.Data;

namespace External_API.Data
{
    public interface ITaskManagerRepo
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
    }
}
