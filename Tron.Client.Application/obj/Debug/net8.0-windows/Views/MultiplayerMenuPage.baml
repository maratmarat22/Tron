   M S B A M L   `   `   `  ����R  NTron.Client.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:Tron.Client.Application.ViewsTron.Client.Application.Views  5      7    1Tron.Client.Application.Views.MultiplayerMenuPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:Tron.Client.Application.Views     5        N� Title$  MultiplayerMenuPage��5	       N� 
Background 5      �� 5   
    �� ImageSource$( "../Resources/Images/Background.jpg��6   .��5      � $��Center=�6   $��Center=�6(   $
��1080��6C   $
��1920��6Q   .+�5   
   �� $	��600��5      $	��100��5      $��Center=�5      $��Center=�5      $��900,700,0,0q�5      y�5      �� *ConnectionAttemptFailed+   BoolToVisibility �� 	Converter# [  ��5      �� 5       �� Color$ 	#BB000000��6"   ._�5      �� $��CONNECTION ATTEMPT FAILED��6   $��40(�5      
�������  Tiny#��[ 5      $��Center=�5      $��Center=�5       �� TextAlignment$ Center=�5        J� Effect 5"      3  -System.Windows.Media.Effects.DropShadowEffect  5#         Color$ HotPink��6#      
BlurRadius$ 20Q�5$   '   	   ShadowDepth$	 0Q�5%   '   
   Opacity$
 1Q�5&   '   5(      5)      �� 5+   
   $
��1000��6   $	��170��5,      $��0,0,500,600q�5-      ��5.      �� 5/      $ 	#BB000000��6"   ._�52      �� $��MULTIPLAYER��6   #��[ 53      $ Center=�54      $	��150(�55      
�������$��Center=�57       58        59      $ 
LightGreen��6'   $ 50Q�5:   #   $	 0Q�5;   #   $
 1Q�5<   #   5>      5?      �� 5C   
   $	��500��6   $	��100��5D      $��0,0,800,230q�5E      $��0q�5F      
���������5H      �� *CreateLobbyCommand+��5I      �� 5J      $ 	#BB000000��6"   ��5L      �� 5M      $��CREATE NEW LOBBY��6   #��[ 5N      
�������$��40(�5P       5Q        5R      $
 Blue��6+   $ 20Q�5S   '   $	 0Q�5T   '   $
 1Q�5U   '   5W      5Y      �� 5]   
   $	��500��6   $	��100��5^      $��0,0,800,-20q�5_      $��0q�5`      
���������5b      �� *JoinLobbyCommand+��5c      �� 5d      $ 	#BB000000��6"   ��5f      �� 5g      $��JOIN EXISTING LOBBY��6   #��[ 5h      
�������$��40(�5j       5k        5l      $ Yellow��6+   $ 20Q�5m   '   $	 0Q�5n   '   $
 1Q�5o   '   5q      5s      �� 5w   
   $	��500��6   $	��100��5x      $��0,0,800,-270q�5y      $��0q�5z      
���������5|      �� *InitLocalMpCommand+��5}      �� 5~      $ 	#BB000000��6"   ��5�      �� 5�      $��LOCAL CO-OP��6   #��[ 5�      
�������$��40(�5�       5�        5�      $ Violet��6+   $ 20Q�5�   '   $	 0Q�5�   '   $
 1Q�5�   '   5�      5�      �� 5�   
   $	��500��6   $	��100��5�      $��0,0,800,-520q�5�      $��0q�5�      
���������5�      �� *GoBackCommand+��5�      �� 5�      $ 	#BB000000��6"   ��5�      �� 5�      $
��BACK��6   #��[ 5�      
�������$��40(�5�       5�        5�      $ HotPink��6+   $ 20Q�5�   '   $	 0Q�5�   '   $
 1Q�5�   '   5�      5�      5�      5�      