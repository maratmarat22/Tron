   M S B A M L   `   `   `  ����R  NTron.Client.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:Tron.Client.Application.Views.Menu"Tron.Client.Application.Views.Menu  5      5    /Tron.Client.Application.Views.Menu.SettingsPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Blocal0clr-namespace:Tron.Client.Application.Views.Menu     5        N� Title$  SettingsPage��5	       N� 
Background 5      �� 5   
    �� ImageSource$+ %../../Resources/Images/Background.jpg��6   .��5      � $��Center=�6   $��Center=�6(   $
��1080��6C   $
��1920��6Q   .+�5   
   �� $	��750��6   $	��170��5      $��0,0,750,600q�5      ��5      �� 5       �� Color$ 	#BB000000��6"   ._�5      �� $��SETTINGS��6      Tiny#��[  5       �� TextAlignment$ Center=�5      $	��150(�5      
�������$��Center=�5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$ LightYellow��6'      
BlurRadius$ 50Q�5   '      ShadowDepth$ 0Q�5    '   	   Opacity$	 1Q�5!   '   5#      5$      �� 5'   
   $	��350��6   $	��100��5(      $��0,0,800,230q�5)      ��5*      �� 5+      $ 	#BB000000��6"   ._�5-      �� $��MUSIC VOLUME��6   #��[  5.      $ Center=�5/      $��40(�50      
 �� Padding$
 27q�51      
�������$��Center=�53       54        55      $
 Blue��6'   $ 20Q�56   '   $ 0Q�57   '   $	 1Q�58   '   5:      5;      �� 5>   
   $	��350��6   $	��100��5?      $��0,0,800,-20q�5@      ��5A      �� 5B      $ 	#BB000000��6"   ._�5D      �� $��	FX VOLUME��6   #��[  5E      $ Center=�5F      $��40(�5G      $
 27q�5H      
�������$��Center=�5J       5K        5L      $ 
LightGreen��6'   $ 20Q�5M   '   $ 0Q�5N   '   $	 1Q�5O   '   5Q      5R      �� 5U   
   $	��350��6   $	��100��5V      $��0,0,800,-270q�5W      $��0q�5X      
���������5Z      �� *GoBackCommand+��5[      �� 5\      $ 	#BB000000��6"   ��5^      �� 5_      $
��BACK��6   
�������#��[  5a      $��40(�5b       5c        5d      $ HotPink��6+   $ 20Q�5e   +   $ 0Q�5f   +   $	 1Q�5g   +   5i      5k      �� 5n   
    � Minimum$ 0Q�6    � Maximum$	 100Q�5o       � Value$	 100Q�5p       �� TickFrequency$ 1Q�5q       �� IsSnapToTickEnabled$
 True��5r       �� TickPlacement$ BottomRight=�5s      $	��300��5t      $��40��5u      $��0,0,100,228q�5v      ��5w      �� 5x      $ 	#BB000000��6"   5z      5|      5}      