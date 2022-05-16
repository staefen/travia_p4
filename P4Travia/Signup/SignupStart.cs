using Android.App;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using P4Travia.Helpers;
using Android.Runtime;
using System.Linq;
using Android.Content;

//using Newtonsoft.Json;

namespace P4Travia.Signup
{
    [Activity(Label = "SignupStart")]
    public class SignupStart : AppCompatActivity
    {
        Button signupstart;
        EditText emailText, passwordText, confirmPasswordText;
        string email, password, confirm;

        //Oncreate
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.signup_start);

            emailText = (EditText)FindViewById(Resource.Id.signupEmail);
            passwordText = (EditText)FindViewById(Resource.Id.signupPassword);
            confirmPasswordText = (EditText)FindViewById(Resource.Id.signupPasswordConf);

            signupstart = FindViewById<Button>(Resource.Id.btnNextStart);
            signupstart.Click += SignupStart_Click;
        }

        private void SignupStart_Click(object sender, EventArgs e)
        {
            email = emailText.Text;
            password = passwordText.Text;
            confirm = confirmPasswordText.Text;

            if (!email.Contains("@"))
            {
                Toast.MakeText(this, "Please enter a valid email address", ToastLength.Short).Show();
                return;
            }
            else if (password.Length < 2)
            {
                Toast.MakeText(this, "Password must be atleast 2 characters", ToastLength.Short).Show();
                return;
            }
            else if (password != confirm)
            {
                Toast.MakeText(this, "Password does not match", ToastLength.Short).Show();
                return;
            }
            else
            {
                Signup1 user = new Signup1();
                user.PassInfo(email, password);

                var intent = new Intent(this, typeof(Signup1));
                intent.PutExtra("Email", email);
                intent.PutExtra("Password", password);

                StartActivity(intent);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}