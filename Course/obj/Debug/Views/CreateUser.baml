   M S B A M L   `   `   `  ����A  =Course, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Course.ViewsCourse.Views  5   	       Course.Views.CreateUser   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Course.Views     5   	     � Title$/  )Создание пользователя��5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � WindowState$ 	Maximized=�5
   	   .��5      �� $��Center=�6   $��Center=�6,   $	��450��6I   .+�5   
   �� -   5         ��Name$ 
errorLabel��5      $>��8Все поля должны быть заполнены��6$   $��Center=�6M   
���  ��$y�Hidden=�6{   �� 5   
   -   5         ��Name$ login��5      
 � Tag$ 
Логин��67   
���   �$��0q�6V   $��0q�6b      FontSizeSlider#��[  6m   
���   �  DefaultTextBox#���  6�   ��6!   Z� 6�   G� 5   
   -   5         G�Name$ password��5      $ Пароль��6(   
���   �$��0q�6H   
���   �  DefaultPasswordBox#��[ 6i   $��0 10 0 0q�6�   �� 5   
   -   5        $ question��5      $% Секретный вопрос��6:   
���   �$��0q�6d   #��[  6p   
���   �#���  6�   $��0 10 0 0q�6�   ��6$   Z� 6�   �� 5   
   $<��6Уровень доступа пользователя��6   G� 5   
   -   5        $ answer��5      $" Ответ на вопрос��6<   
���   �$��0q�6e   #��[  6r   
���   �#���  6�   $��0 10 0 0q�6�   ��6&   Z� 6�   �� 5   
   $<��6Уровень доступа пользователя��6   $��0 10 0 0q�67   �� 5   
   -   5         ��Name$ access��5      #��[  6#    �� 
IsEditable . 	 �� 
IsReadOnly	 . 
 �� SelectedIndex$
 0��6r   .@�5      �� $"��0 - Пользователь��6   �� 5      $$��1 - Администратор��6   5      �� 5   
   -   5         ��Name$ create��5      $��Создать��6!   $��Right=�63   $��0 10 0 0q�6O   6a   5      5      