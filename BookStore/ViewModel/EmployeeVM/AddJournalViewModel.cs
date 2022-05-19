using BookStore.Models.Enums;
using BookStore.Server;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;

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
        }

        private void AddJournalToDb()
        {
            ProductService.Instance.AddJournal(EditorName, Title, IssueNumber, Quantity, DateTimeSelection, Price, SelectedJournalFreq, SelectedJournalGenre);
        }
    }
}
