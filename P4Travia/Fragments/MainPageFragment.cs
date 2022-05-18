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
        ImageView natureButton;
        ImageView sportButton;
        ImageView cultureButton;
        ImageView creativityButton;
        ImageView otherButton;
        BrowseActivitiesFragment fragment;
        Bundle bundle;

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
            natureButton = (ImageView)view.FindViewById(Resource.Id.natureimage);
            sportButton = (ImageView)view.FindViewById(Resource.Id.sportsimage);
            cultureButton = (ImageView)view.FindViewById(Resource.Id.cultureimage);
            creativityButton = (ImageView)view.FindViewById(Resource.Id.creativityimage);
            otherButton = (ImageView)view.FindViewById(Resource.Id.otherimage);

            allButton.Click += AllButton_Click;
            foodButton.Click += FoodButton_Click;
            musicButton.Click += MusicButton_Click;
            natureButton.Click += NatureButton_Click;
            sportButton.Click += SportButton_Click;
            cultureButton.Click += CultureButton_Click;
            creativityButton.Click += CreativityButton_Click;
            otherButton.Click += OtherButton_Click;
        }

        private void MusicButton_Click(object sender, EventArgs e)
        {
            bundle.PutString("category", "Music");
            fragment.Arguments = bundle;

            var transaction = FragmentManager.BeginTransaction()
                .Replace(Resource.Id.fragmentcontainer, fragment)
                .Commit();
        }

        private void AllButton_Click(object sender, EventArgs e)
        {
            bundle.PutString("category", "All");
            fragment.Arguments = bundle;

            var transaction = FragmentManager.BeginTransaction()
                .Replace(Resource.Id.fragmentcontainer, fragment)
                .Commit();
        }

        private void FoodButton_Click(object sender, EventArgs e)
        {
            bundle.PutString("category", "Food");
            fragment.Arguments = bundle;

            var transaction = FragmentManager.BeginTransaction()
                .Replace(Resource.Id.fragmentcontainer, fragment)
                .Commit();
        }

        private void NatureButton_Click(object sender, EventArgs e)
        {
            bundle.PutString("category", "Nature");
            fragment.Arguments = bundle;

            var transaction = FragmentManager.BeginTransaction()
                .Replace(Resource.Id.fragmentcontainer, fragment)
                .Commit();
        }

        private void SportButton_Click(object sender, EventArgs e)
        {
            bundle.PutString("category", "Sport");
            fragment.Arguments = bundle;

            var transaction = FragmentManager.BeginTransaction()
                .Replace(Resource.Id.fragmentcontainer, fragment)
                .Commit();
        }

        private void CreativityButton_Click(object sender, EventArgs e)
        {
            bundle.PutString("category", "Creativity");
            fragment.Arguments = bundle;

            var transaction = FragmentManager.BeginTransaction()
                .Replace(Resource.Id.fragmentcontainer, fragment)
                .Commit();
        }

        private void CultureButton_Click(object sender, EventArgs e)
        {
            bundle.PutString("category", "Culture");
            fragment.Arguments = bundle;

            var transaction = FragmentManager.BeginTransaction()
                .Replace(Resource.Id.fragmentcontainer, fragment)
                .Commit();
        }

        private void OtherButton_Click(object sender, EventArgs e)
        {
            bundle.PutString("category", "Other");
            fragment.Arguments = bundle;

            var transaction = FragmentManager.BeginTransaction()
                .Replace(Resource.Id.fragmentcontainer, fragment)
                .Commit();
        }
    }
}