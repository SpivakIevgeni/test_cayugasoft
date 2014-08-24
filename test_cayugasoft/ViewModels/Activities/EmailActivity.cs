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

namespace test_cayugasoft.ViewModels.Activities
{
    [Activity(Label = "Send Email")]
    public class EmailActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //receive email data
            Intent myIntent = this.Intent;
            String email = myIntent.GetStringExtra("email");
            String contactName = myIntent.GetStringExtra("contactName");

            SetContentView(Resource.Layout.Email);

            //put data to the view
            TextView subjectEt = FindViewById<TextView>(Resource.Id.SubjectEt);
            subjectEt.Text = "Hello, " + contactName;
            TextView bodyEt = FindViewById<TextView>(Resource.Id.BodyEt);
            bodyEt.Text = "Dear, " + contactName;
            Button sendBtn = FindViewById<Button>(Resource.Id.SendBtn);
            //sending emails
            sendBtn.Click += delegate
            {
                Intent emailIntent = new Intent(Intent.ActionSendto, Android.Net.Uri.FromParts("mailto",email, null));
                emailIntent.SetType("message/rfc822");
                emailIntent.PutExtra(Intent.ExtraText, bodyEt.Text);
                emailIntent.PutExtra(Intent.ExtraSubject, subjectEt.Text);
                try
                {
                    StartActivity(Intent.CreateChooser(emailIntent, "Send mail..."));
                }
                catch (Android.Content.ActivityNotFoundException ex)
                {
                    Toast.MakeText(this, "There are no email clients installed.", ToastLength.Short).Show();
                }
            };

        }
    }
}