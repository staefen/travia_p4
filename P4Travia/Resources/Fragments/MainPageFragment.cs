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
        TextView all;
        BrowseActivitiesFragment browseActivitiesFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            View view = inflater.Inflate(Resource.Layout.mainpage, container, false);
            return view;

            allButton = (ImageView)view.FindViewById(Resource.Id.allimage);
            foodButton = (ImageView)view.FindViewById(Resource.Id.foodrounded);
            all = (TextView)view.FindViewById(Resource.Id.othertext);


            allButton.Click += AllButton_Click;
            foodButton.Click += FoodButton_Click;
            all.Click += All_Click;

            
        }

        private void All_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this.Activity, "det virker", ToastLength.Long).Show();
        }

        private void AllButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this.Activity, typeof(BrowseActivitiesFragment));
            intent.PutExtra("category", "All");
            StartActivity(intent);
        }

        private void FoodButton_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this.Activity, "det virker", ToastLength.Long).Show();
           
        }

        


        /*Intent intent = new Intent(this.Activity, typeof(BrowseActivitiesFragment));
        intent.PutExtra("category", "Food");
            browseActivitiesFragment = new BrowseActivitiesFragment();
        var transaction = FragmentManager.BeginTransaction()
            .Add(Resource.Id.fragmentcontainer, browseActivitiesFragment, "BrowseActivitiesFragment")
            .Commit();*/

    }

}