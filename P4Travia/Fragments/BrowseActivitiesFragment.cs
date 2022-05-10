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
using Google.Android.Material.FloatingActionButton;

namespace P4Travia
{
    public class BrowseActivitiesFragment : AndroidX.Fragment.App.Fragment
    {
        TextView categoryTextView;
        RecyclerView activityRecyclerView;
        

        // Varibales
        string category;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this.Activity, savedInstanceState);

            View view = inflater.Inflate(Resource.Layout.browseActivities, container, false);
            return view;

            // Set our view from the "main" layout resource

            categoryTextView = (TextView)view.FindViewById(Resource.Id.category);
            activityRecyclerView = (RecyclerView)view.FindViewById(Resource.Id.activityRecyclerView);


            category = this.Activity.Intent.GetStringExtra("category");
            categoryTextView.Text = category;

            


            FloatingActionButton floatingAddButton = (FloatingActionButton)view.FindViewById(Resource.Id.floatingAddButton);

        }

        
    }

}