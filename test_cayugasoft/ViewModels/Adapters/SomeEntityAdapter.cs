using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using test_cayugasoft.Models;

namespace test_cayugasoft.ViewModels.Adapters
{
    public class SomeEntityAdapter : BaseAdapter<SomeEntity>
    {
        Activity activity;
        List<SomeEntity> someEntityList;
        private int highlightId=0;

        public SomeEntityAdapter(Activity activity, List<SomeEntity> items, int hlId=0)
        {
            this.activity = activity;
            this.someEntityList = items;
            highlightId = hlId;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override SomeEntity this[int position]
        {
            get { return someEntityList[position]; }
        }

        public override int Count
        {
            get { return someEntityList.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.SomeEntityListItem, parent, false);
            var entityNameTv = view.FindViewById<TextView>(Resource.Id.EntityNameTv);
            entityNameTv.Text = someEntityList[position].Name;
            //set view background(blue - when add/update SomeEntity,green - when SomeEntity IsActive,transparent - other)
            if (highlightId != 0 && someEntityList[position].Id == highlightId)
            {
                view.SetBackgroundColor(Color.Blue);
            } else if (someEntityList[position].IsActive)
            {
                view.SetBackgroundColor(Color.Green);
            }
            else
            {
                view.SetBackgroundColor(Color.Transparent);
            }
            var entityUpdatedTv = view.FindViewById<TextView>(Resource.Id.EntityUpdatedTv);
            entityUpdatedTv.Text = someEntityList[position].Updated.ToString();
            return view;
        }
    }
}
