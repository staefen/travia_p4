using System;
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
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using P4Travia.Datamodels;
using System;
using System.Collections.Generic;
using Android;

namespace P4Travia
{
    public class ProfileViewFragment : AndroidX.Fragment.App.Fragment
    {
        Button editProfilebutton;
        Button settingsButton;
        //TextView usernameTV;

        public TextView usernameTV { get; set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.profileview, container, false);
            ConnectView(view);
            SeView(view);
            return view;
        }

        // Replace the contents of a view (invoked by the layout manager)
        private void SeView(View view)
        {
            UserDataStorage user = new UserDataStorage();
            usernameTV.Text = user.UserName;

            usernameTV = (TextView)view.FindViewById(Resource.Id.usernameTextView);
        }

        private void ConnectView(View view)
        {
            editProfilebutton = (Button)view.FindViewById(Resource.Id.editprofilebutton);
            editProfilebutton.Click += EditProfileButton_Click;

            settingsButton = (Button)view.FindViewById(Resource.Id.settingsbutton);
            settingsButton.Click += SettingsButton_Click;
        }

        private void EditProfileButton_Click(object sender, EventArgs e)
        {
            AndroidX.Fragment.App.Fragment ProfileFragment = new ProfileFragment();
            var transaction = FragmentManager.BeginTransaction()
                .Replace(Resource.Id.fragmentcontainer, ProfileFragment)
                .Commit();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            AndroidX.Fragment.App.Fragment SettingFragment = new SettingFragment();
            var transaction = FragmentManager.BeginTransaction()
                .Replace(Resource.Id.fragmentcontainer, SettingFragment)
                .Commit();
        }
    }
}