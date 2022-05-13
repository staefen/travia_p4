
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DocumentReference = Firebase.Firestore.DocumentReference;
using Java.Util;
using Firebase.Auth;
using P4Travia.EventListeners;
using P4Travia.Helpers;

namespace P4Travia.Activities
{
	[Activity (Label = "ViewProfile")]			
	public class ViewProfile : Activity
	{
		EditText postName;
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView(Resource.Layout.profileview);
			postName = (EditText)FindViewById(Resource.Id.username);

			AppDataHelper.GetName();


			DocumentReference newPostRef = AppDataHelper.GetFirestore().Collection("users").Document();


			string postKey = newPostRef.Id;

        }

    }
}

