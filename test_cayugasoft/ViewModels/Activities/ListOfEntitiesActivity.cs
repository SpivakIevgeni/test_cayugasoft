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
using test_cayugasoft.ViewModels.Adapters;
using test_cayugasoft.ViewModels.SQLite;

namespace test_cayugasoft.ViewModels.Activities
{
    [Activity(Label = "List Of Entities")]
    public class ListOfEntitiesActivity : Activity
    {
        private List<SomeEntity> someEntities;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SomeDB someDB = new SomeDB(this);

            //receive SomeEntity Id
            Intent myIntent = this.Intent;
            int id = myIntent.GetIntExtra("Id", 0);
            
            //input test data to the table SomeEntities
            //someDB.EditSomeEntity(new SomeEntity() { Name = "One", Description = "This is the 1 entity", IsActive = true, Updated = DateTime.Now });
            //someDB.EditSomeEntity(new SomeEntity() { Name = "Two", Description = "This is the 2 entity", IsActive = false, Updated = DateTime.Now });
            //someDB.EditSomeEntity(new SomeEntity() { Name = "Three", Description = "This is the 3 entity", IsActive = true, Updated = DateTime.Now });
            //someDB.EditSomeEntity(new SomeEntity() { Name = "Four", Description = "This is the 4 entity", IsActive = false, Updated = DateTime.Now });
            //someDB.EditSomeEntity(new SomeEntity() { Name = "Five", Description = "This is the 5 entity", IsActive = true, Updated = DateTime.Now });
            //someDB.EditSomeEntity(new SomeEntity() { Name = "Six", Description = "This is the 6 entity", IsActive = true, Updated = DateTime.Now });

            SetContentView(Resource.Layout.SomeEntityList);
            ListView someEntityList = FindViewById<ListView>(Resource.Id.SomeEntityList);
            // populate the listview with data
            someEntities = someDB.SelectSomeEntityRecords();
            someEntityList.Adapter = new SomeEntityAdapter(this, someEntities,id);
            someEntityList.ItemClick += OnListItemClick;
           
            Button addSomeEntityBtn = FindViewById<Button>(Resource.Id.AddSomeEntityBtn);
            addSomeEntityBtn.Click += delegate
            {
                Intent intent = new Intent();
                intent.SetClass(this, typeof (SomeEntityActivity));
                StartActivity(intent);
            };
        }

        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var someEntity = someEntities[e.Position];
            Intent intent = new Intent();
            intent.SetClass(this, typeof(SomeEntityActivity));
            intent.PutExtra("Id", someEntity.Id);
            intent.PutExtra("Name", someEntity.Name);
            intent.PutExtra("Description", someEntity.Description);
            intent.PutExtra("IsAtive", someEntity.IsActive);
            StartActivity(intent);
        }
    }
}