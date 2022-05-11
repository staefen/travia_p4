using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;

namespace P4Travia.Signup
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button login1;
        Button createAccount;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.login_signup_start);

            //Login button 
            login1 = (Button)FindViewById(Resource.Id.btnLogin1);
            login1.Click += Login_Click;

            //Signup button
            createAccount = FindViewById<Button>(Resource.Id.btnSignupStart);
            createAccount.Click += CreateAccount_Click;
        }
        //Go to login page
        private void Login_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Login));
        }
        //Go to signup page
        private void CreateAccount_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SignupStart));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

}
