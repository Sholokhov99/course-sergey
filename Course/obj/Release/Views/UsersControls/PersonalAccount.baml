   M S B A M L   `   `   `  ���� A  =Course, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Course.Views.UsersControlsCourse.Views.UsersControls  0    *Course.Views.UsersControls.PersonalAccount   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     :local(clr-namespace:Course.Views.UsersControls     -   
�������.���� $��Stretch=�$��Stretch=�
�������.@��� $!��Личный кабинет��
�������.���   � ColumnDefinitions  �� $
��auto ��� $	��150 ��� $
��100* � � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��100* �.+��� $��Логин:��$��0��$��0���� -    ��Name$ login��$��	You login��$��0��$��1��   
PA_textBox#���   �� $��Пароль:��$��1��$��0���� -   $ passwordTxtBox��$��	You login��$��1��$��1��$y�Hidden=�#���   G� -    G�Name$ passwordPassBox��$��1��$��1�� G� Password$ You password��  PA_passwordBox#���  �� -    ��Name$ eyePassword��$��1��$��2�� 
 fa_eye#��[   FontSizeSlider#��[ $��Center=�  BtnFontAwersone#��[ $
��Left=� � ToolTip _� .��Показать пароль�� $&�� Секретный вопрос:��$��2��$��0���� -   $ secretQuestion��$��	You login��$��2��$��1��#���   �� $#��Ответ на вопрос:��$��3��$��0���� -   $ answerQuestionTxtBox��$��	You login��$��3��$��1��$y�Hidden=�#���   G� -   $ answerQuestionPassBox��$��3��$��1��$ You password��#���  �� -	   $ eyeSecretQuestion��$��3��$��3��#��[ #��[ $��Center=�#��[ $
��Left=� _� .��/-Показать ответ на вопрос�� $$��Уровень доступа:��$��4��$��0���� -
   $ accessTxtBox��$#��Уровень доступа��$��4��$��1��#���   � $��2��$��5��$��Right=�$��Bottom=�.+��� -   $ unSign��$��
Выйти��$��0 0 20 0q� _� .��" Выйти из аккаунта�� $��Сохранить�� _� .��'%Сохранить изменения�� -    ��Name$ driveAccounts��$/��)Управление аккаунтами��$
��Auto=�
�������.���  �� $
��100* ��� $
��auto �.+�� -    �Name$ 	wrapPanel��$��Center=�$��0��� $��1��$��Right=�.+��� -   $ 
newAccount��$-��'Создать пользователя��$��Right=�$��0 0 20 0q� _� .��86Создание нового пользователя�� -   $ SaveDriveAccount��$��Сохранить�� _� .��)'Сохранение изменений�� -   $ driveProgram��$/��)Управление программой�����	#FFACACAC$
��Auto=�
�������.��� .+��� $
��Left=�.+��� -   $ 
NewChapter��$!��Создание главы��$��0 20 0 0q�$	��144���� -   $ NewTopic��$��Создание темы��$��0 20 0 0q�$	��144���� -   $ NewSlide��$#��Создание слайда��$��0 20 0 0q�$	��144���� -   $ 	EditSlide��$/��)Редактирование слайда��$��0 20 0 0q�$	��144���� $
��Left=�.+��� -   $ 
DelChapter��$!��Удаление главы��$��	20 20 0 0q�$	��144���� -   $ DelTopic��$��Удаление темы��$��	20 20 0 0q�$	��144���� -   $ DelSlide��$#��Удаление слайда��$��	20 20 0 0q�$	��144��