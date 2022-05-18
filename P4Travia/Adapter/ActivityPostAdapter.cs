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
using AndroidX.RecyclerView.Widget;
using FFImageLoading;
using P4Travia.Datamodels;

namespace P4Travia.Adapter
{
    internal class ActivityPostAdapter : RecyclerView.Adapter
    {

        public event EventHandler<PostAdapterClickEventArgs> ItemClick;
        public event EventHandler<PostAdapterClickEventArgs> ItemLongClick;
        List<ActivityPost> items;

        public ActivityPostAdapter(List<ActivityPost> data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            View itemView = null;
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.post, parent, false);
            PostAdapterViewHolder vh = new PostAdapterViewHolder(itemView, OnClick, OnLongClick);

            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as PostAdapterViewHolder;

            
            holder.dateTextView.Text = item.ActivityDate;
            holder.timeTextView.Text = item.ActivityTime;
            holder.nameTextView.Text = item.ActivityName;
            holder.spotsLeftTextView.Text = item.ActivitySpots;
            holder.userNameTextView.Text = item.ActivityUserName;

            GetImage(item.ActivityDownloadUrl, holder.postImageView);
        }

        //Denne metode finder billederne gennem URL og sætter dem ind i en row
        //Dette er for at billederne ikke gemmes permanent på en brugers tlf
        void GetImage(string url, ImageView imageView)
        {
            ImageService.Instance.LoadUrl(url)
                .Retry(3, 200) //Vil prøve at downloade et billede 3 gange med et delay på 200 ms
                .DownSample(400, 400) //Compressing Image - Size er 400 x 400 for at reducere billedets størrelse
                .Into(imageView); //Sender billedet til et ImageView
        }

        public override int ItemCount => items.Count;

        void OnClick(PostAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(PostAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class PostAdapterViewHolder : RecyclerView.ViewHolder
    {

        //private Action<PostAdapterClickEventArgs> onClick;
        //private Action<PostAdapterClickEventArgs> onLongClick;

        //public TextView TextView { get; set; }

        public TextView userNameTextView { get; set; }
        //public TextView postBodyTextView { get; set; }
        public ImageView postImageView { get; set; }
        public TextView nameTextView { get; set; }
        public TextView timeTextView { get; set; }
        public TextView dateTextView { get; set; }
        public TextView spotsLeftTextView { get; set; }

        public PostAdapterViewHolder(View itemView, Action<PostAdapterClickEventArgs> clickListener,
                            Action<PostAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            userNameTextView = (TextView)itemView.FindViewById(Resource.Id.userNameTextView);
            postImageView = (ImageView)itemView.FindViewById(Resource.Id.eventImageView);
            nameTextView = (TextView)itemView.FindViewById(Resource.Id.nameTextView);
            timeTextView = (TextView)itemView.FindViewById(Resource.Id.timeTextView);
            dateTextView = (TextView)itemView.FindViewById(Resource.Id.dateTextView);
            spotsLeftTextView = (TextView)itemView.FindViewById(Resource.Id.spotsLeftTextView);

            //indsæt alle de andre her


            itemView.Click += (sender, e) => clickListener(new PostAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new PostAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class PostAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}