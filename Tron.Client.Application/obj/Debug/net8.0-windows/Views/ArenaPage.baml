   M S B A M L   `   `   `  ����R  NTron.Client.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:Tron.Client.Application.Views.Gameplay&Tron.Client.Application.Views.Gameplay  5      F BTron.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullP7clr-namespace:Tron.Common.Messages;assembly=Tron.CommonTron.Common.Messages 5      6    0Tron.Client.Application.Views.Gameplay.ArenaPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Flocal4clr-namespace:Tron.Client.Application.Views.Gameplay     5      N
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
��1920��5!      .+�5$   
   �� $	��100��6   $	��100��5%   	   $��0,0,1250,-520q�5&   	   $��0q�5'   	   
���������5)   	   �� *GoBackCommand+��5*      �� 5+       �� Color$ 	#BB000000��6"   ��5-      �� 5.      $��<��6   
�������   Tiny#��[  50      $��40(�51       J� Effect 52      3  -System.Windows.Media.Effects.DropShadowEffect  53         Color$ HotPink��6+      
BlurRadius$ 20Q�54   #      ShadowDepth$ 0Q�55   #      Opacity$ 1Q�56   #   58      5:      �� 5<   
   #��[  5=       �� TextAlignment$ Center=�5>      $	��150(�5?      
�������$��Center=�5A      $��Center=�5B      $	��200��5C      $	��200��5D       K� ZIndex$ 1��5E      ��5<      �� *CountdownMessage+y�5F      �� *CountdownVisibility+  BoolToVisibility �� 	Converter# [  5H        5I      $ HotPink��6#   $ 50Q�5J      $ 0Q�5K      $ 1Q�5L      5N      �� 5P   
   $��5q�6   $��Center=�5Q      $��Center=�5R      
�������$	��710��5T      $
��1010��5U      ._�5W      �� -   5         ��Name$ Arena��5W      $��Center=�5X      $��Center=�5Y      
���   �$	��700��5[      $
��1000��5\       � DataContext 5]      �� 5^      5_      5`      