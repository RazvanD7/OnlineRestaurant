   M S B A M L   `   `   `  ����N  JOnlineRestaurantWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:OnlineRestaurantWpf.ViewsOnlineRestaurantWpf.Views  5      O,clr-namespace:OnlineRestaurantWpf.ViewModelsOnlineRestaurantWpf.ViewModels  5      G(clr-namespace:OnlineRestaurantWpf.ModelsOnlineRestaurantWpf.Models  5      O,clr-namespace:OnlineRestaurantWpf.ConvertersOnlineRestaurantWpf.Converters  5	      .    (OnlineRestaurantWpf.Views.ClientMenuView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:OnlineRestaurantWpf.Views     5      ;vm,clr-namespace:OnlineRestaurantWpf.ViewModels     5      ;models(clr-namespace:OnlineRestaurantWpf.Models     5      C
converters,clr-namespace:OnlineRestaurantWpf.Converters     5	      
�������  � 	Resources  5      A   ;OnlineRestaurantWpf.Converters.BooleanToVisibilityConverter %  !LocalBooleanToVisibilityConverter<   6OnlineRestaurantWpf.Converters.InverseBooleanConverter  InverseBooleanConverter7   1OnlineRestaurantWpf.Converters.ImagePathConverter  ImagePathConverter �� 
TargetType  CategoryHeaderStyle �� Property �� Value  ItemNameStyle  ItemDetailStyle  ItemBorderStyle  AddToOrderButtonStyle  DishImageStyle��%�  &	        &	      &	 &     &	 9     &	 �     &	 �    &	 ,    &	 c    &	 t      5   
   62     5   
   6-     5   
   6(   �� 5   
   " ��6   .�5      ��  � ��$ 24(�6)   �� 5       � ��$
 Bold"�6+   �� 5       � ��$ 	0,20,0,10q�6'   �� 5       � �� �#3336+   5      �� 5   
   " ��6   .�5      ��  � ��$ 18(�6)   �� 5       � ��$ SemiBold"�6+   �� 5       � ��$ 0,0,0,5q�6'   5      �� 5   
   " ��6   .�5      ��  � ��$ 14(�6)   �� 5       � ��$ 0,0,0,3q�6'   �� 5       � �� �#5556+   5       �� 5!   
   " ��6   .�5"      ��  � ��
 �����6+   �� 5#       � ��
 �����6,   �� 5$       � ��$ 1q�60   �� 5%       �@��CornerRadius$ 5��6-   �� 5&       �@��Padding$ 15q�6(   �� 5'       � ��$ 5q�6'   �� 5(       � ��$	 300��6&   5)      �� 5*   
   " ��6   .�5+      ��  � ��$
 10,5q�6(   �� 5,       � ��$ 14(�6)   �� 5-       � ��
 �"�"�6+   �� 5.       � ��
 �����6+   �� 5/       � ��$ 0,10,0,0q�6'   �� 50       �@�Cursor$
 Hand��6'   51      �� 52   
   " ��6   .�53      ��  � ��$	 120��6'   �� 54       � ��$	 270��6&   �� 55       � ��$ UniformToFill=�6(   �� 56       � ��$ 0,0,0,10q�6'   �� 57       � ��$ Center=�64   58      59      .��5;      �  � RowDefinitions 5<   
   �� 5=      $
��Auto �6   �� 5>      $��* �6   .+�5A   
   �� $��0��6    �� Padding$ 10q�6   
�������._�5B      �� .+�5C      ��  �� Orientation$ 
Horizontal=�6   $��0,0,0,10q�66   .+�5D      �� $	��300��6   $��0,0,10,0q�6-   ��5E      �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=��� 5F      $��Search��6   ��6.   �� *SearchCommand+5G      �� 5H      $ 
Horizontal=�6   .+�5I      	� $��Search by name��6"   $��0,0,20,0q�5J   !    d� 	IsChecked 5I   ;   �� *SearchByName+5J   !   	� 5K      $��Exclude allergen��6"    6=   �� *ExcludeAllergen+5L      5M      5N      �� 5P   
   $��1��6   $
��Auto=�6$   $��Disabled=�6G   $��10q�6p   .��5Q      �� .+�5R      �� 
���  ��$��0,0,0,10q�5S   �   ��5R      �� *ErrorMessage+y�5S      �� *ErrorMessage+	 �� 	Converter#	 [  
 �� ConverterParameter$
 NotNullOrEmpty��6�   � 5T      $��5��6D   $	��0,5q�6O    � IsIndeterminate 6   �� *	IsLoading+y�5U      �� *	IsLoading+#	 [  �� 5W      ��6   �� *MenuCategories+��5X      �� 5Y       �� DataType4   .OnlineRestaurantWpf.ViewModels.DisplayCategory"  6'   .R�5Z      �� $��0,0,0,20q�6)   .+�5[   "   �� #��[ 6R   ��6,   �� *CategoryDetails.Name+6R   �� 5]   "   $��Dishes��6,   $��20(�6:   $��Medium"�6H   $��0,10,0,5q�6\   y�5^   ,   �� *Dishes.Count+#	 [  $
 GreaterThanZero���� 5_   "   ��6/   �� *Dishes+��5`   &   �� 5a   *   .R�5b   .   �  � Orientation$ 
Horizontal=�68   5c   +   ��5e   &   �� 5f   *   %   OnlineRestaurantWpf.Models.Dish"  67   .R�5g   .   �� #��[ 65   $	��420��6^   ._�5h   2   �  5i   6   �� 5j   :   $��* �6H   �� 5k   :   $
��Auto �6H   .+�5m   6   �� $��0��6A   .+�5n   :   �� #��[ 6@   y�5o   @   �� *Images.Count+#	 [  $
 GreaterThanZero����5p   >   �� 5q   B    �� Path$ Images[0].ImagePath�6J   5s   ;   �� 5t   :   #��[ 6Z   ��6D   �� *Name+6Z   �� 5u   :   #��[ 6a   $
��Wrap=�6�   ��6D   �� *Description+6�   �� 5v   :   #��[ 6D   �� 5w   >   $�Price:��6B   $��SemiBold"�6P    �� 6i   �6m   �� *Price+ �� StringFormat$ C�� �� ConverterCulture$ ro-RO��5x   ;   �� 5y   :   #��[ 6D   �� 5z   >   $�Portion:��6B   $��SemiBold"�6R    �� 5{   >   �6B   �� *PortionQuantity+ �� 6f   �6j   �� *Unit+5|   ;   �� 5}   :   $��SemiBold"�6D   #��[ 6Z   $��0,5,0,0q�6�   y�5~   D   �� *DishAllergens.Count+#	 [  $
 GreaterThanZero���� 5   >   $�
Allergens:��6B   5�   ;   �� 5�   :   ��6G   �� *DishAllergens+��5�   >   �� 5�   B   -   'OnlineRestaurantWpf.Models.DishAllergen"  6O   .R�5�   F   �� #��[ 6o   $��10,0,0,0q�6�   ��6P   �� *Allergen.Name+6�   5�   C   5�   ;   5�   7   �� 5�   6   $��1��6A   $ 
Horizontal=�6N   $��0,10,0,0q�6g   .+�5�   :   �� $
��Edit��6A   #��[ 5�   A   
��� ���$��0,0,0,0q�5�   A   ��5�   A   �� *DataContext.UpdateDishCommand+ �� RelativeSource ��  �� AncestorType UserControl��5�   A   �� y�5�   A   �� *DataContext.IsAdmin+ ��  UserControl  	 BooleanToVisibilityConverter#	 [	 �� 5�   :   $��Delete��6A   #��[ 5�   A   
���  ��$��10,0,0,0q�5�   A   ��5�   A   �� *DataContext.DeleteDishCommand+ ��  UserControl��5�   A   �� y�5�   A   �� *DataContext.IsAdmin+ ��  UserControl#	 [	 5�   7   5�   3   5�   /   5�   +   5�   #   �� 5�   "   $��Menus��6,   $��20(�69   $��Medium"�6G   $��0,15,0,5q�6[   y�5�   ,   �� *Menus.Count+#	 [  $
 GreaterThanZero���� 5�   "   ��6/   �� *Menus+��5�   &   �� 5�   *   .R�5�   .   � $ 
Horizontal=�68   5�   +   ��5�   &   �� 5�   *   %   OnlineRestaurantWpf.Models.Menu"  67   .R�5�   .   �� #��[ 65   $	��420��6^   ._�5�   2   �  5�   6   �� 5�   :   $��* �6H   �� 5�   :   $
��Auto �6H   .+�5�   6   �� $��0��6A   .+�5�   :   �� $	��120��6@   $	��270��6M   $��UniformToFill=�6Y   $��0,0,0,10q�6q   $��Center=�6�   ��5�   >   �� 5�   B   $ FirstDishImagePath�6J   #	 [ 6d   5�   ;   �� 5�   :   #��[ 6Z   ��6D   �� *Name+6Z   �� 5�   :   #��[ 6a   $
��Wrap=�6�   ��6D   �� *Description+6�   �� 5�   :   #��[ 6D   �� 5�   >   $�Price: ��6B   $��SemiBold"�6Q    �� 5�   >   �6B   �� *.+ 
 MenuPriceConverter#	 [
 5�   ;   �� 5�   :   $��	Contains:��6D   $��SemiBold"�6U   #��[ 6k   $��0,5,0,0q�6�   �� 5�   :   ��6G   �� *
MenuDishes+��5�   >   �� 5�   B   )   #OnlineRestaurantWpf.Models.MenuDish"  6O   .R�5�   F   �� #��[ 6P   $��10,0,0,0q�6y   �� 5�   J   �6N   �� *	Dish.Name+ (�� 6m   �6q   �� *QuantityInMenu+�� 6�   �6�   �� *	Dish.Unit+)5�   G   5�   C   5�   ;   �� 5�   :   $��
Allergens:��6D   $��SemiBold"�6V   #��[ 6l   $��0,5,0,0q�6�   �� 5�   :   ��6G   �� *
MenuDishes+��5�   >   �� 5�   B   .R�5�   F   �� ��6S   �� *Dish.DishAllergens+��5�   J   �� 5�   N   .R�5�   R   �� #��[ 6{   $��5,0,0,0q�6�   ��6\   �� *Allergen.Name+6�   5�   O   5�   G   5�   C   5�   ;   5�   7   �� 5�   6   $��1��6A   $ 
Horizontal=�6N   $��0,10,0,0q�6g   .+�5�   :   �� $
��Edit��6A   #��[ 5�   A   
��� ���$��0,0,0,0q�5�   A   ��5�   A   �� *DataContext.UpdateMenuCommand+ ��  UserControl��5�   A   �� y�5�   A   �� *DataContext.IsAdmin+ ��  UserControl#	 [	 �� 5�   :   $��Delete��6A   #��[ 5�   A   
���  ��$��10,0,0,0q�5�   A   ��5�   A   �� *DataContext.DeleteMenuCommand+ ��  UserControl��5�   A   �� y�5�   A   �� *DataContext.IsAdmin+ ��  UserControl#	 [	 5�   7   5�   3   5�   /   5�   +   5�   #   5�      5�      5�      5�      5�      5�      5�      