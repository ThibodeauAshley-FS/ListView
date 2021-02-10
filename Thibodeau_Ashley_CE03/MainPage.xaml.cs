/*
    Ashley Thibodeau
    Interface Programming
    C20210201
    Code Exercise 03

    GitHub Repo: https://github.com/InterfaceProgramming/ce3-ThibodeauAshley-FS
 
 */
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
        private List<CharacterData> characterList = new List<CharacterData>();

        public MainPage()
        {
            InitializeComponent();
            addButton.Clicked += AddButton_Clicked;
            deleteAllButton.Clicked += DeleteAllButton_Clicked;

            DataTemplate dt = new DataTemplate(typeof(ImageCell));

            dt.SetBinding(ImageCell.ImageSourceProperty, new Binding("CharacterClassIMG"));
            dt.SetBinding(ImageCell.TextProperty, new Binding("CharacterNameText"));
            dt.SetBinding(ImageCell.DetailProperty, new Binding("Date"));
            dt.SetValue(ImageCell.TextColorProperty, Color.Blue);
            listView.ItemTemplate = dt;
            listView.ItemSelected += ListView_ItemSelected;

            DeleteAll_Button();

        }

        private void DeleteAllButton_Clicked(object sender, EventArgs e)
        {
            var file = Directory.EnumerateFiles(App.FolderPath, "*.CE03.txt");
            foreach (var filename in file)
            {
                characterList.Remove(new CharacterData
                {
                    Filename = filename,
                    CharacterNameText = File.ReadAllText(filename),
                    Date = File.GetCreationTime(filename)
                });
            }

        }



        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem != null)
            {
                Navigation.PushAsync(new CharacterEntryPage
                {
                    BindingContext = e.SelectedItem as CharacterData
                });
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            characterList.Clear();


            var files = Directory.EnumerateFiles(App.FolderPath, "*.CE03.txt");
            foreach(var filename in files)
            {
                string strData = File.ReadAllText(filename);
                string[] data = strData.Split(',');

                string data_name = data[0];
                string data_align = data[1];
                double.TryParse(data[2], out double data_level);
                string data_img = data[3];
                int.TryParse(data[4], out int data_class);


                characterList.Add(new CharacterData
                {
                    Filename = filename,
                    CharacterNameText= data_name,
                    CharacterAlignmentText = data_align,
                    CharacterLevel = data_level,
                    CharacterClass = data_class,  
                    Date = File.GetCreationTime(filename),

                }) ;

                
            }

            


            listView.ItemsSource = characterList.OrderBy(d => d.Date).ToList();

            DeleteAll_Button();
        }

        //send to new entry page
        private void AddButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CharacterEntryPage
            {
                BindingContext = new CharacterData()
            });
        }

        //Toggle Visibility
        private void DeleteAll_Button()
        {
            if (characterList.Count >= 1)
            {
                deleteAllButton.IsVisible = true;
            }
            else if (characterList.Count < 1)
            {
                deleteAllButton.IsVisible = false;
            }
        }
    }
}
