// ----------------------------------------------------------------------------------------------------- //
//                                                                                                       //
// @File      AddBookViewModel.cs                                                                        //
// @Details   Responsible on adding new books to stock                                                   //
// @Author    Or Abergil                                                                                 //
// @Since     15/03/2022                                                                                 //
//                                                                                                       //
// ----------------------------------------------------------------------------------------------------- //

using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using System.Linq;
using System;

using BookStore.Views.EmployeeViews;
using BookStore.Models.Enums;
using BookStore.Server;
using BookStore.Models;

using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;

namespace BookStore.ViewModel
{
    public class AddBookViewModel : ViewModelBase
    {
        //Binded fields

        #region Fields
        private string    authorName;
        private string    title;
        private int       eddition;
        private int       quantity;
        private decimal   price;
        private BookGenre selectedBookGenre;
        private DateTime  dateTimeSelection;


        #endregion
        #region FullProperties
        public string AuthorName           { get => authorName;          set => Set(ref authorName, value); }
        public string Title                { get => title;               set => Set(ref title, value); }
        public int Quantity                { get => quantity;            set => Set(ref quantity, value); }
        public int Eddition                { get => eddition;            set => Set(ref eddition, value); }
        public decimal Price               { get => price;               set => Set(ref price, value); }
        public DateTime DateTimeSelection  { get => dateTimeSelection;   set => Set(ref dateTimeSelection, value); }
        public BookGenre SelectedBookGenre { get => selectedBookGenre;   set => Set(ref selectedBookGenre, value); }
        public ObservableCollection<BookGenre> BookGenres { get; set; }

        //Commands
        public RelayCommand AddProductCommand             { get; set; }

        #endregion
        #region c'tor
        public AddBookViewModel()
        {
            DateTimeSelection   = DateTime.Now;
            AddProductCommand   = new RelayCommand(AddBookToDb);
            BookGenres          = new ObservableCollection<BookGenre>(Enum.GetValues(typeof(BookGenre)).Cast<BookGenre>());
            MessengerInstance.Register<bool>(this, "resetFields", resetFields);
        }

        #endregion
        #region Functions
        private void AddBookToDb()
        {
            var book = new Book(AuthorName, Title, Quantity, DateTimeSelection, Price, SelectedBookGenre, Eddition);

            if (book.IsValid())
            {
                if (ProductService.Instance.AddBook(book))
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
            AuthorName           = default;
            Title                = default;
            Quantity             = default;
            Eddition             = default;
            SelectedBookGenre    = default;
            Price                = default;
            DateTimeSelection    = default;
            BookGenres           = default;
        }

        #endregion
    }
}
