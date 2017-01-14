using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using ModelEntity.DAO;
using ModelEntity.EF;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;
using System.Collections;
using DemoWebAPI.Commons;

namespace DemoWebAPI.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        //public HttpResponseMessage GetUserLists()
        //{
        //    var model=new UserDAO().ListAll();
        //    return Request.CreateResponse(HttpStatusCode.OK, model);
        //}
        public List<User> GetUserLists()
        {
            var model = new UserDAO().ListAll();
            return model;
        }
        [HttpGet]
        public User GetUser(int id)
        {
            var model = new UserDAO().ViewDetail(id);
            return model;
        }
        [HttpPost]
        public bool InsertUser(string name, string userName,string passWord, Boolean status)
        {
            if (ModelState.IsValid)
            {
                //string name, string userName, bool status
                User user = new User();
                user.Name = name;
                user.UserName = userName;
                user.Password = passWord;
                user.Status = status;
                bool kiemtra = new UserDAO().GetByName(userName);
                if (kiemtra == true)
                {
                    return false;
                }
                else
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);//Goi ham ma hoa MD5 ben Common ma hoa Password
                    user.Password = encryptedMd5Pas; //Gan ma hoa Password
                    var userDao = new UserDAO();
                    long id = userDao.Insert(user);//Goi ham Insert lop UserDAO
                    if (id > 0)
                    {
                        return true;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Them User khong thanh cong !");
                    }
                }
               
            }
            return true;
        }
        [HttpPut]
        [AcceptVerbs("PUT")]
        public bool Update(User user)  //Kiểu đối tượng này đã được map với Json, Client nó đã map user và binding lúc Single rồi
        {
            //int id, string name, string userName, Boolean status
            //User user = new User();
            //user.ID = id;
            //user.Name = name;
            //user.UserName = userName;
            //user.Status = status;
            //User user= JsonConvert.DeserializeObject<User>(json);
            //long id=user.ID;
            //string name = user.Name;
            //string userName = user.UserName;
            //bool status = user.Status;
            //user.ID = id;
            //user.Name = name;
            //user.UserName = userName;
            //user.Status = status;
            UserDAO userDao = new UserDAO();
            var result = userDao.Update(user);
            if (result)
            {
                return true;
            }
            else return false;
        }
        [HttpDelete]
        public bool Delete(int id)
        {
            var userDao = new UserDAO();
            var result=userDao.Delete(id);
            if (result)
            {
                return true;
            }
            else return false;
        }
        [HttpGet]
        public List<User> Search(string name)
        {
            var model = new UserDAO().SearchUser(name); ;
            return model;
        }
    }
}
