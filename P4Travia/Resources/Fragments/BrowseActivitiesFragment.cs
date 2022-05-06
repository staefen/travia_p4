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

namespace P4Travia.Resources.Fragments
{
    public class BrowseActivitiesFragment : AndroidX.Fragment.App.Fragment
    {
        TextView categoryTextView;
        RecyclerView activityRecyclerView;
        


        // Varibales
        string category;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            View view = inflater.Inflate(Resource.Layout.browseActivities, container, false);
            return view;

            // Set our view from the "main" layout resource
            


            categoryTextView = (TextView)FindViewById(Resource.Id.category);
            activityRecyclerView = (RecyclerView)FindViewById(Resource.Id.activityRecyclerView); // måske er det her forkert

            category = Intent.GetStringExtra("category");
            categoryTextView.Text = category;


            FloatingActionButton floatingAddButton = (FloatingActionButton)FindViewById(Resource.Id.floatingAddButton);


            floatingAddButton.Click += floatingAddButton_Click;
            CreateData();
            SetupRecyclerView();

        }

        private void floatingAddButton_Click(object sender, EventArgs e)
        {
            createPostFragment = new CreatePostFragment();
            var trans = SupportFragmentManager.BeginTransaction();
            createPostFragment.Show(trans, "new post");  // forstår ikke helt det her
            createPostFragment.OnPostRegistered += CreatePostFragment_OnPostRegistered;
        }

    }
}