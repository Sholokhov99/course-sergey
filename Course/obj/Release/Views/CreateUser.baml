   M S B A M L   `   `   `  ���� A  =Course, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Course.ViewsCourse.Views      Course.Views.CreateUser   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:Course.Views       � Title$/  )Создание пользователя�� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=�.���� $��Center=�$��Center=�$	��450��.+��� -    ��Name$ 
errorLabel��$>��8Все поля должны быть заполнены��$��Center=�
���  ��$y�Hidden=��� -    ��Name$ login��
 � Tag$ 
Логин��
���   �$��0q�$��0q�   FontSizeSlider#��[  
���   �  DefaultTextBox#���  ��Z� G� -    G�Name$ password��$ Пароль��
���   �$��0q�
���   �  DefaultPasswordBox#��[ $��0 10 0 0q��� -   $ question��$% Секретный вопрос��
���   �$��0q�#��[  
���   �#���  $��0 10 0 0q���Z� �� $<��6Уровень доступа пользователя��G� -   $ answer��$" Ответ на вопрос��
���   �$��0q�#��[  
���   �#���  $��0 10 0 0q���Z� �� $<��6Уровень доступа пользователя��$��0 10 0 0q��� -    ��Name$ access��#��[   �� 
IsEditable . 	 �� 
IsReadOnly	 . 
 �� SelectedIndex$
 0��.@��� $"��0 - Пользователь���� $$��1 - Администратор���� -    ��Name$ create��$��Создать��$��Right=�$��0 10 0 0q�