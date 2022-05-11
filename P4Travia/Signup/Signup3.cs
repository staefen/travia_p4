using Android.App;
using Android.OS;
using Android.Widget;
using System;
using Android.Runtime;
using Android.Content;
using System.Collections.Generic;

using System.Globalization;
using Android.Views;
using Java.Interop;

namespace P4Travia.Signup
{
    [Activity(Label = "Signup3")]
    public class Signup3 : Activity
    {
        Button signup3;
        Button skip1;
        /*CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);*/
        string email, password, name, gender, nationality;
        int birthday;
        IList<string> language = new List<string>();
        CheckBox dan_OnClick, nor_OnClick, swe_OnClick;

        [Export("language_ItemSelected")]

        public void language_ItemSelected(View view)
        {
            switch (view.Id)
            {
                case Resource.Id.danish:
                    Toast.MakeText(this, "Danish selected", ToastLength.Short).Show();
                    language.Add("Danish");
                    break;
                case Resource.Id.norwegian:
                    Toast.MakeText(this, "Norwegian selected", ToastLength.Short).Show();
                    language.Add("Norwegian");
                    break;
                case Resource.Id.swedish:
                    Toast.MakeText(this, "Swedish selected", ToastLength.Short).Show();
                    language.Add("Swedish");
                    break;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.signup_3);

            signup3 = FindViewById<Button>(Resource.Id.btnNext3);
            signup3.Click += Signup3_Click;

            skip1 = FindViewById<Button>(Resource.Id.btnSkip1);
            skip1.Click += Skip1_Click;

            dan_OnClick = FindViewById<CheckBox>(Resource.Id.danish);
            nor_OnClick = FindViewById<CheckBox>(Resource.Id.norwegian);
            swe_OnClick = FindViewById<CheckBox>(Resource.Id.swedish);


            nationality = Intent.GetStringExtra("Nationality");
            gender = Intent.GetStringExtra("Gender");
            email = Intent.GetStringExtra("Email");
            password = Intent.GetStringExtra("Password");
            birthday = Intent.GetIntExtra("Birthday", 000000);
            name = Intent.GetStringExtra("Name");

        }

        private void Signup3_Click(object sender, EventArgs e) //skal ha betingelser som gjør at man skal fylle ut feltene først
        {
            /*if (string.IsNullOrEmpty(language))
            {
                Toast.MakeText(this, "Please select a language or press the Skip button", ToastLength.Short).Show();
                return;
            }
            else
            {*/
                var intent = new Intent(this, typeof(Signup4));
                intent.PutStringArrayListExtra("Language", language);
                intent.PutExtra("Nationality", nationality);
                intent.PutExtra("Gender", gender);
                intent.PutExtra("Birthday", birthday);
                intent.PutExtra("Name", name);
                intent.PutExtra("Email", email);
                intent.PutExtra("Password", password);

                StartActivity(intent);
            
        }

        private void Skip1_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Signup4));
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
