   M S B A M L   `   `   `  ����A  =Course, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Course.ViewsCourse.Views  5   	   5clr-namespace:Course.ViewModelsCourse.ViewModels  5   	        Course.Views.Authenication   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Course.Views     5   	   .vmclr-namespace:Course.ViewModels     5   	   RwindowsControl9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   -   5          � Title$  Авторизация��5
   	   $	��662��5   	   $
��1006��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowState$ 	Maximized=�5   	    � 
ResizeMode$ NoResize=�5   	   ���#f5f6fa � 	Resources 5         boolToVisibilityConverter��%   &	        �� 5   
   6Z   5      .��5      � -   5         �Name$ mainGrid��5      .+�5   
   r� -   5         r�Name$ 	BackVideo��5      $��Stretch=�6*   $��Stretch=�6F    J� OpacityMask
 ����� r� Volume$ 0Q�6�   	 r� IsMuted	 . 
 r� Stretch$
 UniformToFill=�6�    r� UnloadedBehavior$ Manual=�6�    r� ScrubbingEnabled .  r� LoadedBehavior$ Manual=�6   r� Source 6x   �� *
UriVideo+6  � 5   
   $	��500��6   $	��300��6   $��Center=�6(   $��Center=�6E   .+�5      �� 
���   �$��2��6%    J� Opacity$	 0.8Q�66    �� RadiusX$ 15��6E    �� RadiusY$ 15��6R   $��Stretch=�6_   $��Stretch=�6{     DefaultColor#��[ 6�   $��2��6�   �� 5      $��50q�6   .+�5      ��   FontSizeLowercase#��[ 6�   $��0q�6�   $��0q�6�   
���  ��$��Center=�6�   $��Center=�6  $
��Bold"�6$  ��6   �� *	ErrorText+y�66   ��  �� Path$ DisplayError� �� 	Converter# [  6$  � 5!      $��Center=�6�   $��Center=�6�   $	��350��6�   y�6   �� $ VisibilityAuthenication�# [   � RowDefinitions 5"      �� 5#      $
��auto �6(   �� 5$      $
��auto �6(   �� 5%      $
��auto �6(   �� 5&      $
��auto �6(    � ColumnDefinitions 5(      �� 5)      $	��50* �6+   �� 5*      $	��50* �6+   .+�5.      �� $��1��65   $��2��6B   
 � Tag$ 
Логин��6l   
�������
�������  DefaultTextBox#���  6�   ��6   �� *Login+��6V   Z� 6�   G� 5/      -   5         G�Name$ passwordBox��5/   "   $��2��67   $��2��6D   $��0 10 0 0q�6n   $ Пароль��6�   $��0q�6�   
�������
�������  DefaultPasswordBox#���  6�   ��6X   Z� 6�   �� 51      $��3��6B   $��0��6O   $��Смена пароля��6_   $
��Left=�6v   $��0 10 0 0q�6�   ��6   �� *ResetPasswordBtn+6�   �� 52      $��3��69   $��1��6F   $��Авторизация��6V   $��Right=�6l   $��0 10 0 0q�6�   ��6   �� *	SignBtn+6�   53      � 56      $��Center=�6   $��Center=�6�   $	��350��6�   y�6   �� $ VisibilityResetGrid�# [   57      �� 58      $��4* �6:   �� 59      $	��23* �6:    5;      �� 5<      $
��auto �6(   �� 5=      $
��auto �6(   �� 5>      $
��auto �6(   .+�5@      �� $��0��6   
�������$��14(�6�   $��2��6�   y�6)   �� $ VisibilitiAdditional�# [  ��6�   �� *
Additional+6�   �� 5A      $��1��6   
�������$��0q�6�   $��0q�6�   #���  6  $��2��6*  ��6+   �� *Answer+y�6C   �� $ VisibilityAnswer�# [  ��6�   Z�  6�   �� *	TagAnswer+6*  G� 5C      -   5        $
 pass��5C   "   $��1��6�   $��0q�6�   
�������#���  6�   $��2��6!  y�60   �� $ VisibilitiResPassBox�# [  ��6�   Z�  6�   �� *	TagAnswer+6D  �� 5D      $��2��6:   $��1��6V   $��
Далее��6f   $��Right=�6v   $��0 10 0 0q�6�   ��6   �� *
NextStep+6�   5E      5F      5G      5H      5I      