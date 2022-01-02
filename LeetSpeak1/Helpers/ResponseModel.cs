using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeetSpeak1.Helpers
{
    public class ResponseModel
    {

        public Sucess success { get; set; }
        public Error error { get; set; }
        public Contents contents { get; set; }
     
    }

    public class Sucess{

        public int total { get; set; }
    }
    public class Error
    {

        public string code { get; set; }
        public string message { get; set; }
    }

    public class Contents
    {

       
        public string translated { get; set; }
        public string text { get; set; }
        public string translation { get; set; }


        
    }

}