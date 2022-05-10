using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using AndroidX.AppCompat.App;
using Google.Android.Material.BottomNavigation;

namespace P4Travia
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        //Liste af fragments
        List<AndroidX.Fragment.App.Fragment> fragments;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.bottom_nav_bar);

            //Elementer i listen som refererer til de fragments vi har lavet
            fragments = new List<AndroidX.Fragment.App.Fragment>();
            fragments.Add(new ProfileFragment());
            fragments.Add(new MainPageFragment());
            fragments.Add(new CalendarFragment());

            SupportFragmentManager.BeginTransaction()
                                    .Replace(Resource.Id.fragmentcontainer, fragments[1])
                                    .Commit();

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

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
    }
}

