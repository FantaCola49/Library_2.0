   M S B A M L   `   `   `  ����>  :Lib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Lib.WindowsLib.Windows  5          Lib.Windows.MainWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:Lib.Windows     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        � Title$  Библиотека 2.0��5      $	��828��5	      $	��570��5
      $	��828��5      $	��570��5       � WindowStartupLocation$ CenterScreen=�5       � 	Resources 5       �� Source�� 5   
   $! Styles/MainWindowStyle.xaml?�6   %    .��5      � -   5         �Name$ 
CommonGrid��5       � ColumnDefinitions 5   
   �� 5      $��5* �6   �� 5      $��4* �6   $	��250��6*    � RowDefinitions 5   
   �� 5      $��24��6   �� 5      .+�5   
   o� $��0��5       $��2��5!      $��3q�5"      
�������.@�5$      m� $��Файл��6   .@�5%      m� $��
Выход��6;    m� Command 6   �� *ExitCommand+6;   5&      m� 5'      $+��%Дополнительные окна��6   .@�5(      m� $��Компании��6G    6   �� *SettingCompaniesCommand+6G   m� 5)      $��
Жанры��6D    6   �� *SettingGenresCommand+6D   5*      m� 5+      $��Справка��6;    6   �� *GetInfoAboutApp+6;   5,      � 5.   
   $��1��6   $��0��6    5/      �� 50      �� 51      .+�54      �� -   5         ��Name$ stackP��54      $��0��6)   .+�55      ��    TxtToSearch#��[  6   ��6?   �� *FilterAuthors+	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=��� 57        
btnPlusPic#��[ 6>   ��6   �� *AddAuthorCommand+.��58      �� $��/Resources/plus1.png��6   59      �� 5;      -   5        $��
lstAuthors��5<      ��5=      �� *FilteredAuthors+ �� SelectedItem 5>      ��  �� Path$ SelectedAuthor�$	 TwoWay=���5?      �� 5@      .R�5A      �� $��Stretch=�6(   �� 5B   "   �6&   �� *	Surname+ �� 5C   "   �6&   �� *Name+  StringToProperties �� 	Converter# [  �� 5D   "   �6&   �� *
Patronymic+# [ 5E      5F      5H      5I      �� 5K      $��1��6   .+�5L      �� #��[  6   ��6?   �� *FilterBooks+$	 TwoWay=�$
 PropertyChanged=��� 5M      #��[ 6<   ��6   �� *AddBookCommand+.��5N      �� $��/Resources/plus1.png��6   5O      �� 5P      -   5        $��lstBooks��5Q      ��5R      �� *FilteredBooks+ 5S      �� *SelectedBook+��5T      �� 5U      .R�5V      �� ��6(   �� $
 Name�5W      5Y      5Z      5[      � 5]   
   $��1��6   $��1��6    5^      �� 5_      �� 5`      $
��0.8* �6    .+�5c      �� -   5        $��	sltAuthor��5d      $��0��5e       � DataContext 5f      �� *SelectedItem+ �� ElementName$ 
lstAuthors��.+�5g      �� $��Center=�5h      $
��Bold"�5i      $,��&Информация об авторе��5j      �� 5k      $��Фамилия:��6   �� 5l      $��2q�6   ��6%   �� $ Surname��� 5m      $��Имя:��6   �� 5n      $��2q�6   ��6%   �� $
 Name��� 5o      $��Отчество:��6   �� 5p      $��2q�6   ��6%   �� $ 
Patronymic��� 5q      $��Псевдоним:��6   �� 5r      $��2q�6   ��6%   �� $ Nickname��� 5t      $��Center=�6    �� Orientation$ 
Horizontal=�6:   .+�5u      �� $��Изменить��5x      ��5v      �� $ 
lstAuthors��$# DataContext.EditAuthorCommand���5w      �� $ DataContext�$ 	sltAuthor��5x      �� 5y      $��Удалить��5|      ��5z      �� $ 
lstAuthors��$% DataContext.DeleteAuthorCommand���5{      �� $ 
lstAuthors��$ SelectedItem�5|      5}      5~      �� 5�      -   5        $��sltBook��5�      $��1��5�       5�      �� *SelectedItem+$ lstBooks��.+�5�      �� $��Center=�5�      $
��Bold"�5�      $(��"Информация о книге��5�      �� 5�      $��Название:��6   �� 5�      $��2q�6   ��6%   �� $
 Name��� 5�      $��	Жанр:��6   �� 5�      $��2q�6   ��6%   �� $ DataContext�$ sltBook��$	 TwoWay=�  FormatJanre# [ �� 5�      $��ISBN:��6   �� 5�      $��2q�6   ��6%   �� $
 ISBN��� 5�      $��Center=�6   $ 
Horizontal=�6:   .+�5�      �� $��Изменить��5�      ��5�      �� $ 
lstAuthors��$! DataContext.EditBookCommand���5�      �� $ DataContext�$ sltBook��5�      �� 5�      $��Удалить��5�      ��5�      �� $ 
lstAuthors��$# DataContext.DeleteBookCommand���5�      �� $ lstBooks��$ SelectedItem�5�      5�      5�      5�      5�      5�      