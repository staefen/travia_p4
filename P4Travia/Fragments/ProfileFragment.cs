﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace P4Travia
{
    public class ProfileFragment : AndroidX.Fragment.App.Fragment
    {
        Button reportButton;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.profile, container, false);
            return view;

            reportButton = (Button)view.FindViewById(Resource.Id.reportbutton);

        }

    }
}