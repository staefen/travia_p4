﻿using System;
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
    [Activity(Label = "LocationActivity")]
    public class LocationActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.location_settings);
            //SetButtons();
            SetSeekbar();
            SetSWitchButtons();
            SetLocationChange();

        }
    

        //Switch buttons + haandtering af device location
        private void SetSWitchButtons()
        {
            Switch locationSwitch = FindViewById<Switch>(Resource.Id.switch_location);
            locationSwitch.Checked = false;
            Switch radiusSwitch = FindViewById<Switch>(Resource.Id.switch_radius);
            radiusSwitch.Checked = true;


            LocationHandler lh = new LocationHandler();
            Location usrloc = new Location();

            lh.UserLocation(usrloc);

            lh.ConvertCoordinate(usrloc);

            locationSwitch.CheckedChange += (s, e) =>
            {
                bool isChecked = e.IsChecked;

                if (isChecked == true)
                {
                    try
                    {

                        TextView changedLocation = FindViewById<TextView>(Resource.Id.location);
                        changedLocation.Text = usrloc.City;
                    }

                    catch { }
                }
                else
                {

                }
            };

            //Haandtering af radius GOER IKKE NOGET
            radiusSwitch.CheckedChange += (s, e) => 
            {
                bool isValid = e.IsChecked;

                if (isValid == true)
                {
                    
                }
            };

        }

        //Change location in toolbar
        private void SetLocationChange()
        {
            EditText changeLocation = FindViewById<EditText>(Resource.Id.change_location);
            Button applyButton = FindViewById<Button>(Resource.Id.apply_button);
            TextView changedLocation = FindViewById<TextView>(Resource.Id.location);

            string address = null;

            applyButton.Click += (e, o) =>
            {
                string location = changeLocation.Text;
                changedLocation.Text = location;

               address = location;
            };

            LocationHandler lh = new LocationHandler();
            Location usrloc = new Location();

            lh.ConvertAddress(address, usrloc);

        }

        //Seekbar radius toogle 
        public void SetSeekbar()
        {
            SeekBar seekbarRadius = FindViewById<SeekBar>(Resource.Id.radius_seekbar);
            TextView textResult = FindViewById<TextView>(Resource.Id.radius_km);

            double r; //Skal bruges til sortActivityProximity

            seekbarRadius.ProgressChanged += (s, e) =>
            {
 
                    textResult.Text = string.Format("{0}", e.Progress);
                    r = e.Progress;
            };

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