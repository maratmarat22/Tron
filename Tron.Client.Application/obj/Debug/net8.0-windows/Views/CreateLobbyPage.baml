   M S B A M L   `   `   `  ����R  NTron.Client.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:Tron.Client.Application.ViewsTron.Client.Application.Views  5      3    -Tron.Client.Application.Views.CreateLobbyPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:Tron.Client.Application.Views     5      -   5          N� Title$  CreateLobbyPage��5
       N� 
Background 5      �� 5   
    �� ImageSource$( "../Resources/Images/Background.jpg��6   .��5      � $��Center=�6   $��Center=�6(   $
��1080��6C   $
��1920��6Q   .+�5   
   �� $
��1100��6   $	��170��5      $��0,0,400,600q�5      ��5      �� 5       �� Color$ 	#BB000000��6"   ._�5      �� $��CREATE LOBBY��6      Tiny#��[  5       �� TextAlignment$ Center=�5      $	��150(�5      
�������$��Center=�5       J� Effect 5       3  -System.Windows.Media.Effects.DropShadowEffect  5!         Color$
 Blue��6'      
BlurRadius$ 50Q�5"   '      ShadowDepth$ 0Q�5#   '   	   Opacity$	 1Q�5$   '   5&      5'      �� 5)   
   $	��600��5*   	   $	��100��5+   	   $��Center=�5,   	   $��Center=�5-   	   $��900,700,0,0q�5.   	   y�5)      �� *ConnectionAttemptFailed+  BoolToVisibility
 �� 	Converter#
 [ ��5/      �� 50      $ 	#BB000000��6"   ._�53      �� $��CONNECTION ATTEMPT FAILED��6   $��40(�54      
�������#��[  56      $��Center=�57      $��Center=�58      $ Center=�59       5;        5<      $ HotPink��6'   $ 20Q�5=      $ 0Q�5>      $	 1Q�5?      5A      5B      �� 5E   
   $	��350��6   $	��100��5F      $��0,0,800,230q�5G      ��5H      �� 5I      $ 	#BB000000��6"   ._�5K      �� $��PRIVACY��6   #��[  5L      $ Center=�5M      $��40(�5N       �� Padding$ 27q�5O      
�������$��Center=�5Q       5R        5S      $ Violet��6'   $ 20Q�5T   '   $ 0Q�5U   '   $	 1Q�5V   '   5X      5Y      � 5[   
   $��Center=�5\      $��Center=�5]      $��
0,0,50,230q�5^      $	��100��6!   $	��350��6.   ��5_      �� 5`      $ 	#BB000000��6"    � ColumnDefinitions 5c      �� 5d      $��75 �6#   �� 5e      $	��200 �6#   �� 5f      $��75 �6#   .+�5j      �� $��0��6   $��Center=�5k      $��Center=�5l      $��75��5m      $��0q�5n      $	��100��5o      $��24(�5p      ��5q      �� *ChangePrivacyModeCommand+��5s      �� 5t      $ 	#00000000��6&   ��5w      �� 5x      $��<��6    #��[  5y       
�������$��40(�5{        5}        5~      $ Violet��6/   $ 20Q�5   /   $ 0Q�5�   /   $	 1Q�5�   /   5�      5�      �� 5�      $��1��6   #��[  5�      $��40(�5�      
�������$ Center=�5�      $��Center=�5�      $��Center=�5�      $
��20,0q�5�      ��5�      �� *PrivacyMode+ 5�        5�      $ Violet��6'   $ 20Q�5�      $ 0Q�5�      $	 1Q�5�      5�      �� 5�      $��2��6   $��Center=�5�      $��Center=�5�      $��75��5�      $��0q�5�      $	��100��5�      $��24(�5�      ��5�      �� *ChangePrivacyModeCommand+��5�      �� 5�      $ 	#00000000��6&   ��5�      �� 5�      $��>��6    #��[  5�       
�������$��40(�5�        5�        5�      $ Violet��6/   $ 20Q�5�   /   $ 0Q�5�   /   $	 1Q�5�   /   5�      5�      5�      �� 5�   
   -   5         ��Name$ PasswordTextBox��5�      $��Center=�5�      $��Center=�5�      $	��350��5�      $	��100��5�      $��700,0,0,230q�5�      
�������#��[  5�      $��40(�5�       �� TextAlignment$ Center=�5�      $��Center=�5�      y�5�      �� *PasswordTextBlockVisibility+#
 [ .��6r   ��5�      �� 5�      $ 	#BB000000��6"   .��5�   "   5�      �� 5�   
   $	��350��6   $	��100��5�      $��0,0,800,-20q�5�      $��0q�5�      
���������5�      �� *CreateLobbyCommand+��5�      �� 5�      $ 	#BB000000��6"   ��5�      �� 5�      $��CREATE��6   
�������#��[  5�      $��40(�5�       5�        5�      $ 
LightGreen��6+   $ 20Q�5�   +   $ 0Q�5�   +   $	 1Q�5�   +   5�      5�      �� 5�   
   $	��350��6   $	��100��5�      $��0,0,800,-270q�5�      $��0q�5�      
���������5�      �� *GoBackCommand+��5�      �� 5�      $ 	#BB000000��6"   ��5�      �� 5�      $
��BACK��6   
�������#��[  5�      $��40(�5�       5�        5�      $ HotPink��6+   $ 20Q�5�   +   $ 0Q�5�   +   $	 1Q�5�   +   5�      5�      5�      5      