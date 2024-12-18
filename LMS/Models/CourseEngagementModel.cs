using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class CourseEngagementModel
    {
        public string is_pageload { get; set; }

        public Int64 TOPICID { get; set; }
    }

    public class GetUserTopicList
    {
       
        public Int64 user_id { get; set; }
        public string user_name { get; set; }
        public Int64 TOPICID { get; set; }
        public string TOPICNAME { get; set; }
        public Int64 CONTENT_ID { get; set; }
        public string CONTENTTITLE { get; set; }

        public Int32 COST_ID { get; set; }
        public string cost_description { get;  set; }
        public DateTime CREATEDON { get; set; }

        public string EMPNAME { get; set; }

        public DateTime CONTENTLASTVIEW { get; set; }

        public Decimal TimeSpent { get; set; }
        

    }
}