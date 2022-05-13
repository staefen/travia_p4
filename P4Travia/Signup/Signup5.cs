using Android.App;
using Android.OS;
using Android.Widget;
using System;
using Android.Runtime;
using Android.Content;
using System.Collections.Generic;


namespace P4Travia.Signup
{
    [Activity(Label = "Signup5")]
    public class Signup5 : Activity
    {
        Button signup5, skip3;
        EditText bioText;
        string bio, email, password, name, gender, nationality;
        int birthday;
        IList<string> language = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.signup_5);
            bioText = (EditText)FindViewById(Resource.Id.signupBio);

            signup5 = FindViewById<Button>(Resource.Id.btnNext5);
            signup5.Click += Signup5_Click;

            skip3 = FindViewById<Button>(Resource.Id.btnSkip3);
            skip3.Click += Skip3_Click;

            language = Intent.GetStringArrayListExtra("Language");
            nationality = Intent.GetStringExtra("Nationality");
            gender = Intent.GetStringExtra("Gender");
            email = Intent.GetStringExtra("Email");
            password = Intent.GetStringExtra("Password");
            birthday = Intent.GetIntExtra("Birthday", 000000);
            name = Intent.GetStringExtra("Name");
        }

        private void Signup5_Click(object sender, EventArgs e) //betingelse
        {
            bio = bioText.Text;

            //User cant ble empty
            if (string.IsNullOrEmpty(bio))
            {
                Toast.MakeText(this, "Please write a biography or press the Skip button", ToastLength.Short).Show();
                return;
            }
            else
            {
                var intent = new Intent(this, typeof(Signup6));
                intent.PutExtra("Bio", bio);
                intent.PutStringArrayListExtra("Language", language);
                intent.PutExtra("Nationality", nationality);
                intent.PutExtra("Gender", gender);
                intent.PutExtra("Birthday", birthday);
                intent.PutExtra("Name", name);
                intent.PutExtra("Email", email);
                intent.PutExtra("Password", password);

                StartActivity(intent);
            }
        }

        private void Skip3_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Signup6));
            intent.PutStringArrayListExtra("Language", language);
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