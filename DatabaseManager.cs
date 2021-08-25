using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SQLite;
using ToDoList_Xamarin_Android.Models;

namespace ToDoList_Xamarin_Android
{
    public static class DatabaseManager
    {
        //YOUR CLASS NAME MUST BE YOUR TABLE NAME
        public static SQLiteConnection db;
        public static string databasePath;
        public static string databaseName;

        static DatabaseManager()
        {
            //Using Xamarin Essentials to set the path. AppdataDirectory gets backed up by Android
            databaseName = "ToDoList.db";
            databasePath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, databaseName);

        
                db = new SQLiteConnection(databasePath);

               // Console.WriteLine("******Done");
        
                //create a new instance of the db, if it doesn't exist create it and all the tables
                db.CreateTable<tblToDo>();
              
          
        }

        public static List<tblToDo> ViewAll()
        {
            try
            {
                // var cmd = db.Query<tblToDo>("select * from tblToDo");
                // Console.WriteLine(cmd);

                return db.Table<tblToDo>().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);

                //making some fake items to stop the system from crashing when the DB doesn't connect
                List<tblToDo> fakeitem = new List<tblToDo>();
                //make a single item
                tblToDo item = new tblToDo();
                item.Id = 100;
                item.Date = DateTime.Now.Date;
                item.Details = "There are no items";
                item.Title = "No Items";
                fakeitem.AddRange(new[] { item }); //add it to the fake item list
                return fakeitem;
            }
        }
        public static void AddItem(string title, string details)
        {
            try
            {
                var addThis = new tblToDo() { Title = title, Details = details };
                db.Insert(addThis);
            }
            catch (Exception e)
            {
                Console.WriteLine("Add Error:" + e.Message);
            }
        }

        public static void EditItem(string title, string details, int listid)
        {
            try
            {
                // http://stackoverflow.com/questions/14007891/how-are-sqlite-records-updated 
                var EditThis = new tblToDo() { Title = title, Details = details, Id = listid };
                db.Update(EditThis);
                //or this
                //   db.Execute("UPDATE tblToDoList Set Title = ?, Details =, WHERE ID = ?", title, details, listid);

            }
            catch (Exception e)
            {
                Console.WriteLine("Update Error:" + e.Message);
            }
        }

        public static void DeleteItem(int listid)
        {
            // https://developer.xamarin.com/guides/cross-platform/application_fundamentals/data/part_3_using_sqlite_orm/ 
            try
            {
                db.Delete<tblToDo>(listid);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete Error:" + ex.Message);
            }
        }
    }


}
