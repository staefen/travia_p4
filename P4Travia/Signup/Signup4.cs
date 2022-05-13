using Android.App;
using Android.OS;
using Android.Widget;
using System;
using Android.Runtime;
using Android.Content;

namespace P4Travia.Signup
{
    [Activity(Label = "Signup4")]
    public class Signup4 : Activity
    {
        Button signup4;
        Button skip2;
        string email, password, name, gender, nationality, language;
        int birthday;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.signup_4);

            signup4 = FindViewById<Button>(Resource.Id.btnNext4);
            signup4.Click += Signup4_Click;

            skip2 = FindViewById<Button>(Resource.Id.btnSkip2);
            skip2.Click += Skip2_Click;
        }

        private void Signup4_Click(object sender, EventArgs e)
        {
            language = Intent.GetStringExtra("Language");
            nationality = Intent.GetStringExtra("Nationality");
            gender = Intent.GetStringExtra("Gender");
            email = Intent.GetStringExtra("Email");
            password = Intent.GetStringExtra("Password");
            birthday = Intent.GetIntExtra("Birthday", 000000);
            name = Intent.GetStringExtra("Name");

            var intent = new Intent(this, typeof(Signup5));
            intent.PutExtra("Language", language);
            intent.PutExtra("Nationality", nationality);
            intent.PutExtra("Gender", gender);
            intent.PutExtra("Birthday", birthday);
            intent.PutExtra("Name", name);
            intent.PutExtra("Email", email);
            intent.PutExtra("Password", password);

            StartActivity(intent);
        }

        private void Skip2_Click(object sender, EventArgs e)
        {
            language = Intent.GetStringExtra("Language");
            nationality = Intent.GetStringExtra("Nationality");
            gender = Intent.GetStringExtra("Gender");
            email = Intent.GetStringExtra("Email");
            password = Intent.GetStringExtra("Password");
            birthday = Intent.GetIntExtra("Birthday", 000000);
            name = Intent.GetStringExtra("Name");

            var intent = new Intent(this, typeof(Signup5));
            intent.PutExtra("Language", language);
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
