using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace P4Travia.EventListener
{
    public class PostEventListener : Java.Lang.Object, IOnSuccessListener, IEventListener
    {
        public List<Post> ListOfPost = new List<Post>();

        public event EventHandler<PostEventArgs> OnPostRetrieved;

        public class PostEventArgs : EventArgs
        {
            public List<Post> Posts { get; set; }
        }

        public void FetchPost()
        {
            //Retrieve Only Once

            //AppDataHelper.GetFirestore().Collection("posts").Get()
            //    .AddOnSuccessListener(this);

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

            //Dette tjekker hele listen af posts igennem i databasen
            if (!snapshot.IsEmpty)
            {
                //Her sørger vi for at posts ikke bliver duplikeret
                if (ListOfPost.Count > 0)
                {
                    ListOfPost.Clear();
                }

                foreach (DocumentSnapshot item in snapshot.Documents)
                {
                    Post post = new Post();
                    post.ID = item.Id;

                    //Hvis en bruger ikke har mangler en eller flere parametre til en post, kan vi risikere at appen crasher
                    //Det kan vi håndtere med følgende:
                    post.PostBody = item.Get("post_body") != null ? item.Get("post_body").ToString() : "";

                    post.Author = item.Get("author") != null ? item.Get("author").ToString() : "";
                    post.ImageId = item.Get("image_id") != null ? item.Get("image_id").ToString() : "";
                    post.OwnerId = item.Get("owner_id") != null ? item.Get("owner_id").ToString() : "";
                    post.DownloadUrl = item.Get("download_url") != null ? item.Get("download_url").ToString() : "";
                    string datestring = item.Get("post_date") != null ? item.Get("post_date").ToString() : "";
                    post.PostDate = DateTime.Parse(datestring);

                    //Finder ud af hvilke posts der er blevet liked og hvor mange der liked en post
                    var data = item.Get("likes") != null ? item.Get("likes") : null;
                    if (data != null)
                    {
                        var dictionaryFromHashMap = new Android.Runtime.JavaDictionary<string, string>(data.Handle, JniHandleOwnership.DoNotRegister);

                        string uid = AppDataHelper.GetFirebaseAuth().CurrentUser.Uid;

                        post.LikeCount = dictionaryFromHashMap.Count;
                        if (dictionaryFromHashMap.Contains(uid))
                        {
                            post.Liked = true;
                        }
                    }

                    //Efter al data er hentet fra databasen, smider vi det ind i en list

                    ListOfPost.Add(post);
                }

                OnPostRetrieved?.Invoke(this, new PostEventArgs { Posts = ListOfPost });

            }
        }

    }
}