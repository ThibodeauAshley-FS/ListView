/*
    Ashley Thibodeau
    Interface Programming
    C20210201
    Code Exercise 03

    GitHub Repo: https://github.com/InterfaceProgramming/ce3-ThibodeauAshley-FS
 
 */
using System;
using System.Collections.Generic;
using Thibodeau_Ashley_CE03.Models;
using System.IO;

using Xamarin.Forms;

namespace Thibodeau_Ashley_CE03
{
    public partial class CharacterEntryPage : ContentPage
    {
        public CharacterEntryPage()
        {
            InitializeComponent();

            classPicker.Items.Add("Priest");
            classPicker.Items.Add("Warrior");
            classPicker.Items.Add("Mage");
            classPicker.Items.Add("Rogue");

            saveButton.ImageSource = ImageSource.FromFile("save48.png");
            deleteButton.ImageSource = ImageSource.FromFile("delete48.png");

            //Events
            classPicker.SelectedIndexChanged += ClassPicker_SelectedIndexChanged;
            saveButton.Clicked += SaveButton_Clicked;
            deleteButton.Clicked += DeleteButton_Clicked;
            levelStepper.ValueChanged += LevelStepper_ValueChanged;
        }

        private void ClassPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = classPicker.SelectedIndex;

            if (selectedIndex != -1)
            {
                classPickerImage.Source = ImageSource.FromFile(SIImageFile(selectedIndex));

            }


        }

        private string SIImageFile(int selectedIndex)
        {
            string imgOut = null;

            if (selectedIndex == 0)
            {
                imgOut = "priest.png";

            }
            else if (selectedIndex == 1)
            {
                 imgOut = "warrior.png";

            }
            else if (selectedIndex == 2)
            {
                 imgOut = "mage.png";

            }
            else if (selectedIndex == 3)
            {
                imgOut = "rogue.png";

            }

            return imgOut;

        }

        private void LevelStepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
                levelLabel.Text = "Level: " + levelStepper.Value.ToString();
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var characterName = BindingContext as CharacterData;
            var characterAlignment = BindingContext as CharacterData;
            var characterLevel = BindingContext as CharacterData;
            var characterClassIMG = BindingContext as CharacterData;
            var characterClass = BindingContext as CharacterData;
            
            if (!string.IsNullOrWhiteSpace(characterName.CharacterNameText))
            {
                string[] data = characterName.CharacterNameText.Split(',');

                characterName.CharacterNameText = data[0];

                nameEntry.Text = characterName.CharacterNameText;
                alignmentEntry.Text = characterAlignment.CharacterAlignmentText;
                levelStepper.Value = characterLevel.CharacterLevel;
                classPickerImage.Source = characterClassIMG.CharacterClassIMG;
                classPicker.SelectedIndex = characterClass.CharacterClass;
            }
            

        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var CharFile = (CharacterData)BindingContext;
            if(File.Exists(CharFile.Filename))
            {
                File.Delete(CharFile.Filename);
            }

            Navigation.PopAsync();
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            //Bind var to text entry's
            var characterName = (CharacterData)BindingContext;
            characterName.CharacterNameText = nameEntry.Text;

            var characterAlignment = (CharacterData)BindingContext;
            characterAlignment.CharacterAlignmentText = alignmentEntry.Text;

            var characterClassIMG = (CharacterData)BindingContext;
            characterClassIMG.CharacterClassIMG = SIImageFile(classPicker.SelectedIndex);

            var characterClass = (CharacterData)BindingContext;
            characterClass.CharacterClass = classPicker.SelectedIndex;

            if(!string.IsNullOrWhiteSpace(characterName.CharacterNameText))
            {

                if (string.IsNullOrWhiteSpace(characterName.Filename))
                {
                    //New
                    var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.CE03.txt");

                    File.WriteAllText(filename, $"{ characterName.CharacterNameText},{characterAlignment.CharacterAlignmentText},{levelStepper.Value.ToString()},{characterClassIMG.CharacterClassIMG},{characterClass.CharacterClass.ToString()}");
                }
                else
                {
                    //Update
                    File.WriteAllText(characterName.Filename, $"{characterName.CharacterNameText},{characterAlignment.CharacterAlignmentText},{levelStepper.Value.ToString()},{characterClassIMG.CharacterClassIMG},{characterClass.CharacterClass.ToString()}");
                }
                Navigation.PopAsync();
            }
            else
            {
                errorLabel.IsVisible = true;
                errorLabel.Text = "Please Enter Name to Save";
            }





        }
    }
}
