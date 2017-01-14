using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelEntity.EF;

namespace ModelEntity.DAO
{
    public class UserDAO
    {
        //Khoi tao Contructor
        DBContextAPI db = null;
        public UserDAO()
        {
            db = new DBContextAPI(); //Khoi tao DB
        }
        public List<User> ListAll()
        {
            return db.Users.ToList();
        }
        public User ViewDetail(int id)
        {
            return db.Users.SingleOrDefault(x => x.ID == id);
        }
        public User GetByID(int id)
        {
            return db.Users.Find(id);
        }
        //Check User Name da co trong he thong chua
        public bool GetByName(string userName)
        {
            var result = db.Users.Count(x=>x.UserName==userName);
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public long Insert(User entity)
        {
            db.Users.Add(entity); //Users la bang da dc EF gen ra trong EF, Goi phuong thuc Add entity User gan vao Users
            db.SaveChanges(); //Sau do luu lai
            return entity.ID; //Tra ket qua 1 ra ben ngoai neu OK
        }
        public bool Update(User entity)  //Khoi tao Bang User; Users la bang anh xa bang User
        {
            var user = db.Users.Find(entity.ID);//Tim ID cua ban ghi can Update
            user.Name = entity.Name; //user.Name la(Users) EF da gen; entity.Name la CSDL
            user.UserName = entity.UserName;
            user.Status = entity.Status;
            db.SaveChanges();  //Luu thong tin sau khi Update
            return true;
        }
        public List<User> SearchUser(string name)
        {
            return db.Users.Where(x => x.Name == name).ToList();  //Trả ra nhiều bản ghi
            //return db.Users.SingleOrDefault(x=>x.Name==name); //Chỉ ra 1 bản ghi, First: trong bản ghi khác kiểu dữ liệu, Single cùng kiều
        }
        //Xóa bản ghi, nhận vào la ID
        public bool Delete(int id)
        {
            var user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return true;
        }
    }
}