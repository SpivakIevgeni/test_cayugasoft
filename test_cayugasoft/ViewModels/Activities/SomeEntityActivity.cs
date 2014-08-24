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
using test_cayugasoft.Models;
using test_cayugasoft.ViewModels.SQLite;

namespace test_cayugasoft.ViewModels.Activities
{
    [Activity(Label = "Some Entity")]
    public class SomeEntityActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //receive SomeEntity data
            Intent myIntent = this.Intent;
            int id = myIntent.GetIntExtra("Id", 0);
            String name = myIntent.GetStringExtra("Name");
            String description = myIntent.GetStringExtra("Description");
            bool isAtive = myIntent.GetBooleanExtra("IsAtive", false);

            SetContentView(Resource.Layout.SomeEntity);
            //SomeEntity views
            TextView entityNameEt = FindViewById<TextView>(Resource.Id.EntityNameEt);
            entityNameEt.Text = name;
            TextView descriptionEt = FindViewById<TextView>(Resource.Id.DescriptionEt);
            descriptionEt.Text = description;
            CheckBox isActiveRb = FindViewById<CheckBox>(Resource.Id.IsActiveCb);
            isActiveRb.Checked = isAtive;


            Button saveSomeEntityBtn = FindViewById<Button>(Resource.Id.SaveSomeEntityBtn);
            saveSomeEntityBtn.Click += delegate
            {
                if (!String.IsNullOrEmpty(entityNameEt.Text) && !String.IsNullOrEmpty(descriptionEt.Text))
                {
                    if (id == 0)
                    {
                        SomeDB someDB = new SomeDB(this);
                        someDB.EditSomeEntity(new SomeEntity()
                        {
                            Name = entityNameEt.Text,
                            Description = descriptionEt.Text,
                            IsActive = isActiveRb.Checked,
                            Updated = DateTime.Now
                        });
                        id = someDB.GetEntityLastId();
                    }
                    else
                    {
                        SomeDB someDB = new SomeDB(this);
                        someDB.EditSomeEntity(new SomeEntity()
                        {
                            Id = id,
                            Name = entityNameEt.Text,
                            Description = descriptionEt.Text,
                            IsActive = isActiveRb.Checked,
                            Updated = DateTime.Now
                        });
                    }
                    Intent intent = new Intent();
                    intent.SetClass(this, typeof(ListOfEntitiesActivity));
                    intent.PutExtra("Id", id);
                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(this, "One or more fields are empty.", ToastLength.Short).Show();
                }
            };
        }
    }
}