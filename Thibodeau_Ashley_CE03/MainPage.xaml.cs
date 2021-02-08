using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;

using Thibodeau_Ashley_CE03.Models;

namespace Thibodeau_Ashley_CE03
{
    public partial class MainPage : ContentPage
    {
        private List<TaskData> taskList = new List<TaskData>();

        public MainPage()
        {
            InitializeComponent();
            addButton.Clicked += AddButton_Clicked;

            DataTemplate dt = new DataTemplate(typeof(TextCell));
            dt.SetBinding(TextCell.TextProperty, new Binding("Text"));
            dt.SetBinding(TextCell.DetailProperty, new Binding("Date"));
            dt.SetValue(TextCell.TextColorProperty, Color.Blue);
            listView.ItemTemplate = dt;

            listView.ItemSelected += ListView_ItemSelected;

        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem != null)
            {
                Navigation.PushAsync(new TaskEntryPage
                {
                    BindingContext = e.SelectedItem as TaskData
                });
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            taskList.Clear();

            var files = Directory.EnumerateFiles(App.FolderPath, "*.CE03.txt");
            foreach(var filename in files)
            {
                taskList.Add(new TaskData
                {
                    Filename = filename,
                    Text = File.ReadAllText(filename),
                    Date = File.GetCreationTime(filename)
                }) ;
            }

            listView.ItemsSource = taskList.OrderBy(d => d.Date).ToList();
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TaskEntryPage
            {
                BindingContext = new TaskData()
            });
        }
    }
}
