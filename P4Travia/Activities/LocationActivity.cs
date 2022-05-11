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
    [Activity(Label = "LocationActivity")]
    public class LocationActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // SetButtons();
            //  SetSeekbar();
            // SetLocationChange();
            
            //SetSWitchButtons();


            SetContentView(Resource.Layout.location_settings);
            Switch locationSwitch = FindViewById<Switch>(Resource.Id.switch_location);
            locationSwitch.Checked = true;

            locationSwitch.CheckedChange += delegate (object sender, CompoundButton.CheckedChangeEventArgs e)
            {
                // var toast = Toast.MakeText(this, "I Love Xamarin !" +
                // (e.IsChecked ? "Yes" : " No"), ToastLength.Short);
                // toast.Show();
                Toast.MakeText(this, "HEI", ToastLength.Short).Show();
            };


        TextView location_settings = (TextView)FindViewById(Resource.Id.location);
        location_settings.Click += Location_Settings_Click;

    }

    //Switch buttons

   /* private void SetSWitchButtons()
    {
        //Switch radiusSwitch = FindViewById<Switch>(Resource.Id.switch_radius);

        //radiusSwitch.Checked = true;


        //locationSwitch.CheckedChange += delegate(object sender, CompoundButton.CheckedChangeEventArgs e)
        /*locationSwitch.CheckedChange += (s, e) =>
        {
            /*  bool isChecked = b.IsChecked;

              LocationHandler lh = new LocationHandler();
              Location usrloc = new Location(); */
            /*
              var toast = Toast.MakeText(this, "I Love Xamarin !" +
              (e.IsChecked ? "Yes" : " No"), ToastLength.Short);
              toast.Show(); */
            /*
            e.IsChecked ?  : ;
            {    

                lh.UserLocation(usrloc);
            }
        }; 

    }*/

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