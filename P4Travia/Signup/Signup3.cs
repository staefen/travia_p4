using Android.App;
using Android.OS;
using Android.Widget;
using System;
using Android.Runtime;
using Android.Content;

using System.Globalization;


namespace P4Travia.Signup
{
    [Activity(Label = "Signup3")]
    public class Signup3 : Activity
    {
        Button signup3;
        Button skip1;
        /*CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);*/
        string language, email, password, name, gender, nationality;
        int birthday;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.signup_3);

            //language spinner
            //https://stackoverflow.com/questions/42100972/how-to-get-item-selected-in-spinner-to-use-it-as-string
            var language_spinner = FindViewById<Spinner>(Resource.Id.signupLanguage);
            language_spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(language_ItemSelected);
            var language_adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.language_dropdown, Android.Resource.Layout.SimpleSpinnerItem);

            language_adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            language_spinner.Adapter = language_adapter;

            /*foreach (CultureInfo culture in cultures)
             {
                 languages += culture.EnglishName;
             }

             var spinner = FindViewById<Spinner>(Resource.Id.signupLanguage);
             spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
             var adapter = ArrayAdapter.CreateFromResource(
                     this, Resource.String.languages, Android.Resource.Layout.SimpleSpinnerItem);

             adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
             spinner.Adapter = adapter;     
             */

            signup3 = FindViewById<Button>(Resource.Id.btnNext3);
            signup3.Click += Signup3_Click;

            skip1 = FindViewById<Button>(Resource.Id.btnSkip1);
            skip1.Click += Skip1_Click;

            nationality = Intent.GetStringExtra("Nationality");
            gender = Intent.GetStringExtra("Gender");
            email = Intent.GetStringExtra("Email");
            password = Intent.GetStringExtra("Password");
            birthday = Intent.GetIntExtra("Birthday", 000000);
            name = Intent.GetStringExtra("Name");

        }

        //language spinner
        private void language_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            language = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, language, ToastLength.Long).Show();
        }

        private void Signup3_Click(object sender, EventArgs e) //skal ha betingelser som gjør at man skal fylle ut feltene først
        {
            if (string.IsNullOrEmpty(language))
            {
                Toast.MakeText(this, "Please select a language or press the Skip button", ToastLength.Short).Show();
                return;
            }
            else
            {
                var intent = new Intent(this, typeof(Signup4));
                intent.PutExtra("Language", language);
                intent.PutExtra("Nationality", nationality);
                intent.PutExtra("Gender", gender);
                intent.PutExtra("Birthday", birthday);
                intent.PutExtra("Name", name);
                intent.PutExtra("Email", email);
                intent.PutExtra("Password", password);

                StartActivity(intent);
            }
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
