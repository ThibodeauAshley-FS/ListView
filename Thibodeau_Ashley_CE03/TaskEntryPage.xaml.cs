using System;
using System.Collections.Generic;
using Thibodeau_Ashley_CE03.Models;
using System.IO;

using Xamarin.Forms;

namespace Thibodeau_Ashley_CE03
{
    public partial class TaskEntryPage : ContentPage
    {
        public TaskEntryPage()
        {
            InitializeComponent();

            saveButton.ImageSource = ImageSource.FromFile("save48.png");
            deleteButton.ImageSource = ImageSource.FromFile("delete48.png");

            saveButton.Clicked += SaveButton_Clicked;
            deleteButton.Clicked += DeleteButton_Clicked;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var task = BindingContext as TaskData;
            if(!string.IsNullOrWhiteSpace(task.Text))
            {
                taskEditor.Text = task.Text;
            }
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var task = (TaskData)BindingContext;
            if(File.Exists(task.Filename))
            {
                File.Delete(task.Filename);
            }

            Navigation.PopAsync();
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            var task = (TaskData)BindingContext;
            task.Text = taskEditor.Text;

            if(string.IsNullOrWhiteSpace(task.Filename))
            {
                //New
                var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.CE03.txt");
                File.WriteAllText(filename, task.Text);
            }
            else
            {
                //Update
                File.WriteAllText(task.Filename, task.Text);
            }

            Navigation.PopAsync();
        }
    }
}
