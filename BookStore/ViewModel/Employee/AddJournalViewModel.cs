using BookStore.Models.Enums;
using BookStore.Server;
using BookStore.Views.EmployeeViews;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BookStore.ViewModel
{
    public class AddJournalViewModel : ViewModelBase
    {
        private string editorName;
        private string title;
        private int issueNumber;
        private int quantity;
        private decimal price;
        private DateTime dateTimeSelection;
        private JournalFrequency selectedJournalFreq;
        private JournalGenre selectedJournalGenre;

        
        public string EditorName                    { get => editorName; set => Set(ref editorName, value); }
        public string Title                         { get => title; set => Set(ref title, value); }
        public int Quantity                         { get => quantity; set => Set(ref quantity, value); }
        public int IssueNumber                      { get => issueNumber; set => Set(ref issueNumber, value); }
        public decimal Price                        { get => price; set => Set(ref price, value); }
        public DateTime DateTimeSelection           { get => dateTimeSelection; set => Set(ref dateTimeSelection, value); }
        public RelayCommand AddProductCommand       { get; set; }
        public JournalFrequency SelectedJournalFreq { get => selectedJournalFreq; set => Set(ref selectedJournalFreq, value); }
        public JournalGenre SelectedJournalGenre    { get => selectedJournalGenre; set => Set(ref selectedJournalGenre, value); }
        public ObservableCollection<JournalFrequency> JournalFrequencies { get; set; }
        public ObservableCollection<JournalGenre> JournalGenres { get; set; }

        public AddJournalViewModel()
        {
            AddProductCommand = new RelayCommand(AddJournalToDb);
            JournalFrequencies = new ObservableCollection<JournalFrequency>(Enum.GetValues(typeof(JournalFrequency)).Cast<JournalFrequency>());
            JournalGenres = new ObservableCollection<JournalGenre>(Enum.GetValues(typeof(JournalGenre)).Cast<JournalGenre>());
            MessengerInstance.Register<bool>(this, "resetFields", resetFields);
            DateTimeSelection = DateTime.Now;
        }

        private void AddJournalToDb()
        {
            if (false == ProductService.Instance.AddJournal(EditorName, Title, IssueNumber, Quantity, DateTimeSelection, Price, SelectedJournalFreq, SelectedJournalGenre))
            {
                MessageBox.Show("Please Enter Valid Details!");
                return;
            }
            
            else
            {
                MessengerInstance.Send<bool>(true, "resetFields");
                MessengerInstance.Send<UserControl>(new Addedsuccessfully());
            }
        }
        private void resetFields(bool obj)
        {
            EditorName = default;
            Title = default;
            Quantity = default;
            IssueNumber = default;
            Price = default;
            DateTimeSelection = default;
            SelectedJournalFreq = default;
            SelectedJournalGenre = default;
        }
    }
}
