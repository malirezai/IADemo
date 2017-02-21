using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iaservice.DataObjects
{
    public class FormItem : EntityData
    {
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FormType { get; set; }
        public string FormData { get; set; }
        public string EnteredDateUTC { get; set; }
        public string Byte64StringImage { get; set; }
    }
    
}