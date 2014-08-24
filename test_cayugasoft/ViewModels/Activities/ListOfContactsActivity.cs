using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using test_cayugasoft.Models;
using test_cayugasoft.ViewModels.Adapters;

namespace test_cayugasoft.ViewModels.Activities
{
    [Activity(Label = "List of contacts")]
    public class ListOfContactsActivity : Activity
    {
        List<Contact> contactList;
        List<Contact> contactListTmp;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ListOfContacts);

            //Contacts ListView
            ListView listView = FindViewById<ListView>(Resource.Id.List); 
            // populate the listview with data
            FillContacts();
            contactListTmp = contactList;
            listView.Adapter = new ContactsAdapter(this,contactList);
            listView.ItemClick += OnListItemClick;

            //Searching Contacts
            EditText searchEt = FindViewById<EditText>(Resource.Id.SearchEt);
            searchEt.TextChanged += delegate
            {
                if (String.IsNullOrEmpty(searchEt.Text))
                {
                    contactListTmp = contactList;
                    listView.Adapter = new ContactsAdapter(this, contactList);
                    listView.ItemClick += OnListItemClick;
                }
                else
                {
                    contactListTmp = contactList.Where(item => item.DisplayName.ToUpper().Contains(searchEt.Text.ToUpper())).ToList();
                    listView.Adapter = new ContactsAdapter(this, contactListTmp);
                    listView.ItemClick += OnListItemClick;
                }
                
            };
        }

        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var contact = contactListTmp[e.Position];
            //   Android.Widget.Toast.MakeText(this, contact.Id.ToString(), Android.Widget.ToastLength.Short).Show();

            Intent myIntent = new Intent();
            myIntent.SetClass(this, typeof(ContactActivity));
            myIntent.PutExtra("contactName", contact.DisplayName);
            myIntent.PutExtra("phones", FindContactPhones(contact.Id));
            myIntent.PutExtra("emails", FindContactEmails(contact.Id));
            StartActivity(myIntent);
        }

        private void FillContacts()
        {
            var uri = ContactsContract.Contacts.ContentUri;
            contactList = new List<Contact>();
            string[] projection = {
                ContactsContract.Contacts.InterfaceConsts.Id,
                ContactsContract.Contacts.InterfaceConsts.DisplayName
            };

            //Run after API11
            var loader = new CursorLoader(this, uri, projection, null, null, null);
            var cursor = (ICursor)loader.LoadInBackground();

            if (cursor.MoveToFirst())
            {
                do
                {
                    contactList.Add(new Contact
                    {
                        Id = cursor.GetLong(cursor.GetColumnIndex(projection[0])),
                        DisplayName = cursor.GetString(cursor.GetColumnIndex(projection[1]))
                    });
                } while (cursor.MoveToNext());
            }
            cursor.Close();
            contactList = contactList.OrderBy(item => item.DisplayName).ToList();
        }

        private string[] FindContactPhones(long contactId)
        {
            var uri = ContactsContract.CommonDataKinds.Phone.ContentUri;
            List<string> phones = new List<string>();
            contactList = new List<Contact>();
            string[] projection = {
                ContactsContract.CommonDataKinds.Phone.Number
            };
            string[] selectionArgs = { contactId.ToString() };

            CursorLoader loader = new CursorLoader(this, uri, projection, "contact_id = ?", selectionArgs, null);
            var cursor = (ICursor)loader.LoadInBackground();

            if (cursor.MoveToFirst())
            {
                do
                {
                    phones.Add(cursor.GetString(cursor.GetColumnIndex(projection[0])));
                } while (cursor.MoveToNext());
            }
            cursor.Close();

            return phones.ToArray();
        }

        private string[] FindContactEmails(long contactId)
        {
            var uri = ContactsContract.CommonDataKinds.Email.ContentUri;
            List<string> phones = new List<string>();
            contactList = new List<Contact>();
            string[] projection = {
                ContactsContract.CommonDataKinds.Email.Address
            };
            string[] selectionArgs = { contactId.ToString() };

            CursorLoader loader = new CursorLoader(this, uri, projection, "contact_id = ?", selectionArgs, null);
            var cursor = (ICursor)loader.LoadInBackground();

            if (cursor.MoveToFirst())
            {
                do
                {
                    phones.Add(cursor.GetString(cursor.GetColumnIndex(projection[0])));
                } while (cursor.MoveToNext());
            }
            cursor.Close();

            return phones.ToArray();
        }
    }
}