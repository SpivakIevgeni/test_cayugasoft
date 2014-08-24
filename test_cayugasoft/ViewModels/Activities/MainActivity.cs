using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace test_cayugasoft.ViewModels.Activities
{
    [Activity(Label = "test_cayugasoft", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            
            //List of Contacts Tab
            Button listOfContactsBtn = FindViewById<Button>(Resource.Id.ListOfContactsBtn);
            listOfContactsBtn.Click += delegate
            {
                Intent myIntent = new Intent();
                myIntent.SetClass(this, typeof(ListOfContactsActivity));
                StartActivity(myIntent);
            };

            //Gallery Tab
            Button galleryBtn = FindViewById<Button>(Resource.Id.GalleryBtn);
            galleryBtn.Click += delegate
            {
                Intent myIntent = new Intent();
                myIntent.SetClass(this, typeof(GalleryActivity));
                StartActivity(myIntent);
            };

            //List of Entities Tab
            Button listOfEntitiesBtn = FindViewById<Button>(Resource.Id.ListOfEntitiesBtn);
            listOfEntitiesBtn.Click += delegate
            {
                Intent myIntent = new Intent();
                myIntent.SetClass(this, typeof(ListOfEntitiesActivity));
                StartActivity(myIntent);
            };

            //Image Container Tab
            Button imageContainerBtn = FindViewById<Button>(Resource.Id.ImageContainerBtn);
            imageContainerBtn.Click += delegate
            {
                Intent myIntent = new Intent();
                myIntent.SetClass(this, typeof(ImageContainerActivity));
                StartActivity(myIntent);
            };
        }
    }
}

