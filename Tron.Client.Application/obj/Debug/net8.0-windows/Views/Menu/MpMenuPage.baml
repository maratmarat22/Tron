   M S B A M L   `   `   `  ����R  NTron.Client.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:Tron.Client.Application.Views.Menu"Tron.Client.Application.Views.Menu  5      3    -Tron.Client.Application.Views.Menu.MpMenuPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Blocal0clr-namespace:Tron.Client.Application.Views.Menu     5        N� Title$  
MpMenuPage��5	       N� 
Background 5      �� 5   
    �� ImageSource$+ %../../Resources/Images/Background.jpg��6   .��5      � $��Center=�6   $��Center=�6(   $
��1080��6C   $
��1920��6Q   .+�5   
   �� $
��1000��6   $	��170��5      $��0,0,500,600q�5      ��5      �� 5       �� Color$ 	#BB000000��6"   ._�5      �� $��MULTIPLAYER��6      Tiny#��[  5       �� TextAlignment$ Center=�5      $	��150(�5      
�������$��Center=�5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$ 
LightGreen��6'      
BlurRadius$ 50Q�5   #      ShadowDepth$ 0Q�5    #   	   Opacity$	 1Q�5!   #   5#      5$      �� 5(   
   $	��500��6   $	��100��5)      $��0,0,800,230q�5*      $��0q�5+      
���������5-      �� *CreateLobbyCommand+��5.      �� 5/      $ 	#BB000000��6"   ��51      �� 52      $��CREATE NEW LOBBY��6   #��[  53      
�������$��40(�55       56        57      $
 Blue��6+   $ 20Q�58   '   $ 0Q�59   '   $	 1Q�5:   '   5<      5>      �� 5B   
   $	��500��6   $	��100��5C      $��0,0,800,-20q�5D      $��0q�5E      
���������5G      �� *JoinLobbyCommand+��5H      �� 5I      $ 	#BB000000��6"   ��5K      �� 5L      $��JOIN EXISTING LOBBY��6   #��[  5M      
�������$��40(�5O       5P        5Q      $ Yellow��6+   $ 20Q�5R   '   $ 0Q�5S   '   $	 1Q�5T   '   5V      5X      �� 5\   
   $	��500��6   $	��100��5]      $��0,0,800,-270q�5^      $��0q�5_      
���������5a      �� *InitLocalMpCommand+��5b      �� 5c      $ 	#BB000000��6"   ��5e      �� 5f      $��LOCAL CO-OP��6   #��[  5g      
�������$��40(�5i       5j        5k      $ 
DarkViolet��6+   $ 20Q�5l   '   $ 0Q�5m   '   $	 1Q�5n   '   5p      5r      �� 5v   
   $	��500��6   $	��100��5w      $��0,0,800,-520q�5x      $��0q�5y      
���������5{      �� *GoBackCommand+��5|      �� 5}      $ 	#BB000000��6"   ��5      �� 5�      $
��BACK��6   #��[  5�      
�������$��40(�5�       5�        5�      $ HotPink��6+   $ 20Q�5�   '   $ 0Q�5�   '   $	 1Q�5�   '   5�      5�      5�      5�      