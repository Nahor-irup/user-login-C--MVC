using DAL;
using Model;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class Login_Service : Login_Interface
    {
        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Login_Model GetUserByEmail(string email)
        {
            using(var _context =new UserLoginEntities())
            {
                try
                {
                    var data = (from lg in _context.Logins.Where(lg => lg.Email == email)
                                select new Login_Model()
                                {
                                    LoginId = lg.LoginId,
                                    Username = lg.Username,
                                    Password = lg.Password,
                                    Email = lg.Email,
                                    DateCreated = lg.DateCreated,
                                    DateUpdated = lg.DateUpdated,
                                    Attempt=lg.Attempt,
                                }).FirstOrDefault();
                    return data;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        //To add new user in database
        public bool Save(Login_Model model)
        {
            using (var _context = new UserLoginEntities())
            {
                try
                {
                    var data = new Login()
                    {
                        Username = model.Username,
                        Password = model.Password,
                        Attempt = model.Attempt,
                        DateCreated = DateTime.Now,
                        Email = model.Email
                    };
                    _context.Logins.Add(data);
                    _context.SaveChanges();
                    return true;

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public bool Update(Login_Model model)
        {
            using (var _context = new UserLoginEntities())
            {
                try
                {
                    var login = _context.Logins.Where(lid => lid.LoginId== model.LoginId).FirstOrDefault();
                    login.LoginId= model.LoginId;
                    login.Password = model.Password;
                    login.Attempt = model.Attempt;
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public bool UpdateAttempt(Login_Model model)
        {
            using (var _context = new UserLoginEntities())
            {
                try
                {
                    var login = _context.Logins.Where(lid => lid.LoginId == model.LoginId).FirstOrDefault();
                    login.LoginId = model.LoginId;
                    login.Attempt = model.Attempt;
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        //to check whether user exist or not 
        public bool UserExist(string username)
        {
            using (var _context = new UserLoginEntities())
            {
                try
                {
                    var data = (from login in _context.Logins.Where(lg => lg.Username == username)
                                select new Login_Model()
                                {
                                    Username = login.Username

                                }).FirstOrDefault();
                    if (data!=null&&username == data.Username)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        //to check whether email exist or not 
        public bool EmailExist(string email)
        {
            using (var _context = new UserLoginEntities())
            {
                try
                {
                    var data = (from login in _context.Logins.Where(lg => lg.Email == email)
                                select new Login_Model()
                                {
                                    Email = login.Email

                                }).FirstOrDefault();
                    if (data!=null&&email == data.Email)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }


    }
}
