using BookStore.Models.Enums;
using BookStore.Server;
using BookStore.Views.EmployeeViews;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BookStore.ViewModel
{
    public class AddBookViewModel : ViewModelBase
    {
        #region Fields
        private string    authorName;
        private string    title;
        private int       eddition;
        private int       quantity;
        private decimal   price;
        private BookGenre selectedBookGenre;
        private DateTime  dateTimeSelection;
        #endregion
        #region Properties
        public string AuthorName           { get => authorName; set => Set(ref authorName, value); }
        public string Title                { get => title; set => Set(ref title, value); }
        public int Quantity                { get => quantity; set => Set(ref quantity, value); }
        public int Eddition                { get => eddition; set => Set(ref eddition, value); }
        public decimal Price               { get => price; set => Set(ref price, value); }
        public DateTime DateTimeSelection  { get => dateTimeSelection; set => Set(ref dateTimeSelection, value); }
        public BookGenre SelectedBookGenre { get => selectedBookGenre; set => Set(ref selectedBookGenre, value); }
        public RelayCommand AddProductCommand { get; set; }
        public ObservableCollection<BookGenre> BookGenres { get; set; }
        #endregion
        #region c'tor
        public AddBookViewModel()
        {
            AddProductCommand = new RelayCommand(AddBookToDb);
            BookGenres = new ObservableCollection<BookGenre>(Enum.GetValues(typeof(BookGenre)).Cast<BookGenre>());
            MessengerInstance.Register<bool>(this, "resetFields", resetFields);
            DateTimeSelection = DateTime.Now;
        }

        #endregion
        #region Functions
        private void AddBookToDb()
        {
            if (false == ProductService.Instance.AddBook(AuthorName, Title, Quantity, DateTimeSelection, Price, SelectedBookGenre, Eddition))
            MessageBox.Show("Please Enter Valid Details!");
            else
            {
                MessengerInstance.Send<bool>(true, "resetFields");
                MessengerInstance.Send<UserControl>(new Addedsuccessfully());
            }
        }
        private void resetFields(bool obj)
        {
            AuthorName = default;
            Title = default;
            Quantity = default;
            Eddition = default;
            SelectedBookGenre = default;
            Price = default;
            DateTimeSelection = default;
            BookGenres = default;
        }

        #endregion
    }
}
