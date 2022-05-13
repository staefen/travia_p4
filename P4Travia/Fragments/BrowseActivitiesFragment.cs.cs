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
using Firebase.Storage;
using Google.Android.Material.FloatingActionButton;
using P4Travia.Activities;
using P4Travia.Adapter;
using P4Travia.Datamodels;
using P4Travia.EventListener;
using P4Travia.Helpers;
using AndroidX.AppCompat.App;

namespace P4Travia
{
    public class BrowseActivitiesFragment : AndroidX.Fragment.App.Fragment
    {
        TextView categoryTextView;
        RecyclerView activityRecyclerView;
        ActivityPostAdapter activityPostAdapter;
        List<ActivityPost> ListOfPost;
        PostEventListener postEventListener;
        
 
        // Varibales
        string category;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            View view = inflater.Inflate(Resource.Layout.browseActivities, container, false);
            ConnectViews(view);
            

            // Set our view from the "main" layout resource

            //Retrieves fullname on login
            InfoListener infoListener = new InfoListener();
            infoListener.FetchUser();


            //CreateData();
            FetchPost();

            return view;

        }

        private void ConnectViews(View view)
        {

            categoryTextView = (TextView)view.FindViewById(Resource.Id.category);
            activityRecyclerView = (RecyclerView)view.FindViewById(Resource.Id.activityRecyclerView);
            FloatingActionButton floatingAddButton = (FloatingActionButton)view.FindViewById(Resource.Id.floatingAddButton);


            category = this.Activity.Intent.GetStringExtra("category");
            categoryTextView.Text = category;

            
            floatingAddButton.Click += floatingAddButton_Click;



        }

        private void floatingAddButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this.Activity, typeof(CreatePostActivity));  // Har virket
            StartActivity(intent);
        }

        void FetchPost()
        {
            postEventListener = new PostEventListener();
            postEventListener.FetchPost();
            postEventListener.OnPostRetrieved += PostEventListener_OnPostRetrieved;
        }

        //Her henter vi en list af de post som er i Databasen (Listen laves i PostEventListener.cs) og smider dem ind i et recyclerview
        private void PostEventListener_OnPostRetrieved(object sender, PostEventListener.PostEventArgs e)
        {
            ListOfPost = new List<ActivityPost>();
            ListOfPost = e.Posts;

            //Her sorterer vi listen, så den nyeste post altid vises øverst i listen
            if (ListOfPost != null)
            {
                ListOfPost = ListOfPost.OrderByDescending(o => o.ActivityPostDate).ToList();
            }

            SetupRecyclerView();
        }

        //Dummy data
        void CreateData()
        {
            ListOfPost = new List<ActivityPost>();
            ListOfPost.Add(new ActivityPost { ActivityDescription = "Is this working? Hallo?", ActivityUserName = "Christian Holfelt"});
            ListOfPost.Add(new ActivityPost { ActivityDescription = "jlngj wEJFHJ wgljgkl wKJLEGF FGJKWGJ EGJJ GFEF", ActivityUserName = "Johnny Kirkegaard"});
            ListOfPost.Add(new ActivityPost { ActivityDescription = "Yes my boi", ActivityUserName = "Mufasa Uganda"});
            ListOfPost.Add(new ActivityPost { ActivityDescription = "Test. Test! Test? Test...", ActivityUserName = "Testo Testorino"});
        }

        void SetupRecyclerView()
        {
            activityRecyclerView.SetLayoutManager(new AndroidX.RecyclerView.Widget.LinearLayoutManager(activityRecyclerView.Context));
            activityPostAdapter = new ActivityPostAdapter(ListOfPost);
            activityRecyclerView.SetAdapter(activityPostAdapter);
            
        }

    }

}