   M S B A M L   `   `   `  ����N  JOnlineRestaurantWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:OnlineRestaurantWpf.ViewsOnlineRestaurantWpf.Views  5   	   O,clr-namespace:OnlineRestaurantWpf.ViewModelsOnlineRestaurantWpf.ViewModels  5   	   *    $OnlineRestaurantWpf.Views.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:OnlineRestaurantWpf.Views     5   	   ;vm,clr-namespace:OnlineRestaurantWpf.ViewModels     5   	     � Title$  Online Restaurant Deluxe��5	   	   $	��768��6*   $
��1366��67    � WindowStartupLocation$ CenterScreen=�6D    � 	Resources 5
          BooleanToVisibilityConverter��%   &	        �� 5   
   6'   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   ���	#FF4A4A4A �� Padding$ 10q�65   ._�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $"��Restaurant Management System��6   $��22(�6@   $
��Bold"�6N   
�������$��Center=�6s   �� 5      $ 
Horizontal=�6   $��Right=�66   $��20,0,0,0q�6R   y�6d   �� *IsUserLoggedIn+ �� 	Converter# [  .+�5      �� $��	Welcome, ��6    
�������$��Center=�6H   �� 5      $��SemiBold"�6I   
�������$��Center=�6r   $��0,0,20,0q�6�   ��6    �� *LoggedInUserDisplayName+6�   �� 5      $��Logout��6   $
��10,5q�6P   ���	#FF7E7E7E
�������$��0q�6�   ��6.   �� *LogoutCommand+6�   5      5      5      �� 5   
   $��1��6   $��5q�6K   ��6&   �� *CurrentViewModel+6K   5      5       