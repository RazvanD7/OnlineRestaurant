   M S B A M L   `   `   `  ����N  JOnlineRestaurantWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:OnlineRestaurantWpf.ViewsOnlineRestaurantWpf.Views  5      O,clr-namespace:OnlineRestaurantWpf.ViewModelsOnlineRestaurantWpf.ViewModels  5      )    #OnlineRestaurantWpf.Views.LoginView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:OnlineRestaurantWpf.Views     5      ;vm,clr-namespace:OnlineRestaurantWpf.ViewModels     5      $��Segoe UI)�5
      
�������$	��300��6<   $	��450��6K   $��30q�6Z     � 	Resources  5       �� 
TargetType �� Property �� Value��%�  '��       '�� q     'G� O    '�� -    �� 5   
   " ��6   .�5      ��  � ��$ 0,0,0,5q�6'   �� 5       � ��$ 14(�6)   5      �� 5   
   " ��6   .�5      ��  � ��$ 0,0,0,15q�6'   �� 5       � ��$ 8q�6(   �� 5       � ��$ 14(�6)   �� 5       � ��$ 1q�60   �� 5       � ��
 �����6,   5      �� 5   
   " G�6   .�5      ��  � ��$ 0,0,0,20q�6'   �� 5       � ��$ 8q�6(   �� 5       � ��$ 14(�6)   �� 5       � ��$ 1q�60   �� 5       � ��
 �����6,   5      �� 5   
   " ��6   .�5      ��  � ��$
 15,8q�6(   �� 5        � ��$ 15(�6)   �� 5!       � ��$ SemiBold"�6+   �� 5"       � �� �	#FF007ACC6+   �� 5#       � ��
 �����6+   �� 5$       � ��$ 0q�60   �� 5%       �@�Cursor$
 Hand��6'   �� 5&       � ��$ 0,5,0,0q�6'   5'      5(      .��5*      ��  �� CornerRadius$ 5��6   
������� �� Padding$ 25q�61   ._�5+   
   �� $��Stretch=�6   $��Center=�63   .+�5,      �� $��Restaurant Login��6   $��28(�60   $
��Bold"�6>   $��0,0,0,25q�6P   $��Center=�6b   ���	#FF007ACC6   �� 5.      $��Email Address:��6   �� 5/      ��6   �� *Email+ �� UpdateSourceTrigger$ PropertyChanged=��� 51      $��	Password:��6   G� 52      -   5         G�Name$ PasswordBoxControl��52      66   �� 54      $��Login��6    �� 	IsDefault$
 True��6F   ��6%   �� *LoginCommand+6F   �� 56      
���  ��$��Center=�6G   $��0,10,0,0q�6d   $
��Wrap=�6v   ��6   �� *ErrorMessage+6v   �� 58      $��Create Account��6   $��0,15,0,0q�6\   
������ ���	#FF007ACC$��0q�6�   $��Center=�6�   ��6.   �� *NavigateToRegisterCommand+6�   �� 5:      $��or��6   $��Center=�6"   $��0,15,0,0q�6?   
�������6Q   �� 5<      $��View Menu Without Login��6   $��0,15,0,0q�6g   
������ ���	#FF007ACC$��0q�6�   $��Center=�6�   ��67   �� *NavigateToClientViewCommand+6�   5=      5>      5?      