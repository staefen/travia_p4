
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
using FFImageLoading;

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
        TextView userNameTextView;
        ImageView eventimageView;

        string eventName;
        string eventDate;
        string eventTime;
        string eventLocation;
        string eventDescription;
        string userName; //null
        string imageId;
        string downloadUrl;

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
            userNameTextView = (TextView)FindViewById(Resource.Id.userNameTextView);
            eventimageView = (ImageView)FindViewById(Resource.Id.activityimageView);

            imageId= Intent.GetStringExtra("image_id");
            downloadUrl= Intent.GetStringExtra("download_url");

            GetImage(downloadUrl, eventimageView);

            eventName = Intent.GetStringExtra("activity name");
            eventNameTextView.Text = eventName;

            eventLocation = Intent.GetStringExtra("location");
            eventLocationTextView.Text = eventLocation;

            eventDescription = Intent.GetStringExtra("description");
            eventDescriptionTextView.Text = eventDescription;

            userName = Intent.GetStringExtra("username");
            userNameTextView.Text = userName;


            eventTime = Intent.GetStringExtra("time");
            eventTimeTextView.Text = eventTime;

            eventDate = Intent.GetStringExtra("date");
            eventDateTextView.Text = eventDate;

        }

        void GetImage(string url, ImageView imageView)
        {
            ImageService.Instance.LoadUrl(url)
                .Retry(3, 200) //Vil prøve at downloade et billede 3 gange med et delay på 200 ms
                .DownSample(400, 400) //Compressing Image - Size er 400 x 400 for at reducere billedets størrelse
                .Into(imageView); //Sender billedet til et ImageView
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

