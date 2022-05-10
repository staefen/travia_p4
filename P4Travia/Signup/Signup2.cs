using Android.App;
using Android.OS;
using Android.Widget;
using System;
using Android.Runtime;
using Android.Content;
using System.Globalization;
using System.Collections.Generic;


namespace P4Travia.Signup
{
    [Activity(Label = "Signup2")]
    public class Signup2 : Activity
    {
        Button signup2;
        string gender, nationality, email, password, name;
        int birthday;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.signup_2);

            //nationality spinner
            //https://stackoverflow.com/questions/42100972/how-to-get-item-selected-in-spinner-to-use-it-as-string
            var nationality_spinner = FindViewById<Spinner>(Resource.Id.signupNationality);
            nationality_spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(nationality_ItemSelected);
            var nationality_adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.nationality_dropdown, Android.Resource.Layout.SimpleSpinnerItem);

            nationality_adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            nationality_spinner.Adapter = nationality_adapter;


            //gender spinner
            var gender_spinner = FindViewById<Spinner>(Resource.Id.signupGender);
            gender_spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(gender_ItemSelected);
            var gender_adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.gender_dropdown, Android.Resource.Layout.SimpleSpinnerItem);

            gender_adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            gender_spinner.Adapter = gender_adapter;

            //Next button
            signup2 = FindViewById<Button>(Resource.Id.btnNext2);
            signup2.Click += Signup2_Click;

        }

        //nationality spinner
        private void nationality_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            nationality = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
        }

        //gender spinner
        private void gender_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            gender = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
        }

        private void Signup2_Click(object sender, EventArgs e)
        {
            email = Intent.GetStringExtra("Email");
            password = Intent.GetStringExtra("Password");
            birthday = Intent.GetIntExtra("Birthday", 000000);
            name = Intent.GetStringExtra("Name");

            var intent = new Intent(this, typeof(Signup3));
            intent.PutExtra("Nationality", nationality);
            intent.PutExtra("Gender", gender);
            intent.PutExtra("Birthday", birthday);
            intent.PutExtra("Name", name);
            intent.PutExtra("Email", email);
            intent.PutExtra("Password", password);

            StartActivity(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}