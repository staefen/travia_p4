﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace P4Travia.Activities
{
	[Activity(Label = "NotificationActivity")]
	public class NotificationActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// SetExploreButton();

			SetContentView(Resource.Layout.bottom_nav_bar);

			TextView location_settings = (TextView)FindViewById(Resource.Id.location);
			location_settings.Click += Location_Settings_Click;

			ImageView notification = (ImageView)FindViewById(Resource.Id.notification_bell);
			notification.Click += Notification_Click;
		}
		/*
		//omdirigere fra notification til homepage
		public void SetExploreButton()
		{
			GoToHomepage();
		}
		
		private void GoToHomepage()
		{
			Button homepage = (Button)FindViewById(Resource.Id.explore_button);
			homepage.Click += Homepage_Click;
		}

		private void Homepage_Click(object sender, EventArgs e)
		{
			StartActivity(typeof(MainActivity));//her skal MainActivity indsættes
		}

		*/



		//Clicked buttons
		private void Notification_Click(object sender, EventArgs e)
		{
			StartActivity(typeof(NotificationActivity));
		}

		private void Location_Settings_Click(object sender, EventArgs e)
		{
			StartActivity(typeof(LocationActivity));
		}
	}
}