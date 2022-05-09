using Android.App;
using Android.OS;
using Android.Widget;
using System;
using Android.Runtime;
using Android.Content;

namespace P4Travia.Signup
{
    [Activity(Label = "Signup1")]
    public class Signup1 : Activity
    {
        Button signup1;
        EditText nameText, birthdayText;
        string name;
        int birthday;
        string password, email;

        //OnCreate
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.signup_1);

            nameText = (EditText)FindViewById(Resource.Id.signupName);
            birthdayText = (EditText)FindViewById(Resource.Id.signupBirthday);

            signup1 = FindViewById<Button>(Resource.Id.btnNext1);
            signup1.Click += Signup1_Click;
        }

        //Button
        private void Signup1_Click(object sender, EventArgs e)
        {
            birthday = Int32.Parse(birthdayText.Text);
            name = nameText.Text;

            //User cant ble empty
            if (string.IsNullOrEmpty(name))
            {
                Toast.MakeText(this, "Please enter a username", ToastLength.Short).Show();
                return;
            }
            else
            {
                email = Intent.GetStringExtra("Email");
                password = Intent.GetStringExtra("Password");

                var intent = new Intent(this, typeof(Signup2));
                intent.PutExtra("Birthday", birthday);
                intent.PutExtra("Name", name);
                intent.PutExtra("Email", email);
                intent.PutExtra("Password", password);

                StartActivity(intent);
            }
        }

        public void PassInfo(string _email, string _password)
        {
            email = _email;
            password = _password;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}