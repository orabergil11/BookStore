   M S B A M L   `   `   `  ����D  @BookStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:BookStore.ViewsBookStore.Views  5      !    BookStore.Views.AddBookView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:BookStore.Views     5        � DataContext  5	      ��    Locator �� Source# [   �� Path$ AddBook�.��5
      �  � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      .+�5      �� $��Author Name:��6   
�������$��20(�6?   $��0��6M   �� 5      $��Title:��6   
�������$��20(�6?   $��1��6M   �� 5      $��
Date Time:��6   
�������$��20(�6?   $��2��6M   �� 5      $��
Base Price��6   
�������$��20(�6?   $��3��6M   �� 5      $��Eddition��6   
�������$��20(�6?   $��4��6M   �� 5      $��Book Genre: ��6   
�������$��20(�6?   $��5��6M   �� 5       $��
Quantity: ��6   
�������$��20(�6?   $��6��6M   �� 5"      $��1��6h   $��0��6x   ��6   �� *
AuthorName+6x   �� 5#      $��1��6h   $��1��6x   ��6   �� *Title+6x   (  "System.Windows.Controls.DatePicker  5$      $��1��6h   $��2��6x      Text 6   �� *DateTimeSelection+ �� Mode$ TwoWay=�6x   �� 5%      $��1��6h   $��3��6x   ��6   �� *Price+6x   �� 5&      $��1��6h   $��4��6x   ��6   �� *
Eddition+6x   �� 5'      $��1��6d   $��5��6t   ��6   �� *
BookGenres+ �� SelectedItem 6=   �� *SelectedGenre+6t   �� 5(      $��1��6h   $��6��6x   ��6   �� *
Quantity+6x   �� 5)   
   $��Add Product��5*   	   $��1��5,      $��7��5-      $��30��5.      $	��100��5/      ��5+   	   �� *AddProductCommand+50      52      53      