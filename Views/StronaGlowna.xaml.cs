using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;


namespace LosowanieOU.Views
{
    public partial class StronaGlowna : ContentPage
    {
        private List<string> peopleList = new List<string>();
        private string filePath = @"C:\Users\Testo\source\repos\LosowanieOU\Resources\Files\PeopleList.txt";

        public StronaGlowna()
        {
            InitializeComponent();
            LoadPeopleList(); // Load list on startup
        }

        private void LoadPeopleList()
        {
            // Load people list from file
            if (File.Exists(filePath))
                peopleList = File.ReadAllLines(filePath).ToList();
            peopleListView.ItemsSource = peopleList;
        }

        private async void SavePeopleList()
        {
            // Save people list to file
            try
            {

                await File.WriteAllLinesAsync(filePath, peopleList);

            }

            catch (Exception ex)
            {
                Console.WriteLine($"Wystπpi≥ b≥πd podczas zapisu pliku: {ex.Message}");
            }
        }

        private bool WrongSymbol(string text)
        {
            string pattern = @"^[A-Za-zπÊÍ≥ÒÛúüø•∆ £—”åèØ\s]+$"; // Only letters and spaces
            return Regex.IsMatch(text, pattern); // Check if it's okay
        }

        private void AddPerson_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                DisplayAlert("B≥πd", "Pole jest puste", "OK");
            }
            else
            {

                string name = txtName.Text.Trim();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    if (WrongSymbol(name))
                    {
                        peopleList.Add(name);
                        SavePeopleList();

                        // Odúwieø ürÛd≥o danych dla ListView
                        peopleListView.ItemsSource = null;
                        peopleListView.ItemsSource = peopleList;
                    }
                    else
                    {
                        DisplayAlert("B≥πd", "ImiÍ i nazwisko zawierajπ niedozwolone znaki", "OK");
                    }

                }
            } 
        }
   

    private void PickRandomPerson_Clicked(object sender, EventArgs e)
        {
            // Pick a random person from the list
            if (peopleList.Any())
            {
                Random rand = new Random();
                string randomPerson = peopleList[rand.Next(0, peopleList.Count)];
                DisplayAlert("Wylosowana osoba", randomPerson, "OK");
            }
            else
            {
                DisplayAlert("B≥πd", "Lista uczniÛw jest pusta.", "OK");
            }
        }

        
        private void peopleListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            string item = (string)e.SelectedItem;
            peopleList.Remove(item);
            
            SavePeopleList();

            // Odúwieø ürÛd≥o danych dla ListView
                peopleListView.ItemsSource = null;
                peopleListView.ItemsSource = peopleList;

        }
    }
}
