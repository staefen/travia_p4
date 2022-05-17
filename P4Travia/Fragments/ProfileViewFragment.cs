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

namespace P4Travia
{
    public class ProfileViewFragment : AndroidX.Fragment.App.Fragment
    {
        Button editProfilebutton;
        Button settingsButton;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.profileview, container, false);
            ConnectView(view);
            return view;
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
            AndroidX.Fragment.App.Fragment ProfileViewFragment = new ProfileViewFragment();
            var transaction = FragmentManager.BeginTransaction()
                .Replace(Resource.Id.fragmentcontainer, ProfileViewFragment)
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