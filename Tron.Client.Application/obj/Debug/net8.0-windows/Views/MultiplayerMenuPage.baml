   M S B A M L   `   `   `  ����R  NTron.Client.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:Tron.Client.Application.ViewsTron.Client.Application.Views  5      7    1Tron.Client.Application.Views.MultiplayerMenuPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:Tron.Client.Application.Views     5        N� Title$  MultiplayerMenuPage��5	       N� 
Background 5      �� 5   
    �� ImageSource$( "../Resources/Images/Background.jpg��6   .��5      � $��Center=�6   $��Center=�6(   $
��1080��6C   $
��1920��6Q   .+�5   
   �� $	��600��5      $	��100��5      $��Center=�5      $��Center=�5      $��900,700,0,0q�5      y�5      �� *NotConnected+   BoolToVisibility �� 	Converter# [  ��5      �� 5       �� Color$ 	#BB000000��6"   ._�5      �� $��CONNECTION ATTEMPT FAILED��6   $��40(�5      
�������  Tiny#��[ 5      $��Center=�5      $��Center=�5        �� TextAlignment$ Center=�5!       J� Effect 5#      3  -System.Windows.Media.Effects.DropShadowEffect  5$         Color$ HotPink��6#      
BlurRadius$ 20Q�5%   '   	   ShadowDepth$	 0Q�5&   '   
   Opacity$
 1Q�5'   '   5)      5*      �� 5,   
   $
��1000��6   $	��170��5-      $��0,0,500,600q�5.      ��5/      �� 50      $ 	#BB000000��6"   ._�53      �� $��MULTIPLAYER��6   #��[ 54      $ Center=�55      $	��150(�56      
�������$��Center=�58       59        5:      $ 
LightGreen��6'   $ 50Q�5;   #   $	 0Q�5<   #   $
 1Q�5=   #   5?      5@      �� 5D   
   $	��500��6   $	��100��5E      $��0,0,800,230q�5F      $��0q�5G      
���������5I      �� *CreateLobbyCommand+��5J      �� 5K      $ 	#BB000000��6"   ��5M      �� 5N      $��CREATE NEW LOBBY��6   #��[ 5O      
�������$��40(�5Q       5R        5S      $
 Blue��6+   $ 20Q�5T   '   $	 0Q�5U   '   $
 1Q�5V   '   5X      5Z      �� 5^   
   $	��500��6   $	��100��5_      $��0,0,800,-20q�5`      $��0q�5a      
���������5c      �� *JoinLobbyCommand+��5d      �� 5e      $ 	#BB000000��6"   ��5g      �� 5h      $��JOIN EXISTING LOBBY��6   #��[ 5i      
�������$��40(�5k       5l        5m      $ Yellow��6+   $ 20Q�5n   '   $	 0Q�5o   '   $
 1Q�5p   '   5r      5t      �� 5x   
   $	��500��6   $	��100��5y      $��0,0,800,-270q�5z      $��0q�5{      
���������5}      �� *InitLocalMpCommand+��5~      �� 5      $ 	#BB000000��6"   ��5�      �� 5�      $��LOCAL CO-OP��6   #��[ 5�      
�������$��40(�5�       5�        5�      $ Violet��6+   $ 20Q�5�   '   $	 0Q�5�   '   $
 1Q�5�   '   5�      5�      �� 5�   
   $	��500��6   $	��100��5�      $��0,0,800,-520q�5�      $��0q�5�      
���������5�      �� *GoBackCommand+��5�      �� 5�      $ 	#BB000000��6"   ��5�      �� 5�      $
��BACK��6   #��[ 5�      
�������$��40(�5�       5�        5�      $ HotPink��6+   $ 20Q�5�   '   $	 0Q�5�   '   $
 1Q�5�   '   5�      5�      5�      5�      