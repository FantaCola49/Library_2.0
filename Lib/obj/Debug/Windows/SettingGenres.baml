   M S B A M L   `   `   `  ����>  :Lib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Lib.WindowsLib.Windows  5          Lib.Windows.SettingGenres   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:Lib.Windows     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        � Title$  
Жанры��5      $	��500��5	      $	��350��5
      $	��500��5      $	��350��5       � 
ResizeMode$ NoResize=�5       � SizeToContent$ Height=�5       � WindowStartupLocation$ CenterScreen=�5       � 	Resources 5       �� Source�� 5   
   $! Styles/MainWindowStyle.xaml?�6   %    .��5      �  � RowDefinitions 5   
   �� 5      $��1* �6   $	��100��6(   �� 5      $��1* �6   $	��100��6(   �� 5      $
��auto �6   .+�5   
   �� -   5         ��Name$ stackP��5      $��0��5      $��2��5      .+�5      ��  �� 
IsReadOnly .     TxtToSearch#��[  5!      ��5"      �� *FilterGenre+	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=��� 5#      -   5        $��	lstGenres��5$      $	��100��5%      $	��483��5&      $��Disabled=�5(      ��5'      �� *FilteredGenres+ �� SelectedItem 5)      �� *SelectedGenre+��5*      �� 5+      .R�5,      �  K� IsItemsHost . 6$   5-      ��5/      �� 50      .R�51      �� $��25��6$   $��Stretch=�63   �� 52      �6"   �� *Name+53      54      56      57      �� 59   
   -   5        $��sltGenre��5:      $��1��5;       � DataContext 5<      �� *SelectedItem+ �� ElementName$ 	lstGenres��.+�5=      �� $��Center=�5>      $
��Bold"�5?      $��Жанр��5@      �� 5A      $��Название:��6   �� 5B      $��2q�5C       .  ��5E      ��  �� Path$
 Name��� 5G      $��Center=�6    �� Orientation$ 
Horizontal=�66   .+�5H      �� $��Удалить��5K      ��5I      �� $ 	lstGenres��$$ DataContext.DeleteGenreCommand���5J      �� $ 	lstGenres��$ SelectedItem�5K      5L      5M      �� 5O   
   $��2��6   $��Center=�6"   .+�5P      9� $��Center=�6    9� ExpandDirection$
 Down=�62   .��5Q      �� -   5        $��newGenre��5R      $��1��5S      $	��120��5T       5U      �� *
NewGenre+.+�5V      �� $��Center=�5W      $
��Bold"�5X      $��Новый жанр��5Y      �� 5Z      $��Название:��6    �� 5[      $��2q�5\       .  ��5^      �� $
 Name��� 5_      $��Добавить��5b      ��5`      �� $ 	lstGenres��$! DataContext.AddGenreCommand���5a      �� $ 	lstGenres��$ SelectedItem�5b      5c      5d      5e      5f      5g      