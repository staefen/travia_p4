using Android;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using Plugin.Media;
using System;
using P4Travia.EventListeners;
using P4Travia.Helpers;
using Java.Util;
using Firebase.Firestore;
using Android.Views;
using Firebase.Storage;
using P4Travia.Fragments;

namespace P4Travia.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class CreatePostActivity : AppCompatActivity
    {
        //AndroidX.AppCompat.Widget.Toolbar toolbar;
        ImageView postImage;
        Button postButton;
        Button cancelButton;
        EditText titleEditText;
        EditText dateEditText;
        EditText timeEditText;
        EditText locationEditText;
        EditText participantsEditText;
        EditText descriptionEditText;

        TaskCompletionListeners taskCompletionListeners = new TaskCompletionListeners();
        TaskCompletionListeners downloadUrlListener = new TaskCompletionListeners();

        readonly string[] permissionGroup =
        {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.Camera
        };

        byte[] fileBytes;
        ProgressDialogFragment progressDialogue;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.create_post);
            //toolbar = (AndroidX.AppCompat.Widget.Toolbar)FindViewById(Resource.Id.toolbar);
            //SetSupportActionBar(toolbar);
           // SupportActionBar.Title = "Create Post";

            //AndroidX.AppCompat.App.ActionBar actionBar = SupportActionBar;
            //actionBar.SetDisplayHomeAsUpEnabled(true);
            //actionBar.SetHomeAsUpIndicator(Resource.Drawable.outline_arrowback);
            titleEditText = (EditText)FindViewById(Resource.Id.editActivityName);
            dateEditText = (EditText)FindViewById(Resource.Id.editActivityDate);
            timeEditText = (EditText)FindViewById(Resource.Id.editActivityTime);
            locationEditText = (EditText)FindViewById(Resource.Id.editActivityLocation);
            participantsEditText = (EditText)FindViewById(Resource.Id.editActivityParticipants);
            descriptionEditText = (EditText)FindViewById(Resource.Id.editActivityDescription);
            

            postImage = (ImageView)FindViewById(Resource.Id.addImage);
            postImage.Click += PostImage_Click;

            postButton = (Button)FindViewById(Resource.Id.postActivityBtn);
            postButton.Click += SubmitButton_Click;

            cancelButton = (Button)FindViewById(Resource.Id.cancelActivityBtn);
            cancelButton.Click += Cancel_Click;

            RequestPermissions(permissionGroup, 0);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return true;
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
           
            HashMap postMap = new HashMap();
            postMap.Put("name", titleEditText.Text);
            postMap.Put("username", AppDataHelper.GetName());
            postMap.Put("owner_id", AppDataHelper.GetFirebaseAuth().CurrentUser.Uid);
            postMap.Put("location", locationEditText.Text);
            postMap.Put("date", dateEditText.Text);
            postMap.Put("time", timeEditText.Text);
            postMap.Put("participants", participantsEditText.Text);
            postMap.Put("description", descriptionEditText.Text);
            postMap.Put("post_date", DateTime.Now.ToString());

            DocumentReference newPostRef = AppDataHelper.GetFirestore().Collection("posts").Document();
            string postKey = newPostRef.Id;

            postMap.Put("image_id", postKey);


            ShowProgressDialogue("Posting ...");

            // Save Post Image to Firebase Storaage
            StorageReference storageReference = null;
            if (fileBytes != null)
            {
                storageReference = FirebaseStorage.Instance.GetReference("postImages/" + postKey);
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
                postMap.Put("download_url", downloadUrl);

                // Save post to Firebase Firestore
                newPostRef.Set(postMap);
                CloseProgressDialogue();
                Finish();
            };


            // Image Upload Failure Callback
            taskCompletionListeners.Failure += (obj, args) =>
            {
                Toast.MakeText(this, "Upload was not completed", ToastLength.Short).Show();
            };
        }

        private void PostImage_Click(object sender, EventArgs e)
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
            postImage.SetImageBitmap(bitmap);

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            AndroidX.AppCompat.App.AlertDialog.Builder alert = new AndroidX.AppCompat.App.AlertDialog.Builder(this);
            alert.SetTitle("Cancel");
            alert.SetMessage("Are you sure?");

            //Delete post from Firestore
            alert.SetNegativeButton("Yes", (o, args) =>
            {
                StartActivity(typeof(MainPageFragment));
            });

            alert.SetPositiveButton("No", (o, args) =>
            {
                return;
            });

            alert.Show();
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
            postImage.SetImageBitmap(bitmap);

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