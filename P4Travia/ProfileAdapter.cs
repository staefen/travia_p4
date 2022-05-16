using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using P4Travia.Datamodels;
using System;
using System.Collections.Generic;
using Android;

namespace P4Travia
{
    class ProfileAdapter : RecyclerView.Adapter
    {
        List<UserDataStorage> items;

        public ProfileAdapter(List<UserDataStorage> data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            View itemView = null;
            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.profileview, parent, false);
            var vh = new ProfileAdapterViewHolder(itemView);

            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as ProfileAdapterViewHolder;

            //holder.TextView.Text = items[position];

            holder.usernameTextView.Text = item.UserName;
            holder.nationalityTextView.Text = item.Nationality;
            holder.languageTextView.Text = item.Language; //list i stedet for string i .Text hmm
            holder.genderTextView.Text = item.Gender;
        }

        public override int ItemCount => items.Count;

    }

    public class ProfileAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }

        public TextView usernameTextView { get; set; }
        public TextView nationalityTextView { get; set; }
        public TextView languageTextView { get; set; }
        public TextView genderTextView { get; set; }


        public ProfileAdapterViewHolder(View itemView) : base(itemView)
        {
            usernameTextView = (TextView)itemView.FindViewById(Resource.Id.usernameTextView);
            nationalityTextView = (TextView)itemView.FindViewById(Resource.Id.nationalityTextView);
            languageTextView = (TextView)itemView.FindViewById(Resource.Id.languageTextView);
            genderTextView = (TextView)itemView.FindViewById(Resource.Id.genderTextView);
        }
    }
}