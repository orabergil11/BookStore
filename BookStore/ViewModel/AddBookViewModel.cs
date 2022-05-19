using BookStore.Models.Enums;
using BookStore.Server;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.ViewModel
{
    public class AddBookViewModel : ViewModelBase
    {
        private string    authorName;
        private string    title;
        private int       eddition;
        private int       quantity;
        private decimal   price;
        private BookGenre selectedBookGenre;
        private DateTime  dateTimeSelection;

        public string AuthorName           { get => authorName; set => Set(ref authorName, value); }
        public string Title                { get => title; set => Set(ref title, value); }
        public int Quantity                { get => quantity; set => Set(ref quantity, value); }
        public int Eddition                { get => eddition; set => Set(ref eddition, value); }
        public decimal Price               { get => price; set => Set(ref price, value); }
        public DateTime DateTimeSelection  { get => dateTimeSelection; set => Set(ref dateTimeSelection, value); }
        public BookGenre SelectedBookGenre { get => selectedBookGenre; set => Set(ref selectedBookGenre, value); }
        public RelayCommand AddProductCommand { get; set; }
        public ObservableCollection<BookGenre> BookGenres { get; set; }

        public AddBookViewModel()
        {
            AddProductCommand = new RelayCommand(AddBookToDb);
            BookGenres = new ObservableCollection<BookGenre>(Enum.GetValues(typeof(BookGenre)).Cast<BookGenre>());
        }

        private void AddBookToDb() => ProductService.Instance.AddBook(AuthorName, Title, Quantity, DateTimeSelection, Price, Eddition, SelectedBookGenre);
    }
}
