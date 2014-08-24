using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using test_cayugasoft.Models;

namespace test_cayugasoft.ViewModels.SQLite
{
    public class SomeDB
    {
        private SomeDBHelper dbHelper;

        private SQLiteDatabase db;

        public SomeDB(Context context)
        {
            dbHelper= new SomeDBHelper(context);
            db = dbHelper.WritableDatabase;
        }

        //Get the last SomeEntity Id
        public int GetEntityLastId()
        {
            string[] cols = new string[] { "Id"};
            List<SomeEntity> someEntities = new List<SomeEntity>();
            ICursor cursor = db.Query(true, "SomeEntities", cols, null, null, null, null, null, null);
            if (cursor.MoveToFirst())
            {
                do
                {
                    someEntities.Add(new SomeEntity()
                    {
                        Id = cursor.GetInt(cursor.GetColumnIndex(cols[0]))
                    });
                } while (cursor.MoveToNext());
            }
            cursor.Close();
            return someEntities.Max(element => element.Id);
        }

        //Add(when Id<=0) or Update(when Id>0) SomeEntity record
        public void EditSomeEntity(SomeEntity someEntity)
        {
            if (someEntity != null)
            {
                ContentValues values = new ContentValues();
                values.Put("Name", someEntity.Name);
                values.Put("Description", someEntity.Description);
                values.Put("IsActive", someEntity.IsActive);
                values.Put("Updated", someEntity.Updated.ToString());
                if (someEntity.Id > 0)
                {
                    db.Update("SomeEntities", values, "Id="+someEntity.Id, null);
                }
                else
                {
                    db.Insert("SomeEntities", null, values);
                }
                
            }
        }

        //Select all data from SomeEntities table, return list of SomeEntity
        public List<SomeEntity> SelectSomeEntityRecords()
        {
            string[] cols = new string[] { "Id", "Name", "Description", "IsActive", "Updated" };
            List<SomeEntity> someEntities= new List<SomeEntity>();
            ICursor cursor = db.Query(true, "SomeEntities", cols, null, null, null, null, null, null);
            if (cursor.MoveToFirst())
            {
                do
                {
                    someEntities.Add(new SomeEntity()
                    {
                        Id = cursor.GetInt(cursor.GetColumnIndex(cols[0])),
                        Name = cursor.GetString(cursor.GetColumnIndex(cols[1])),
                        Description = cursor.GetString(cursor.GetColumnIndex(cols[2])),
                        IsActive = Convert.ToBoolean(cursor.GetInt(cursor.GetColumnIndex(cols[3]))),
                        Updated = Convert.ToDateTime(cursor.GetString(cursor.GetColumnIndex(cols[4])))
                    });
                } while (cursor.MoveToNext());
            }
            cursor.Close();
            return someEntities;
        }
    }
}
