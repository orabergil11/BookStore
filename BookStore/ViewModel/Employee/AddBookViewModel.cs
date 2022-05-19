using BookStore.Models.Enums;
using BookStore.Server;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;

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
        }

        #endregion
        #region Functions
        private void AddBookToDb() => ProductService.Instance.AddBook(AuthorName, Title, Quantity, DateTimeSelection, Price, SelectedBookGenre, Eddition );

        #endregion
    }
}
