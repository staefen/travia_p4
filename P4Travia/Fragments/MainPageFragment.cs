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
using AndroidX.AppCompat.App;
using P4Travia;

namespace P4Travia
{
    public class MainPageFragment : AndroidX.Fragment.App.Fragment
    {
        ImageView allButton;
        ImageView foodButton;
        ImageView musicButton;
        BrowseActivitiesFragment browseActivitiesFragment;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            View view = inflater.Inflate(Resource.Layout.mainpage, container, false);
            ConnectViews(view);
            return view;
        }

        private void ConnectViews(View view)
        {
            allButton = (ImageView)view.FindViewById(Resource.Id.allimage);
            foodButton = (ImageView)view.FindViewById(Resource.Id.foodrounded);
            musicButton = (ImageView)view.FindViewById(Resource.Id.musicrounded);


            allButton.Click += AllButton_Click;
            foodButton.Click += FoodButton_Click;
            musicButton.Click += MusicButton_Click;
        }

        private void MusicButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this.Activity, typeof(BrowseActivitiesFragment));
            intent.PutExtra("category", "Music");
            browseActivitiesFragment = new BrowseActivitiesFragment();
            var transaction = FragmentManager.BeginTransaction()
                .Replace(Resource.Id.fragmentcontainer, browseActivitiesFragment, "BrowseActivitiesFragment")
                .Commit();

        }

        private void AllButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this.Activity, typeof(BrowseActivitiesFragment));
            intent.PutExtra("category", "All");
            StartActivity(intent);

        }

        private void FoodButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this.Activity, typeof(BrowseActivitiesFragment));
            intent.PutExtra("category", "Food");
            browseActivitiesFragment = new BrowseActivitiesFragment();
            var transaction = FragmentManager.BeginTransaction()
                .Replace(Resource.Id.fragmentcontainer, browseActivitiesFragment, "BrowseActivitiesFragment")
                .Commit();

        }

    }

}