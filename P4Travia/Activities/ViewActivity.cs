
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

namespace P4Travia.Activities
{
	[Activity (Label = "ViewActivity", MainLauncher = false)]	
	public class ViewActivity : Activity
    {
        TextView eventNameTextView;
        TextView eventDateTextView;
        TextView eventTimeTextView;
        TextView eventLocationTextView;
        TextView eventDescriptionTextView;


        string eventName;
        string eventDate;
        string eventTime;
        string eventLocation;
        string eventDescription;

	
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView(Resource.Layout.viewactivity);
            SetButton();


            eventNameTextView = (TextView)FindViewById(Resource.Id.activityNameTextView);
            eventTimeTextView = (TextView)FindViewById(Resource.Id.timeTextView);
            eventDateTextView = (TextView)FindViewById(Resource.Id.dateTextView);
            eventLocationTextView = (TextView)FindViewById(Resource.Id.locationTextView);
            eventDescriptionTextView = (TextView)FindViewById(Resource.Id.descriptionTextView);

            eventName = Intent.GetStringExtra("activity name");
            eventNameTextView.Text = eventName;

            eventDate = Intent.GetStringExtra("date");
            eventDateTextView.Text = eventDate;

            eventTime = Intent.GetStringExtra("time");
            eventTimeTextView.Text = eventTime;

            eventLocation = Intent.GetStringExtra("location");
            eventLocationTextView.Text = eventLocation;

            eventDescription = Intent.GetStringExtra("description");
            eventDescriptionTextView.Text = eventDescription;



        }

        public void SetButton()
        {
            Button request_button = (Button)FindViewById(Resource.Id.request);
            request_button.Click += Request_button_Click;
            request_button.Background.SetColorFilter(Android.Graphics.Color.ParseColor("#E76F51"), Android.Graphics.PorterDuff.Mode.Multiply);
        }

        public void Request_button_Click(object sender, EventArgs e)
        {
            //bool requestClicked = false;
            Button request_button = (Button)FindViewById(Resource.Id.request);
            if (request_button.Text == "Cancel")
            {
                CancelRequest(request_button);

            }
            else if (request_button.Text != "Cancel")
            {
                SendRequest(request_button);
            }
        }

        private void SendRequest(Button request_button)
        {
            Toast.MakeText(this, "Your request has been sent", ToastLength.Short).Show();
            request_button.Text = "Cancel";
            request_button.Background.SetColorFilter(Android.Graphics.Color.Gray, Android.Graphics.PorterDuff.Mode.Multiply);

        }

        private void CancelRequest(Button request_button)
        {
            Toast.MakeText(this, "Your request has been canceled", ToastLength.Short).Show();
            request_button.Text = "Request";
            request_button.Background.SetColorFilter(Android.Graphics.Color.ParseColor("#E76F51"), Android.Graphics.PorterDuff.Mode.Multiply);


        }
    }
}

