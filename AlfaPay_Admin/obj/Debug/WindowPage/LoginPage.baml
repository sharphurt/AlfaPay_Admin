   M S B A M L   `   `   `  ����H  DAlfaPay_Admin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:AlfaPay_Admin.CustomControlAlfaPay_Admin.CustomControl  5      (    "AlfaPay_Admin.WindowPage.LoginPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5      =customControl)clr-namespace:AlfaPay_Admin.CustomControl  5      -   5          N� Title$  	LoginPage��5	      $	��630��5      $
��1200��5       N� 
Background 5      �� 5   
      
Background �� ImageSource# �   6    j� Stretch$ UniformToFill=�6@   .��5      � -   5         �Name$ MainGrid��5      $��Center=�6   $��Center=�6:   |�. .+�5   
   � -   5         �Name$ 	MainPanel��5   +    � Orientation$ Vertical=�6   $	��340��6>   .+�5      ��   LightPrimaryText#��[ 6   $��36(�6>   
�������$��Вход��6_   � 5      $ Vertical=�6   $��0,35,0,0q�6/   .+�5      ��   SecondaryText#��[ 6   
�������$��
Логин��6R   �� 5      -   5         ��Name$ 
LoginInput��5   K     InputUnfilledWhiteStyle#��[ 6   $��Center=�5      .��6<   .��5      ��  �� Path$ Login�6   	 �� UpdateSourceTrigger$	 PropertyChanged=�6+   
 �� ValidationRules
 5      5  /System.Windows.Controls.DataErrorValidationRule  5      5      5      5      � 5      $ Vertical=�6   $��0,25,0,0q�6/   .+�5       �� #��[ 6   
�������$��Пароль��6R   �� 5!      -   5        $ PasswordInput��5!   N     PasswordInputUnfilledStyle#��[ 6   $��Center=�5#      .��6<   .��5$      �� $ Password�6   $	 PropertyChanged=�6.   
 5%        5&      5(      5)      5*      �� 5,      -   5         ��Name$ ErrorTextBlock��5,        ErrorMessageStyleTxtBlock#��[ 5-      $��1,5,0,0q�5.      $��50��5/      $
��Wrap=�50       �� 
LineHeight$ 7��52      ��51      �� $ ErrorMessage� �� Mode$ TwoWay=���53      ��  �� ElementName$ 	MainPanel��$ ActualWidth��� 56      -   5         ��Name$ LoginButton��56   3   $��0,35,0,0q�6   $��50��6'   y�57      �� *LoginRequestManager.Status+ / +InvertedRequestStatusInProgressToVisibility �� 	Converter# [ ��58      �� *LoginCommand+ J� OpacityMask 59      !� 5:       �� Opacity$ 1Q�6"    !� Visual 5;      1   +AlfaPay_Admin.CustomControl.HollowTextBlock  5<         
Background
 �   �   
FontFamily$ Segoe UI)�5=   <      FontSize$ 16Q�5>   <      
FontWeight$
 Bold"�5?   <      TextAlignment$ Center=�5@   <      VerticalTextAlignment$ Center=�5A   <      Text$ 
ВОЙТИ��5D   <   ��5B   <   �� $ LoginButton��$ ActualWidth���5C   <   �� $ LoginButton��$ ActualHeight�5D   <   5G      ��5I      �� 5J       �� 
TargetType" ��6     TransparentWhiteButton �� BasedOn# [ 60   .�5K      ��  �� Property � {� �� Value .  66    �� Triggers 5L      ^� 5M        ^� 
Conditions  5N   "   �� 5O   &   ! �� Binding! 60   �� $ (Validation.HasError)�$ 
LoginInput��" �� Value	" False5P   0   �� 5Q   &   ! 60   �� $ Text.IsEmpty�$ 
LoginInput��	" False5R   0   �� 5S   &   ! 5T   )   �� $ (Validation.HasError)�$ PasswordInput��	" False5U   )   �� 5V   &   ! 60   �� $ Text.IsEmpty�$ PasswordInput��	" False5W   0   �� 5X   &   ! 5Y   )   �� *LoginRequestManager.Status+ ) %InvertedRequestStatusInProgressToBool# [ " True5Z   )   .0�5\   "   ��  � {� . 6>   5]      5_      5a      "  FontAwesome.WPF.ImageAwesome  5c       	 SpinSmallWhiteIcon#��[	 5e      $��20��5f      $��20��6   #   Spin# . $��	0,-50,0,0q�64   $   SpinDuration$	$ 0.8Q�5g      y�5d      �� *LoginRequestManager.Status+ '
 #RequestStatusInProgressToVisibility# [
 5g      5i      5j      5k      