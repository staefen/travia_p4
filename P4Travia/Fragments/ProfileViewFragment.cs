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
using AndroidX.RecyclerView.Widget;
using Android;
using AndroidX.AppCompat.App;
using P4Travia.Helpers;
using P4Travia.Activities;
using P4Travia.Datamodels;
using P4Travia.EventListeners;
using Firebase.Storage;

namespace P4Travia
{
    public class ProfileViewFragment : AndroidX.Fragment.App.Fragment
    {
        Button editProfilebutton;
        Button settingsButton;
        //TextView usernameTV;
        RecyclerView profileRecyclerView;
        ProfileAdapter profileAdapter;
        
        List<UserDataStorage> ListOfProfiles;

        ProfileListener profileListener;

        public TextView usernameTV { get; set; }

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

            UserListener userListner = new UserListener();
            userListner.FetchUser();

            FetchMyPost();

        }

        void FetchMyPost()
        {
            profileListener = new ProfileListener();
            profileListener.FetchProfile();
            profileListener.OnProfileRetrieved += PostEventListener_OnPostRetrieved;
        }

        //Her henter vi en list af de post som er i Databasen (Listen laves i PostEventListener.cs) og smider dem ind i et recyclerview
        private void PostEventListener_OnPostRetrieved(object sender, ProfileListener.ProfileEventArgs e)
        {
            ListOfProfiles = new List<UserDataStorage>();
            ListOfProfiles = e.userList;

            SetupRecyclerView();
        }

        void SetupRecyclerView()
        {
            profileRecyclerView.SetLayoutManager(new AndroidX.RecyclerView.Widget.LinearLayoutManager(profileRecyclerView.Context));
            profileAdapter = new ProfileAdapter(ListOfProfiles);
            profileRecyclerView.SetAdapter(profileAdapter);
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