using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using P4Travia.Helpers;
using P4Travia.Signup;

namespace P4Travia
{
    public class SettingFragment : AndroidX.Fragment.App.Fragment
    {

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.settings, container, false);
            ConnectView(view);
            return view;
        }

        private void ConnectView(View view)
        {
            TextView logoutTextview = (TextView)view.FindViewById(Resource.Id.logOut);
            logoutTextview.Click += Logout_Click;
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            AndroidX.AppCompat.App.AlertDialog.Builder alert = new AndroidX.AppCompat.App.AlertDialog.Builder(Activity);
            alert.SetTitle("Logout");
            alert.SetMessage("Are you sure?");

            //Delete post from Firestore
            alert.SetNegativeButton("Signout", (o, args) =>
            {
                //postEventListener.RemoveListener();     //Dette skal tilføjes når Oliver er færdig med sit. Dette gør at appen stopper med at lytte efter nye posts
                AppDataHelper.GetFirebaseAuth().SignOut();
                var intent = new Intent(Activity, typeof(SignupAndLogin));
                StartActivity(intent);
            });

            alert.SetPositiveButton("Cancel", (o, args) =>
            {
                return;
            });

            alert.Show();
        }


    }
}