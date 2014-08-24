using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace test_cayugasoft.ViewModels.Activities
{
    [Activity(Label = "Contact")]
    public class ContactActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Contact);

            //receive contact data
            Intent myIntent = this.Intent;
            String[] phones = myIntent.GetStringArrayExtra("phones");
            String[] emails = myIntent.GetStringArrayExtra("emails");
            String contactName = myIntent.GetStringExtra("contactName");

            //put data to the view
            TextView contName = FindViewById<TextView>(Resource.Id.ContactName);
            contName.Text = contactName;
            //put phones
            LinearLayout contactsData = FindViewById<LinearLayout>(Resource.Id.ContactsData);
            for (int i = 0; i < phones.Length; i++)
            {
                Button btn= new Button(this);
                btn.SetBackgroundColor(Color.Transparent);
                btn.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.WrapContent);
                btn.Text = "Call "+phones[i];
                btn.Tag = phones[i];
                btn.Click += delegate
                {
                    Intent intent = new Intent(Intent.ActionCall);
                    intent.SetData(Android.Net.Uri.Parse("tel:" + btn.Tag));
                    StartActivity(intent);
                };
                contactsData.AddView(btn);
            }
            //put emails
            for (int i = 0; i < emails.Length; i++)
            {
                Button btn = new Button(this);
                btn.SetBackgroundColor(Color.Transparent);
                btn.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.WrapContent);
                btn.Text = "Send email to " + emails[i];
                btn.Tag = emails[i];
                btn.Click += delegate
                {
                    Intent intent = new Intent();
                    intent.SetClass(this, typeof(EmailActivity));
                    intent.PutExtra("email", btn.Tag.ToString());
                    intent.PutExtra("contactName", contactName);
                    StartActivity(intent);
                };
                contactsData.AddView(btn);
            }
        }
    }
}