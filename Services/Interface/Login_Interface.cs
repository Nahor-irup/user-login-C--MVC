using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface Login_Interface
    {
        bool UserExist(string username);
        bool EmailExist(string email);
        Login_Model GetUserByEmail(string email);
        bool Save(Login_Model model);
        bool Update(Login_Model model);
        bool UpdateAttempt(Login_Model model);
        bool Delete(long id);
    }
}
