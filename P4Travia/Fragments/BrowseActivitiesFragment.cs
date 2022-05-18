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

            View view = inflater.Inflate(Resource.Layout.browse_activities, container, false);
            ConnectViews(view);



            return view;



        }

        private void ConnectViews(View view)
        {

            categoryTextView = (TextView)view.FindViewById(Resource.Id.category);
            activityRecyclerView = (RecyclerView)view.FindViewById(Resource.Id.activityRecyclerView);
            FloatingActionButton floatingAddButton = (FloatingActionButton)view.FindViewById(Resource.Id.floatingAddButton);

            //Retrieves category from last fragment
            string category = Arguments.GetString("category");
            categoryTextView.Text = categoryTextView.Text + category;


            floatingAddButton.Click += floatingAddButton_Click;

            //Retrieves fullname on login
            InfoListener infoListener = new InfoListener();
            infoListener.FetchUser();


            //CreateData();
            FetchPost();

        }

        private void floatingAddButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this.Activity, typeof(CreatePostActivity));
            StartActivity(intent);
        }

        void FetchPost() //kald den GetPost
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

            if (ListOfPost != null)
            {
                ListOfPost = ListOfPost.OrderByDescending(o => o.PostDate).ToList();
            }

            SetupRecyclerView();
        }

        //Dummy data
        void CreateData()
        {
            ListOfPost = new List<ActivityPost>();
            ListOfPost.Add(new ActivityPost { ActivityName = "Is this working? Hallo?", ActivityUserName = "Christian Holfelt" });
            ListOfPost.Add(new ActivityPost { ActivityName = "jlngj wEJFHJ wgljgkl wKJLEGF FGJKWGJ EGJJ GFEF", ActivityUserName = "Johnny Kirkegaard" });
            ListOfPost.Add(new ActivityPost { ActivityName = "Yes my boi", ActivityUserName = "Mufasa Uganda" });
            ListOfPost.Add(new ActivityPost { ActivityName = "Test. Test! Test? Test...", ActivityUserName = "Testo Testorino" });
        }

        void SetupRecyclerView()
        {
            activityRecyclerView.SetLayoutManager(new AndroidX.RecyclerView.Widget.LinearLayoutManager(activityRecyclerView.Context));
            activityPostAdapter = new ActivityPostAdapter(ListOfPost);
            activityPostAdapter.ItemClick += ActivityAdapter_ItemClick;
            activityRecyclerView.SetAdapter(activityPostAdapter);
        }

        private void ActivityAdapter_ItemClick (object sender, PostAdapterClickEventArgs e)
        {
            var activityPost = ListOfPost[e.Position];
            Intent intent = new Intent(this.Activity, typeof(ViewActivity));
            intent.PutExtra("activity name", activityPost.ActivityName);
            intent.PutExtra("date", activityPost.ActivityDate);
            intent.PutExtra("time", activityPost.ActivityTime);
            intent.PutExtra("location", activityPost.ActivityLocation);
            intent.PutExtra("description", activityPost.ActivityDescription);
            intent.PutExtra("username", activityPost.ActivityUserName);
            intent.PutExtra("download_url", activityPost.ActivityDownloadUrl);
            intent.PutExtra("image_id", activityPost.ActivityId);

            StartActivity(intent);
        }
    }

}