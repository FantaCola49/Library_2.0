   M S B A M L   `   `   `  ����>  :Lib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Lib.WindowsLib.Windows  5           Lib.Windows.SettingCompany   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:Lib.Windows     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        � Title$)  #Список издательств��5      $	��500��5	      $	��350��5
      $	��500��5      $	��350��5      $	��350��5       � 
ResizeMode$ NoResize=�5       � SizeToContent$ WidthAndHeight=�5       � WindowStartupLocation$ CenterScreen=�5       � 	Resources 5       �� Source�� 5   
   $! Styles/MainWindowStyle.xaml?�6   %    .��5      �  � ColumnDefinitions 5   
   �� 5      $��2* �6   $	��150��6*   �� 5      $��3* �6   $	��300��6*   �� 5      $
��auto �6   $��30��6,   .+�5   
   �� -   5         ��Name$ stackP��5      $��0��6%   .+�5      ��  �� 
IsReadOnly .     TxtToSearch#��[  5      ��5       �� *FilterCompany+	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=��� 5!      -   5        $��lstCompanies��5"      $	��280��5#      ��5$      �� *FilteredCompanies+ �� SelectedItem 5%      �� *SelectedCompany+��5&      �� 5'      .R�5(      �� $��Stretch=�6$   �� 5)      �6"   �� *Name+5*      5+      5-      5.      �� 50   
   -   5        $��
sltCompany��51      $��1��52       � DataContext 53      �� *SelectedItem+ �� ElementName$ lstCompanies��.+�54      �� $��Center=�55      $
��Bold"�56      $8��2Информация об издательстве��57      �� 58      $��Название:��6   �� 59      $��2q�5:       .  ��5<      ��  �� Path$
 Name��� 5=      $��Адрес:��6   �� 5>      $��2q�5?       .  ��5A      �� $ Address��� 5B      $"��Номер телефона:��6   �� 5C      $��2q�5D       .  ��5F      �� $ PhoneNumber��� 5G      $��Почта:��6   �� 5H      $��2q�5I       .  ��5K      �� $
 Mail��� 5M      $��Center=�6    �� Orientation$ 
Horizontal=�66   .+�5N      �� $��Удалить��5Q      ��5O      �� $ lstCompanies��$&  DataContext.DeleteCompanyCommand���5P      �� $ lstCompanies��$ SelectedItem�5Q      5R      5S      �� 5U   
   $��2��6   $��Center=�6%   .+�5V      9� $��Center=�6    9� ExpandDirection$ Right=�62   .��5W      �� -   5        $��
newCompany��5X      $��1��5Y      $	��200��5Z      $	��300��5[       5\      �� *
NewCompany+.+�5]      �� $��Center=�5^      $
��Bold"�5_      $8��2Информация об издательстве��5`      �� 5a      $��Название:��6    �� 5b      $��2q�5c       .  ��5e      �� $
 Name��� 5f      $��Адрес:��6    �� 5g      $��2q�5h       .  ��5j      �� $ Address��� 5k      $"��Номер телефона:��6    �� 5l      $��2q�5m       .  ��5o      �� $ PhoneNumber��� 5p      $��Почта:��6    �� 5q      $��2q�5r       .  ��5t      �� $
 Mail��� 5u      $��Добавить��5x      ��5v      �� $ lstCompanies��$# DataContext.AddCompanyCommand���5w      �� $ lstCompanies��$ SelectedItem�5x      5y      5z      5{      5|      5}      