using Android.App;
using Android.Content;
using Android.OS;
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
using Firebase.Storage;
using P4Travia.Fragments;
using Android.Views;

namespace P4Travia.Activities
{
    [Activity(Label = "CreatePostActivity", Theme = "@style/AppTheme", MainLauncher = true)]
    public class CreatePostActivity : AppCompatActivity

    {

        Button postButton;
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


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
    }
}