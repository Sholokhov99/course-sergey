   M S B A M L   `   `   `  ���� A  =Course, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Course.ViewsCourse.Views  5clr-namespace:Course.ViewModelsCourse.ViewModels       Course.Views.Authenication   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:Course.Views     .vmclr-namespace:Course.ViewModels     RwindowsControl9http://schemas.microsoft.com/winfx/2006/xaml/presentation    -     � Title$  Авторизация��$	��662��$
��1006�� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=� � 
ResizeMode$ NoResize=����#f5f6fa � 	Resources    boolToVisibilityConverter��%   &	        �� .��� -    �Name$ mainGrid��.+�r� -    r�Name$ 	BackVideo��$��Stretch=�$��Stretch=� J� OpacityMask
 ����� r� Volume$ 0Q�	 r� IsMuted	 . 
 r� Stretch$
 UniformToFill=� r� UnloadedBehavior$ Manual=� r� ScrubbingEnabled .  r� LoadedBehavior$ Manual=� r� Source �� *
UriVideo+� $	��500��$	��300��$��Center=�$��Center=�.+��� 
���   �$��2�� J� Opacity$	 0.8Q� �� RadiusX$ 15�� �� RadiusY$ 15��$��Stretch=�$��Stretch=�  DefaultColor#��[ $��2���� $��50q�.+���   FontSizeLowercase#��[ $��0q�$��0q�
���  ��$��Center=�$��Center=�$
��Bold"����� *	ErrorText+y���  �� Path$ DisplayError� �� 	Converter# [  � $��Center=�$��Center=�$	��350��y��� $ VisibilityAuthenication�# [   � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto � � ColumnDefinitions �� $	��50* ��� $	��50* �.+��� $��1��$��2��
 � Tag$ 
Логин��
�������
�������  DefaultTextBox#���  ���� *Login+��Z� G� -    G�Name$ passwordBox��$��2��$��2��$��0 10 0 0q�$ Пароль��$��0q�
�������
�������  DefaultPasswordBox#���  ��Z� �� $��3��$��0��$��Смена пароля��$
��Left=�$��0 10 0 0q����� *ResetPasswordBtn+�� $��3��$��1��$��Авторизация��$��Right=�$��0 10 0 0q����� *	SignBtn+� $��Center=�$��Center=�$	��350��y��� $ VisibilityResetGrid�# [   �� $��4* ��� $	��23* � �� $
��auto ��� $
��auto ��� $
��auto �.+��� $��0��
�������$��14(�$��2��y��� $ VisibilitiAdditional�# [  ���� *
Additional+�� $��1��
�������$��0q�$��0q�#���  $��2������ *Answer+y��� $ VisibilityAnswer�# [  ��Z�  �� *	TagAnswer+G� -   $
 pass��$��1��$��0q�
�������#���  $��2��y��� $ VisibilitiResPassBox�# [  ��Z�  �� *	TagAnswer+�� $��2��$��1��$��
Далее��$��Right=�$��0 10 0 0q����� *
NextStep+