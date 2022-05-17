using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace P4Travia.Datamodels
{
    public class ActivityPost
    {
        public string ActivityName { get; set; }
        public string ActivityId { get; set; }
        public string ActivityDate { get; set; }
        public string ActivityTime { get; set; }
        public string ActivitySpots { get; set; }
        // user name skal nok komme fra en User classe
        public string ActivityUserName { get; set; } //måske bare UserName
        public string ActivityDescription { get; set; }
        public string ActivityLocation { get; set; }

        public string ActivityImageId { get; set; }
        public string ActivityDownloadUrl { get; set; }
        public string ActivityOwnerId { get; set; } //måske UserName
        public DateTime PostDate { get; set; }
    }
}