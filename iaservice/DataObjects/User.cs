using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static iaservice.Controllers.IAUserController;

namespace iaservice.DataObjects
{
    public class User : EntityData
    {

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        
        public string mobileserviceID { get; set; }

        public User() { }

        public User(UserInfo information, string id)
        {
            firstName = information.firstname;
            lastName = information.lastname;
            email = information.email;
            mobileserviceID = id;
            //WE MUST SET THE ID
            Id = id;
        }

    }

    public class UserInfo
    {
        public string email;
        public string firstname;
        public string lastname;
    }

    

}