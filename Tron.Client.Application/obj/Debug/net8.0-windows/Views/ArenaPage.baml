   M S B A M L   `   `   `  ����R  NTron.Client.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:Tron.Client.Application.ViewsTron.Client.Application.Views  5      F BTron.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullP7clr-namespace:Tron.Common.Messages;assembly=Tron.CommonTron.Common.Messages 5      -    'Tron.Client.Application.Views.ArenaPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:Tron.Client.Application.Views     5      N
directions7clr-namespace:Tron.Common.Messages;assembly=Tron.Common     5      -   5          N� Title$  	ArenaPage��5
      $
|�True��5       N� 
Background 5      �� 5   
    �� ImageSource$( "../Resources/Images/Background.jpg��6    J� InputBindings 5      �� 5   
   
 �� Key$ W��6   $  Tron.Common.Messages.Direction	   UP �� CommandParameter# Z 6E    �� Command 6   �� *SetDirectionCommand+6E   �� 5   
   $ S��6      DOWN# Z 6E    6   �� *SetDirectionCommand+6E   �� 5   
   $ A��6   	   LEFT# Z	 6E    6   �� *SetDirectionCommand+6E   �� 5   
   $ D��6   
   RIGHT# Z
 6E    6   �� *SetDirectionCommand+6E   �� 5   
   $ Up��6   # Z 6K    6   �� *ExtraSetDirectionCommand+6K   �� 5   
   $
 Down��6   # Z 6M    6    �� *ExtraSetDirectionCommand+6M   �� 5   
   $
 Left��6   # Z	 6M    6    �� *ExtraSetDirectionCommand+6M   �� 5   
   $ Right��6   # Z
 6N    6!   �� *ExtraSetDirectionCommand+6N   .��5      � $��Center=�6   $��Center=�5      $
��1080��5       $
��1920��5!      .+�5#   
   �� $��Center=�6   $��Bottom=�6.   $
��1080��6I   $
��1920��6W    K� ZIndex$ 1��5$      y�5%      �� *BlackoutVisibility+   BoolToVisibility �� 	Converter# [  ��5&      �� 5'       �� Color$ 	#BB000000��6"   5)      � 5+   
   -   5         �Name$ PlayersGrid��5+      $	��400��5,      $
��auto��5-      $��0,0,1100,700q�5.      $��Center=�5/      $��Center=�50       � ColumnDefinitions 52      �� 53      $	��250 �6#   �� 54      $��75 �6#   �� 55      $��75 �6#   ��58      �� 59      $ 	#BB000000��6"   5;      �� 5>   
   $	��100��6   $	��100��5?   	   $��0,0,1275,-520q�5@   	   $��0q�5A   	   
���������5C   	   �� *GoBackCommand+��5D      �� 5E      $ 	#BB000000��6"   ��5G      �� 5H      $��<��6   
�������  Tiny#��[ 5J      $��40(�5K       J� Effect 5L      3  -System.Windows.Media.Effects.DropShadowEffect  5M         Color$ HotPink��6+      
BlurRadius$ 20Q�5N   #      ShadowDepth$ 0Q�5O   #      Opacity$ 1Q�5P   #   5R      5T      �� 5V   
   #��[ 5W       �� TextAlignment$ Center=�5X      $	��150(�5Y      
�������$��Center=�5[      $��Center=�5\      $	��200��5]      $	��200��5^      $ 2��5_      ��5V      �� *CountdownMessage+y�5`      �� *CountdownVisibility+# [   5a        5b      $ HotPink��6#   $ 50Q�5c      $ 0Q�5d      $ 1Q�5e      5g      �� 5i   
   $��5q�6   $��Center=�5j      $��Center=�5k      
�������$	��710��5m      $
��1010��5n       �� CornerRadius$ 5��5o      $��	400,0,0,0q�5p      ._�5r      � -   5        $ 	ArenaGrid��5r      $	��700��5s      $
��1000��5t      ��5v      �� 5w      $ 	#BB00AA44��6&   .+�5z      �� -   5         ��Name$	 Net��5z      $ 1��5{      5|      5}      5~      5      