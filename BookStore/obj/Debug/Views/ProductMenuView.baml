   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      D @BookStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:BookStore.ViewsBookStore.Views 5      %   BookStore.Views.ProductMenuView   5      1System&clr-namespace:System;assembly=mscorlib  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:BookStore.Views     5        � DataContext  5
      ��    Locator �� Source# [   �� Path$ ProductMenu�.��5      �  � RowDefinitions 5   
   �� 5      �� 5      $	��30* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��2* �6   �� 5      $��2* �6   .+�5   
   �� $	��Top=�6   $��1��6-   .+�5      �� $��Type:��5      
�������5      �� 5       �� Text$ Type:��5      $��0��5"      $��50��5#      ��5       �� *ProductType+ �� SelectedItem 5!      �� *SelectedType+ �� Mode$ TwoWay=�5#      �� 5%      $��Genre:��5&      
�������5'      �� 5)      -   5         ��Name$ BookOrJournalGenreCB��5*      $ Genre��5)      $��0��5+      $��50��5,      ��5-      �� *ProductGenre+$ TwoWay=� 5.      �� *SelectedGenre+50      �� 52   
   $��Name: ��6   
�������$��1��54      $��0��55      �� 57   
   $��Price: ��6   
�������$��1��59      $	��Top=�5:      $��Center=�5;      �� 5=   
   $��1��5>      $��1��5?      
����������#272537��5@      �� *
ShowList+ 5A      �� *SelectedProduct+��5E      �� 5F      .R�5G      � $	��320��6    5H      �� 5I      $	��220 �6/   �� 5J      $	��100 �6/   .+�5L      �� $��0��6$   ��65   �� *Description+�� 5M      $��1��6$   ��64   �� *	BasePrice+5N      5O      5Q      �� 5S   
   $��2��6   $��1��6!   y�6.   �� *BookVisibility+._�5T      �� .+�5U      �� 	 �� Orientation$	 
Horizontal=�6   .+�5V      �� $��Title: ��6    
�������6=   �� 5W      
���������6    �� *Description+6=   5X      �� 5Y      $	 
Horizontal=�6   .+�5Z      �� $��Author: ��6    
�������6@   �� 5[      
���������6    �� *AuthorOrEditor+6@   5\      �� 5]      $	 
Horizontal=�6   .+�5^      �� $��	Edition: ��6    
�������6F   �� 5_      
���������6    �� *EditionOrIssueNumber+6F   5`      �� 5a      $	 
Horizontal=�6   .+�5b      �� $��
Quantity: ��6    
�������6:   �� 5c      
���������6    �� *
Quantity+6:   5d      �� 5e      $	 
Horizontal=�6   .+�5f      �� $��PublicationDate: ��6    
�������6A   �� 5g      
���������6    �� *PublicationDate+6A   5h      5i      5j      �� 5l   
   $��2��6   $��1��6!   y�6.   �� *JournalVisibility+._�5m      �� .+�5n      �� $	 
Horizontal=�6   .+�5o      �� $��Title: ��6    $��16(�6/   
�������6J   �� 5p      $��16(�6=   
���������6    �� *Description+6K   5q      �� 5r      $	 
Horizontal=�6   .+�5s      �� $��Editor: ��6    $��16(�60   
�������6M   �� 5t      $��16(�6@   
���������6    �� *AuthorOrEditor+6N   5u      �� 5v      $	 
Horizontal=�6   .+�5w      �� $��Issue Number: ��6    $��16(�69   
�������6S   �� 5x      $��16(�6F   
���������6    �� *EditionOrIssueNumber+6T   5y      �� 5z      $	 
Horizontal=�6   .+�5{      �� $��
Quantity: ��6    $��16(�65   
�������6G   �� 5|      $��16(�6:   
���������6    �� *
Quantity+6H   5}      �� 5~      $	 
Horizontal=�6   .+�5      �� $��PublicationDate: ��6    $��16(�69   
�������6O   �� 5�      $��16(�6A   
���������6    �� *PublicationDate+6O   5�      5�      5�      5�      5�      