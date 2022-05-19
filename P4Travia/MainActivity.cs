using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.BottomNavigation;
using P4Travia.Activities;
using Android.Runtime;

namespace P4Travia
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        //Liste af fragments
        List<AndroidX.Fragment.App.Fragment> fragments;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.toolbar);
            SetButtons();
            SetContentView(Resource.Layout.bottom_nav_bar);

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

            ImageView notification = (ImageView)FindViewById(Resource.Id.notification_bell);
            notification.Click += Notification_Click;

            TextView location_settings = (TextView)FindViewById(Resource.Id.location);
            location_settings.Click += Location_Settings_Click;

            LocationOnMainPage();
            CurrentLocationOnMainPage();

        }

        private void CurrentLocationOnMainPage()
        {
            string strLocation;
            TextView location_mp = FindViewById<TextView>(Resource.Id.location);
            strLocation = (string)Intent.GetStringExtra("No Location");
            location_mp.Text = strLocation;
        }

        private void LocationOnMainPage()
        {
            string strLocation;
            TextView location_mp = FindViewById<TextView>(Resource.Id.location);
            strLocation = (string)Intent.GetStringExtra("No Location");
            location_mp.Text = strLocation;
        }

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
            Toast.MakeText(this, "Bøgse", ToastLength.Short).Show();
        }

        private void Location_Settings_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(LocationActivity));
        }

    }
}