using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System.Collections.Generic;
using ToDoList_Xamarin_Android.Models;

namespace ToDoList_Xamarin_Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ListView lstToDoList;
        List<tblToDo> myList = new List<tblToDo>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            lstToDoList = FindViewById<ListView>(Resource.Id.listView1);
            myList = DatabaseManager.ViewAll();
            lstToDoList.Adapter = new DataAdapter(this, myList);
           lstToDoList.ItemClick += OnLstToDoListClick;
        }

        //Adds Add to the Menu in the top right of your screen.
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.Add("Add");
            return base.OnPrepareOptionsMenu(menu);
        }
        void OnLstToDoListClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var ToDoItem = myList[e.Position];
            var edititem = new Intent(this, typeof(EditItem));
            edititem.PutExtra("Title", ToDoItem.Title);
            edititem.PutExtra("Details", ToDoItem.Details);
            edititem.PutExtra("ListID", ToDoItem.Id);

            StartActivity(edititem);
        }

        //When you choose Add from the Menu run the Add Activity. Good to know to add more options
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var itemTitle = item.TitleFormatted.ToString();

            switch (itemTitle)
            {
                case "Add":
                    StartActivity(typeof(AddItem));
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
        //Basically reload stuff when the app resumes operation after being paused
        protected override void OnResume()
        {
            base.OnResume();
            myList.Clear();
            myList = DatabaseManager.ViewAll();
            lstToDoList.Adapter = new DataAdapter(this, myList);
        }
   

    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}