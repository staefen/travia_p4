
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
	[Activity (Label = "LocationActivity")]			
	public class LocationActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

            SetButtons();
            SetSeekbar();
            SetLocationChange();
            SetSWitchButtons();

        }

        //Switch buttons
        private void SetSWitchButtons()
        {
            Switch locationSwitch = FindViewById<Switch>(Resource.Id.switch_location);
            Switch radiusSwitch = FindViewById<Switch>(Resource.Id.switch_radius);

            //locationSwitch.Checked = true;
            //radiusSwitch.Checked = true;

            //locationSwitch.CheckedChange += (s, b) =>
            //{
            //    bool isChecked = b.IsChecked;
            //    if(isChecked)
            //};
        }

        //Change location in toolbar
        private void SetLocationChange()
        {
            EditText changeLocation = FindViewById<EditText>(Resource.Id.change_location);
            Button applyButton = FindViewById<Button>(Resource.Id.apply_button);
            TextView changedLocation = FindViewById<TextView>(Resource.Id.location);

            applyButton.Click += (e, o) =>
            {
                string location = changeLocation.Text;
                changedLocation.Text = location;
            };
        }

        //Seekbar radius toogle 
        public void SetSeekbar()
        {
            SeekBar seekbarRadius = FindViewById<SeekBar>(Resource.Id.radius_seekbar);
            TextView textResult = FindViewById<TextView>(Resource.Id.radius_km);

            seekbarRadius.ProgressChanged += (s, e) =>
            {
                textResult.Text = string.Format("{0}", e.Progress);
            };

        }

        //Toolbar buttons
        private void SetButtons()
        {
            NotificationButton();
            LocationSettings();
        }

        //Buttons
        public void NotificationButton()
        {
            ImageView notification = (ImageView)FindViewById(Resource.Id.notification_bell);
            notification.Click += Notification_Click;
        }

        public void LocationSettings()
        {
            TextView location_settings = (TextView)FindViewById(Resource.Id.location);
            location_settings.Click += Location_Settings_Click;
        }


        //Clicked buttons
        private void Notification_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(NotificationActivity));
        }

        private void Location_Settings_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(LocationActivity));
        }

    }
}

