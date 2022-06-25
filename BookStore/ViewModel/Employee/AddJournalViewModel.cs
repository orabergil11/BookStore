// ----------------------------------------------------------------------------------------------------- //
//                                                                                                       //
// @File      AddJournalViewModel.cs                                                                     //
// @Details   Responsible on adding new journals to stock                                                //
// @Author    Or Abergil                                                                                 //
// @Since     15/03/2022                                                                                 //
//                                                                                                       //
// ----------------------------------------------------------------------------------------------------- //

using BookStore.Views.EmployeeViews;
using BookStore.Models.Enums;
using BookStore.Server;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Linq;
using System;
using BookStore.Models;

namespace BookStore.ViewModel
{
    public class AddJournalViewModel : ViewModelBase
    {
        // Binded fields and properties
        private string editorName;
        private string title;
        private int issueNumber;
        private int quantity;
        private decimal price;
        private DateTime dateTimeSelection;
        private JournalFrequency selectedJournalFreq;
        private JournalGenre selectedJournalGenre;

        
        public string EditorName                    { get => editorName;             set => Set(ref editorName, value); }
        public string Title                         { get => title;                  set => Set(ref title, value); }
        public int Quantity                         { get => quantity;               set => Set(ref quantity, value); }
        public int IssueNumber                      { get => issueNumber;            set => Set(ref issueNumber, value); }
        public decimal Price                        { get => price;                  set => Set(ref price, value); }
        public DateTime DateTimeSelection           { get => dateTimeSelection;      set => Set(ref dateTimeSelection, value); }
        public JournalFrequency SelectedJournalFreq { get => selectedJournalFreq;    set => Set(ref selectedJournalFreq, value); }
        public JournalGenre SelectedJournalGenre    { get => selectedJournalGenre;   set => Set(ref selectedJournalGenre, value); }
        public ObservableCollection<JournalFrequency> JournalFrequencies { get; set; }
        public ObservableCollection<JournalGenre> JournalGenres          { get; set; }
        public RelayCommand AddProductCommand                            { get; set; }

        public AddJournalViewModel()
        {
            JournalFrequencies   = new ObservableCollection<JournalFrequency>(Enum.GetValues(typeof(JournalFrequency)).Cast<JournalFrequency>());
            JournalGenres        = new ObservableCollection<JournalGenre>(Enum.GetValues(typeof(JournalGenre)).Cast<JournalGenre>());
            AddProductCommand    = new RelayCommand(AddJournalToDb);
            DateTimeSelection    = DateTime.Now;
            MessengerInstance.Register<bool>(this, "resetFields", resetFields);
        }
        private void AddJournalToDb()
        {
            var journalToAdd = new Journal(EditorName, Title, IssueNumber, Quantity, DateTimeSelection, price, SelectedJournalFreq, SelectedJournalGenre);

            if (journalToAdd.IsValid())
            {
                if (ProductService.Instance.AddJournal(journalToAdd))
                {
                    MessengerInstance.Send<bool>(true, "resetFields");
                    MessengerInstance.Send<UserControl>(new Addedsuccessfully());
                    return;
                }
            }
            MessageBox.Show("Please Enter Valid Details!");
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
