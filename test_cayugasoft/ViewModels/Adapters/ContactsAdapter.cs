using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Database;
using Android.Provider;
using Android.Views;
using Android.Widget;
using test_cayugasoft.Models;

namespace test_cayugasoft.ViewModels.Adapters
{
    public class ContactsAdapter : BaseAdapter<Contact>
    {
        Activity activity;
        List<Contact> contactList;
        public ContactsAdapter(Activity activity, List<Contact> items)
        {
            this.activity = activity;
            this.contactList = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Contact this[int position]
        {
            get { return contactList[position]; }
        }

        public override int Count
        {
            get { return contactList.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.ContactListItem, parent, false);
            var contactName = view.FindViewById<TextView>(Resource.Id.ContactTv);
            contactName.Text = contactList[position].DisplayName;
            return view;
        }
    }
}
