using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using P4Travia.Datamodels;
using System;
using System.Collections.Generic;

namespace FacePost.Adapter
{
    class PostAdapter
    {
        // Create new views (invoked by the layout manager)
        public override void OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = null;
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.profileview, parent, false);
            var vh = new ProfileAdapterViewHolder(itemView);

        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            UserDataStorage item = new UserDataStorage();


            // Replace the contents of the view with that element
            var holder = viewHolder as ProfileAdapterViewHolder;

            //holder.TextView.Text = items[position];

            holder.nameTextView.Text = item.UserName;
  

    }

    public class ProfileAdapterViewHolder
    {
        //public TextView TextView { get; set; }

        public TextView nameTextView { get; set; }
        public TextView postBodyTextView { get; set; }
        public TextView likeCountTextView { get; set; }
        public ImageView postImageView { get; set; }
        public ImageView likeImageView { get; set; }

            public ProfileAdapterViewHolder(View itemView)
            {
                nameTextView = (TextView)itemView.FindViewById(Resource.Id.usernameTextView);
            }
    }
}