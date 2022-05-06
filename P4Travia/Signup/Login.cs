using Android.App;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
//using P4Travia.EventListeners;
//using P4Travia.Helpers;
//using Firebase.Auth;
using System;
using Android.Runtime;

namespace P4Travia.Signup
{
    [Activity(Label = "Login")]
    public class Login : AppCompatActivity
    {
        EditText emailText, passwordText;
        Button login2;
       // TaskCompletionListeners taskCompletionListeners = new TaskCompletionListeners();
        // FirebaseAuth mAuth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.login);

            emailText = (EditText)FindViewById(Resource.Id.loginEmail);
            passwordText = (EditText)FindViewById(Resource.Id.loginPassword);

            login2 = FindViewById<Button>(Resource.Id.btnLogin2);
            login2.Click += Login2_Click;

         //   mAuth = AppDataHelper.GetFirebaseAuth();

        }

        private void Login2_Click(object sender, EventArgs e)
        {
            string email, password;
            email = emailText.Text;
            password = passwordText.Text;

            if (!email.Contains("@"))
            {
                Toast.MakeText(this, "Please provide a valid email address", ToastLength.Short).Show();
                return;
            }
            else if (password.Length < 2 || password.Length == 0)
            {
                Toast.MakeText(this, "Please provide a valid password", ToastLength.Short).Show();
                return;
            }

            /*
            mAuth.SignInWithEmailAndPassword(email, password).AddOnSuccessListener(taskCompletionListeners)
            .AddOnFailureListener(taskCompletionListeners);

            taskCompletionListeners.Success += (success, args) =>
            {
                StartActivity(typeof(MainActivity));
                Toast.MakeText(this, "Login was successfull", ToastLength.Short).Show();
            };

            taskCompletionListeners.Failure += (success, args) =>
            {
                Toast.MakeText(this, "Login Failed : " + args.Cause, ToastLength.Short).Show();
            };*/

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
