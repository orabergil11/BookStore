   M S B A M L   `   `   `  ����  	BookStore$    BookStore.Views.AddJournalView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � DataContext  5      ��    Locator �� Source# [   �� Path$ 
AddJournal�.��5	      �  � RowDefinitions 5
   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $��Editor Name:��6   $��0��6(   
�������$��20(�6I   �� 5   
   $��Name: ��6   $��1��6"   
�������$��20(�6I   �� 5   
   $��Issue Number:��6   $��2��6)   
�������$��20(�6I   �� 5   
   $��
Date Time:��6   $��3��6&   
�������$��20(�6I   �� 5   
   $��Price:��6   $��4��6"   
�������$��20(�6I   �� 5    
   $��	Frequency��6   $��5��6%   
�������$��20(�6I   �� 5!   
   $��Genre: ��6   $��6��6#   
�������$��20(�6I   �� 5"   
   $��
Quantity: ��6   $��7��6&   
�������$��20(�6I   �� 5%      $��0��63   $��1��6@   ��6   �� *
EditorName+6@   �� 5&      $��1��63   $��1��6@   ��6   �� *Title+6@   �� 5'      $��2��63   $��1��6@   ��6   �� *IssueNumber+6@   �� 5(      $��7��63   $��1��6@   ��6   �� *
Quantity+6@   �� 5)      $��4��63   $��1��6@   ��6   �� *Price+6@   (  "System.Windows.Controls.DatePicker  5*      $��3��6Q   $��1��6^      SelectedDate 6   �� *DateTimeSelection+ �� Mode$ TwoWay=�6^   �� 5+   
   $��5��6l   $��1��6y    �� SelectedItem 6   �� *SelectedJournalFreq+��6A   �� *JournalFrequencies+6y   �� 5,   
   $��6��6l   $��1��6y    6   �� *SelectedJournalGenre+��6A   �� *JournalGenres+6y   �� 5-   
   $��Add Product��5.   	   $��1��50      $��8��51      $��30��52      $	��100��53      ��5/   	   �� *AddProductCommand+54      55      56      