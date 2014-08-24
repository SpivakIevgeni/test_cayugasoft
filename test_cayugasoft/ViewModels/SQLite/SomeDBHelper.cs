using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;
using Android.Database.Sqlite;
using Android.Util;

namespace test_cayugasoft.ViewModels.SQLite
{
    public class SomeDBHelper : SQLiteOpenHelper
    {
        private const String DATABASE_NAME = "SomeDB";

        private const int DATABASE_VERSION = 2;

        private const String DATABASE_CREATE = "create table SomeEntities (" +
                                               "Id integer primary key autoincrement," +
                                               "Name text not null," +
                                               "Description text not null," +
                                               "IsActive integer not null," +
                                               "Updated text not null)";

        public SomeDBHelper(Context context)
            : base(context, DATABASE_NAME, null, DATABASE_VERSION) { }

        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL(DATABASE_CREATE);
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            Log.WriteLine(LogPriority.Info, null,
                "Upgrading database from version " + oldVersion + " to " + newVersion +
                " ,wich will destroy all old data");
            db.ExecSQL("DROP TABLE IF EXISTS SomeEntities");
            OnCreate(db);
        }
    }
}
