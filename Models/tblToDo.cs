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
using SQLite;

namespace ToDoList_Xamarin_Android.Models
{
    public class tblToDo
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Details { get; set; }
        public DateTime Date { get; set; }
    }
}