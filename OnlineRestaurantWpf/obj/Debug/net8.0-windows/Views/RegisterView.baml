   M S B A M L   `   `   `  ����N  JOnlineRestaurantWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:OnlineRestaurantWpf.ViewsOnlineRestaurantWpf.Views  5      O,clr-namespace:OnlineRestaurantWpf.ViewModelsOnlineRestaurantWpf.ViewModels  5      ,    &OnlineRestaurantWpf.Views.RegisterView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:OnlineRestaurantWpf.Views     5      ;vm,clr-namespace:OnlineRestaurantWpf.ViewModels     5      $��Segoe UI)�5
      
�������$	��350��6<   $	��500��6K   $��30q�6Z     � 	Resources  5       �� 
TargetType �� Property �� Value��%�  '��       '�� q     'G� O    '�� -    �� 5   
   " ��6   .�5      ��  � ��$ 0,0,0,5q�6'   �� 5       � ��$ 14(�6)   5      �� 5   
   " ��6   .�5      ��  � ��$ 0,0,0,15q�6'   �� 5       � ��$ 8q�6(   �� 5       � ��$ 14(�6)   �� 5       � ��$ 1q�60   �� 5       � ��
 �����6,   5      �� 5   
   " G�6   .�5      ��  � ��$ 0,0,0,15q�6'   �� 5       � ��$ 8q�6(   �� 5       � ��$ 14(�6)   �� 5       � ��$ 1q�60   �� 5       � ��
 �����6,   5      �� 5   
   " ��6   .�5      ��  � ��$
 15,8q�6(   �� 5        � ��$ 15(�6)   �� 5!       � ��$ SemiBold"�6+   �� 5"       � �� �	#FF007ACC6+   �� 5#       � ��
 �����6+   �� 5$       � ��$ 0q�60   �� 5%       �@�Cursor$
 Hand��6'   �� 5&       � ��$ 0,5,0,0q�6'   5'      5(      .��5*      ��  �� CornerRadius$ 5��6   
������� �� Padding$ 25q�61   ._�5+   
   �� $
��Auto=�6   .��5,      �� $��Stretch=�6   $��Center=�67   .+�5-      �� $��Create Account��6   $��28(�62   $
��Bold"�6@   $��0,0,0,25q�6R   $��Center=�6d   ���	#FF007ACC6�   �� 5/      $��First Name:��6   �� 50      ��6   �� *	FirstName+ �� UpdateSourceTrigger$ PropertyChanged=��� 52      $��
Last Name:��6   �� 53      ��6   �� *
LastName+$ PropertyChanged=��� 55      $��Email Address:��6   �� 56      ��6   �� *Email+$ PropertyChanged=��� 58      $��Phone Number (Optional):��6   �� 59      ��6   �� *Phone+$ PropertyChanged=��� 5;      $"��Delivery Address (Optional):��6   �� 5<      ��6   �� *	Address+$ PropertyChanged=� �� TargetNullValue$  ���� 5>      $��	Password:��6   G� 5?      -   5         G�Name$ PasswordBoxRegister��5?      6;   �� 5A      $��Confirm Password:��6   G� 5B      -   5        $  ConfirmPasswordBoxRegister��5B      6B   �� 5D      $��Register��6   $��	0,20,0,10q�6P   ��6,   �� *RegisterCommand+6P   �� 5E      $��Back to Login��6   
������ ���	#FF007ACC$��0q�6�   $��Center=�6�   ��61   �� *NavigateBackToLoginCommand+6�   �� 5G      
���  ��$��Center=�6K   $��0,10,0,0q�6h   $
��Wrap=�6z   ��6   �� *ErrorMessage+6z   5H      5I      5J      5K      