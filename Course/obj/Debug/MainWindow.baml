   M S B A M L   `   `   `  ����A  =Course, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Course.Views.UsersControlsCourse.Views.UsersControls  5   	   +clr-namespace:Course.ViewsCourse.Views  5   	   clr-namespace:CourseCourse  5   	       Course.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7uc(clr-namespace:Course.Views.UsersControls     5   	   ,viewsclr-namespace:Course.Views     5   	   &localclr-namespace:Course     5   	     � Title$-  'Библиотека Харламова��5
   	   $	��450��5   	   $	��800��6    � WindowStartupLocation$ CenterScreen=�5   	    � WindowState$ 	Maximized=�5   	   .��5      � -   5         �Name$ mainGrid��5       � RowDefinitions 5   
   �� 5      $��40 �6   5      �� 5      $
��100* �6   �� 5      $��40 �6    � ColumnDefinitions 5   
   �� 5      $	��33* �6   �� 5      $	��33* �6   �� 5      $	��33* �6   .+�5   
   �� -   5         ��Name$ menuIcon��5   <      BtnFontAwersone#���   6     
fa_list_ul#��[ 6N   
 � Tag$ 	menu_icon��6t   $
��40 0q�6�   $��Center=�6�   $
��Left=�6�   $��0��6�   $��2��6�    � ToolTip 5      _� 5       .��6   
Меню6    5"      (� 5&   
   -   5        	 (�Name$	 viewBoxClock��5&      $��2��6(   $��1��65   .��5'      '   !Course.Views.UsersControls.PCTime  -   5        
  Name$
 clock��5'      $��20��6'   $��Center=�63   $��Bottom=�6P   $��0q�6k   $��0q�6v   5(      (� 5+   
   -   5        $	 viewBoxDriveSlides��5+      $��2��6.   $��2��6;   $��Right=�6K   .��5,      � -   5        $ driveSlideGrid��5,      $��0q�6+    5-      �� 5.      $	��50* �6'   �� 5/      $	��50* �6'   .+�52      �� -   5        $ 	backSlide��52      $ 
back_slide��6-   $��0��6>   #���   6N     fa_chevron_circle_left#��[ 6x    53      _� 54      .��6"   Прошлый слайд61   56      �� 58      -   5        $ 	nextSlide��58      $ 
next_slide��6,   $��1��6=   $
��40 0q�6M   $��Center=�6]   $��Center=�6z   #���   6�     fa_chevron_circle_right#��[ 6�    59      _� 5:      .��6"   Следующий слайд63   5<      5=      5>      �� 5@   
   -	   5         ��Name$ chapter��5@      $�� ��6!   $��0q�6,   $��0q�67   $��Center=�6C   $��Center=�6`   $��3��6{     FontSizeTitle#��[ 6�   � 5B   
   -
   5         �Name$ rightControlPanel��5B      $��2��6/   $��0��6?   $��Right=�6L   .+�5C      �� -   5        $ iconAuthenication��5C        fa_user#��[ 6/     Label_fontawersome#��[ 6R     FontSizeSlider#��[ 6~   �� 5D      -   5        $ authenication��5D      $��Авторизация��6,     DefaultButtonLabel#��[ 6B    	 FontSizeLowercase#��[	 6�    
 DefaultColor#��[
 6�   $��0 0 40 0q�6�   ��6n   Z� 6�   5E      &    Course.Views.UsersControls.Slide  5G   
   -   5          Name$ content��5G      $��3��6$   $��1��68   0   *Course.Views.UsersControls.PersonalAccount  5I   
   -   5          Name$ personalAccount��5I      $��3��66   $��1��6J   $y�Hidden=�6W   /   )Course.Views.UsersControls.FullScreenMenu  5K   
   -   5          Name$ fullScreenMenu��5K      $y�Hidden=�66   $��2��6J   $��3��6[   r� 5L   
   -   5         r�Name$ media��5L       r� Source$ Resources/play.mp3?�6&    r� LoadedBehavior$
 Play=�6B    r� Volume$ .2Q�6X   5M      5O      