using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webvnue.BusinessDataLayer.Models
{
    public class Referral
    {
        public int id { get; set; }
        public int referrer { get; set; }
        public int referent { get; set; }
    }
}