/*
    Ashley Thibodeau
    Interface Programming
    C20210201
    Code Exercise 03

    GitHub Repo: https://github.com/InterfaceProgramming/ce3-ThibodeauAshley-FS
 
 */
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace Thibodeau_Ashley_CE03
{
    public partial class App : Application
    {
        public static string FolderPath { get; private set; }
        public App()
        {
            InitializeComponent();
            FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
