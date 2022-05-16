using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.BottomNavigation;
using P4Travia.Activities;
using P4Travia.Datamodels;
using P4Travia.EventListeners;
using P4Travia.Datamodels;
using AndroidX.RecyclerView.Widget;

namespace P4Travia
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        //Liste af fragments
        List<AndroidX.Fragment.App.Fragment> fragments;
        RecyclerView postRecyclerView;
        List<UserDataStorage> Items;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.toolbar);
            SetButtons();
            SetContentView(Resource.Layout.bottom_nav_bar);

            postRecyclerView = (RecyclerView)FindViewById(Resource.Id.postRecycleView);

            //Elementer i listen som refererer til de fragments vi har lavet
            fragments = new List<AndroidX.Fragment.App.Fragment>();
            fragments.Add(new ProfileViewFragment());
            fragments.Add(new MainPageFragment());
            fragments.Add(new CalendarFragment());

            SupportFragmentManager.BeginTransaction()
                                    .Replace(Resource.Id.fragmentcontainer, fragments[0])
                                    .Commit();

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            FetchUser();
        }
        
        // Henter user profile data
        void FetchUser()
        {
            UserListener userListener = new UserListener();
            userListener.FetchUser();
            SetupRecyclerView();
        }

        void SetupRecyclerView()
        {
            postRecyclerView.SetLayoutManager(new LinearLayoutManager(postRecyclerView.Context));
            Items = new List<UserDataStorage>();
            Items.Add(new UserDataStorage { UserName = "HELLO", Email = "MARIA@MAIL"});
            profileAdapter = new ProfileAdapter(Items);
            postRecyclerView.SetAdapter(profileAdapter);
        }*/

        // Footer
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_profile:
                    SupportFragmentManager.BeginTransaction()
                                    .Replace(Resource.Id.fragmentcontainer, fragments[0])
                                    .Commit();
                    return true;
                case Resource.Id.navigation_main:
                    SupportFragmentManager.BeginTransaction()
                                    .Replace(Resource.Id.fragmentcontainer, fragments[1])
                                    .Commit();
                    return true;
                case Resource.Id.navigation_calendar:
                    SupportFragmentManager.BeginTransaction()
                                    .Replace(Resource.Id.fragmentcontainer, fragments[2])
                                    .Commit();
                    return true;
            }
            return false;
        }

        //Toolbar

        private void SetButtons()
        {
            NotificationButton();
            LocationSettings();
        }

        //Buttons
        public void NotificationButton()
        {
            ImageView notification = (ImageView)FindViewById(Resource.Id.notification_bell);
            notification.Click += Notification_Click;
        }

        public void LocationSettings()
        {
            TextView location_settings = (TextView)FindViewById(Resource.Id.location);
            location_settings.Click += Location_Settings_Click;
        }


        //Clicked buttons
        private void Notification_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(NotificationActivity));
        }

        private void Location_Settings_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(LocationActivity));
        }
    }
}