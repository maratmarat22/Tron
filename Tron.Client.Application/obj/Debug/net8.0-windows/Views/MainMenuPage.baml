   M S B A M L   `   `   `  ����R  NTron.Client.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:Tron.Client.Application.ViewsTron.Client.Application.Views  5      0    *Tron.Client.Application.Views.MainMenuPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:Tron.Client.Application.Views     5        N� Title$  MainMenuPage��5	       N� 
Background 5      �� 5   
    �� ImageSource$( "../Resources/Images/Background.jpg��6   .��5      � $��Center=�6   $��Center=�6(   $
��1080��6C   $
��1920��6Q   .+�5   
   �� $	��500��6   $	��170��5      $��0,0,800,600q�5      ��5      �� 5       �� Color$ 	#BB000000��6"   ._�5      �� $
��TRON��6      Tiny#��[  5       �� TextAlignment$ Center=�5      $	��150(�5      
�������$��Center=�5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$ HotPink��6'      
BlurRadius$ 50Q�5   #      ShadowDepth$ 0Q�5    #   	   Opacity$	 1Q�5!   #   5#      5$      �� 5(   
   $	��350��6   $	��100��5)      $��0,0,800,230q�5*      $��0q�5+      
���������5-      �� *InitSpCommand+��5/      �� 50      $ 	#BB000000��6"   ��52      �� 53      $��SINGLEPLAYER��6   
�������#��[  55      $��40(�56       57        58      $
 Blue��6+   $ 20Q�59   '   $ 0Q�5:   '   $	 1Q�5;   '   5=      5?      �� 5C   
   $	��350��6   $	��100��5D      $��0,0,800,-20q�5E      $��0q�5F      
���������5H      �� *NavToMpMenuCommand+��5J      �� 5K      $ 	#BB000000��6"   ��5M      �� 5N      $��MULTIPLAYER��6   #��[  5O      
�������$��40(�5Q       5R        5S      $ 
LightGreen��6+   $ 20Q�5T   '   $ 0Q�5U   '   $	 1Q�5V   '   5X      5Z      �� 5^   
   $	��350��6   $	��100��5_      $��0,0,800,-270q�5`      $��0q�5a      
���������5c      �� *NavToSettingsCommand+��5e      �� 5f      $ 	#BB000000��6"   ��5h      �� 5i      $��SETTINGS��6   #��[  5j      
�������$��40(�5l       5m        5n      $ LightYellow��6+   $ 20Q�5o   '   $ 0Q�5p   '   $	 1Q�5q   '   5s      5u      �� 5y   
   $	��350��6   $	��100��5z      $��0,0,800,-520q�5{      $��0q�5|      
���������5~      �� *ExitCommand+��5      �� 5�      $ 	#BB000000��6"   ��5�      �� 5�      $
��EXIT��6   #��[  5�      
�������$��40(�5�       5�        5�      $ HotPink��6+   $ 20Q�5�   '   $ 0Q�5�   '   $	 1Q�5�   '   5�      5�      5�      5�      