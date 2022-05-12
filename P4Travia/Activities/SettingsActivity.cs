using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using P4Travia.Helpers;
using System;
using P4Travia.Signup;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P4Travia.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class SettingsActivity : AppCompatActivity
    {


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.settings);

            TextView logoutTextview = (TextView)FindViewById(Resource.Id.logOut);

            logoutTextview.Click += Logout_Click;

        }

        private void Logout_Click(object sender, EventArgs e)
        {
            AndroidX.AppCompat.App.AlertDialog.Builder alert = new AndroidX.AppCompat.App.AlertDialog.Builder(this);
            alert.SetTitle("Logout");
            alert.SetMessage("Are you sure?");

            //Delete post from Firestore
            alert.SetNegativeButton("Signout", (o, args) =>
            {
                //postEventListener.RemoveListener();     //Dette skal tilføjes når Oliver er færdig med sit. Dette gør at appen stopper med at lytte efter nye posts
                AppDataHelper.GetFirebaseAuth().SignOut();
                StartActivity(typeof(SignupAndLogin));
                Finish();
            });

            alert.SetPositiveButton("Cancel", (o, args) =>
            {
                return;
            });

            alert.Show();
        }
    }
}