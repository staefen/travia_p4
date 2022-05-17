using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using P4Travia.Datamodels;
using P4Travia.Helpers;

namespace P4Travia.EventListener
{
    public class PostEventListener : Java.Lang.Object, IOnSuccessListener, IEventListener
    {
        public List<ActivityPost> ListOfPost = new List<ActivityPost>();

        public event EventHandler<PostEventArgs> OnPostRetrieved;

        public class PostEventArgs : EventArgs
        {
            public List<ActivityPost> Posts { get; set; }
        }

        public void FetchPost()
        {


            AppDataHelper.GetFirestore().Collection("posts").AddSnapshotListener(this);
        }

        public void RemoveListener()
        {
            var listener = AppDataHelper.GetFirestore().Collection("posts").AddSnapshotListener(this);
            listener.Remove();
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            OrganizeData(result);
        }

        public void OnEvent(Java.Lang.Object value, FirebaseFirestoreException error)
        {
            OrganizeData(value);
        }

        void OrganizeData(Java.Lang.Object Value)
        {
            var snapshot = (QuerySnapshot)Value;



            foreach (DocumentSnapshot item in snapshot.Documents)
            {
                ActivityPost post = new ActivityPost();
                post.ActivityId = item.Id;

                //Hvis en bruger ikke har mangler en eller flere parametre til en post, kan vi risikere at appen crasher
                //Det kan vi håndtere med følgende:

                post.ActivityName = item.Get("name") != null ? item.Get("name").ToString() : "";
                post.ActivityDate = item.Get("date") != null ? item.Get("date").ToString() : "";
                post.ActivityTime = item.Get("time") != null ? item.Get("time").ToString() : "";
                post.ActivityLocation = item.Get("location") != null ? item.Get("location").ToString() : "";
                post.ActivityUserName = item.Get("username") != null ? item.Get("username").ToString() : "";
                post.ActivityDescription = item.Get("description") != null ? item.Get("description").ToString() : "";
                post.ActivitySpots = item.Get("participants") != null ? item.Get("participants").ToString() : "";
                string datestring = item.Get("post_date") != null ? item.Get("post_date").ToString() : "";
                post.PostDate = DateTime.Parse(datestring);

                post.ActivityImageId = item.Get("image_id") != null ? item.Get("image_id").ToString() : "";
                post.ActivityOwnerId = item.Get("owner_id") != null ? item.Get("owner_id").ToString() : "";
                post.ActivityDownloadUrl = item.Get("download_url") != null ? item.Get("download_url").ToString() : "";




                //Efter al data er hentet fra databasen, smider vi det ind i en list

                ListOfPost.Add(post);
            }

            OnPostRetrieved?.Invoke(this, new PostEventArgs { Posts = ListOfPost });


        }

    }
}