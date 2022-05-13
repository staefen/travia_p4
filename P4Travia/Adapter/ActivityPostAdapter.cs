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

            //holder.TextView.Text = items[position];

            holder.usernameTextView.Text = item.ActivityUserName;
            holder.postBodyTextView.Text = item.ActivityDescription;

            
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

        //public override int Count => throw new NotImplementedException();

        void OnClick(PostAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(PostAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

        //public override Java.Lang.Object GetItem(int position)
        //{
        //    throw new NotImplementedException();
        //}

        //public override long GetItemId(int position)
        //{
        //    throw new NotImplementedException();
        //}

        //public override View GetView(int position, View convertView, ViewGroup parent)
        //{
        //    throw new NotImplementedException();
        //}
    }

    public class PostAdapterViewHolder : RecyclerView.ViewHolder
    {
        private Action<PostAdapterClickEventArgs> onClick;
        private Action<PostAdapterClickEventArgs> onLongClick;

        //public TextView TextView { get; set; }

        public TextView usernameTextView { get; set; }
        public TextView postBodyTextView { get; set; }
        public TextView likeCountTextView { get; set; }
        public ImageView postImageView { get; set; }
        public ImageView likeImageView { get; set; }

        public PostAdapterViewHolder(View itemView, Action<PostAdapterClickEventArgs> clickListener,
                            Action<PostAdapterClickEventArgs> longClickListener, Action<PostAdapterClickEventArgs> likeClickListener) : base(itemView)
        {
            usernameTextView = (TextView)itemView.FindViewById(Resource.Id.userNameTextView);
            postImageView = (ImageView)itemView.FindViewById(Resource.Id.eventImageView);
            //indsæt alle de andre her
            

            itemView.Click += (sender, e) => clickListener(new PostAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new PostAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }

        public PostAdapterViewHolder(View itemView, Action<PostAdapterClickEventArgs> onClick, Action<PostAdapterClickEventArgs> onLongClick) : base(itemView)
        {
            this.onClick = onClick;
            this.onLongClick = onLongClick;
        }
    }

    public class PostAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}