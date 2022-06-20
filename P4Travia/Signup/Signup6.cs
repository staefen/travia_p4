using Android.App;
using Android.OS;
using Android.Widget;
using Firebase.Firestore;
using System;
using AndroidX.AppCompat.App;
using DocumentReference = Firebase.Firestore.DocumentReference;
using Java.Util;
using Firebase.Auth;
using P4Travia.EventListeners;
using P4Travia.Helpers;
using Android.Content;
using P4Travia.Fragments;
using Android;
using Firebase.Storage;
using Plugin.Media;
using Android.Graphics;
using Android.Views;

namespace P4Travia.Signup
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class Signup6 : AppCompatActivity
    {
        Button signup6;
        FirebaseFirestore database;
        FirebaseAuth mAuth;
        ProgressDialogFragment progressDialogue;
        ImageView profileImage;
        string location;


        TaskCompletionListeners taskCompletionListeners = new TaskCompletionListeners();
        TaskCompletionListeners downloadUrlListener = new TaskCompletionListeners();

        readonly string[] permissionGroup =
        {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.Camera
        };

        byte[] fileBytes;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.signup_6);

            signup6 = FindViewById<Button>(Resource.Id.btnNext6);
            signup6.Click += Signup6_Click;

            profileImage = (ImageView)FindViewById(Resource.Id.addProfilePic);
            profileImage.Click += ProfileImage_Click;

            location = Intent.GetStringExtra("Location");

            database = AppDataHelper.GetFirestore();
            mAuth = AppDataHelper.GetFirebaseAuth();

            RequestPermissions(permissionGroup, 0);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return true;
        }


        private void Signup6_Click(object sender, EventArgs e)
        {

            HashMap userMap = new HashMap();
            userMap.Put("username", AppDataHelper.GetName());
            userMap.Put("owner_id", AppDataHelper.GetFirebaseAuth().CurrentUser.Uid);
            userMap.Put("upload_date", DateTime.Now.ToString());

            DocumentReference newProfileRef = AppDataHelper.GetFirestore().Collection("profile_images_id").Document();
            string postKey = newProfileRef.Id;

            userMap.Put("profile_image_id", postKey);


            ShowProgressDialogue("Creating Account ...");

            // Save Post Image to Firebase Storaage
            StorageReference storageReference = null;
            if (fileBytes != null)
            {
                storageReference = FirebaseStorage.Instance.GetReference("profileImages/" + postKey);
                storageReference.PutBytes(fileBytes)
                    .AddOnSuccessListener(taskCompletionListeners)
                    .AddOnFailureListener(taskCompletionListeners);
            }

            // Image Upload Success Callback
            taskCompletionListeners.Success += (obj, args) =>
            {
                if (storageReference != null)
                {
                    storageReference.GetDownloadUrl().AddOnSuccessListener(downloadUrlListener);
                }
            };

            // Image Download URL Callback
            downloadUrlListener.Success += (obj, args) =>
            {
                string downloadUrl = args.Result.ToString();
                userMap.Put("profile_download_url", downloadUrl);

                // Save post to Firebase Firestore
                newProfileRef.Set(userMap);
                CloseProgressDialogue();

                var intent = new Intent(this, typeof(MainActivity));
                intent.PutExtra("Location", location);
                StartActivity(intent);

                Finish();
            };


            // Image Upload Failure Callback
            taskCompletionListeners.Failure += (obj, args) =>
            {
                Toast.MakeText(this, "Upload was not completed", ToastLength.Short).Show();
            };
        }

        private void ProfileImage_Click(object sender, EventArgs e)
        {
            AndroidX.AppCompat.App.AlertDialog.Builder photoAlert = new AndroidX.AppCompat.App.AlertDialog.Builder(this);
            photoAlert.SetMessage("Change Photo");

            photoAlert.SetNegativeButton("Take Photo", (thisalert, args) =>
            {
                // Capture Image
                TakePhoto();
            });

            photoAlert.SetPositiveButton("Upload Photo", (thisAlert, args) =>
            {
                // Choose Image
                SelectPhoto();
            });

            photoAlert.Show();
        }

        async void TakePhoto()
        {
            await CrossMedia.Current.Initialize();
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                CompressionQuality = 20,
                Directory = "Sample",
                Name = GenerateRandomString(6) + "travia.jpg"
            });

            if (file == null)
            {
                return;
            }

            //Converts file.path to byte array and set the resulting bitmap to imageview
            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            fileBytes = imageArray;

            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            profileImage.SetImageBitmap(bitmap);

        }

        async void SelectPhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                Toast.MakeText(this, "Upload not supported", ToastLength.Short).Show();
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                CompressionQuality = 30,
            });

            if (file == null)
            {
                return;
            }

            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            fileBytes = imageArray;

            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            profileImage.SetImageBitmap(bitmap);

        }

        void ShowProgressDialogue(string status)
        {
            progressDialogue = new ProgressDialogFragment(status);
            var trans = SupportFragmentManager.BeginTransaction();
            progressDialogue.Cancelable = false;
            progressDialogue.Show(trans, "Progress");
        }

        void CloseProgressDialogue()
        {
            if (progressDialogue != null)
            {
                progressDialogue.Dismiss();
                progressDialogue = null;
            }
        }


        string GenerateRandomString(int lenght)
        {
            System.Random rand = new System.Random();
            char[] allowchars = "ABCDEFGHIJKLOMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
            string sResult = "";
            for (int i = 0; i <= lenght; i++)
            {
                sResult += allowchars[rand.Next(0, allowchars.Length)];
            }

            return sResult;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}