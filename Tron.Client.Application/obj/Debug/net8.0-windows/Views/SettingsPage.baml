   M S B A M L   `   `   `  ����R  NTron.Client.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:Tron.Client.Application.ViewsTron.Client.Application.Views  5      0    *Tron.Client.Application.Views.SettingsPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:Tron.Client.Application.Views     5        N� Title$  SettingsPage��5	       N� 
Background 5      �� 5   
    �� ImageSource$( "../Resources/Images/Background.jpg��6   .��5      � $��Center=�6   $��Center=�6(   $
��1080��6C   $
��1920��6Q   .+�5   
   �� $	��750��6   $	��170��5      $��0,0,750,600q�5      ��5      �� 5       �� Color$ 	#BB000000��6"   ._�5      �� $��SETTINGS��6      Tiny#��[  5       �� TextAlignment$ Center=�5      $	��150(�5      
�������$��Center=�5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$ LightYellow��6'      
BlurRadius$ 50Q�5   '      ShadowDepth$ 0Q�5    '   	   Opacity$	 1Q�5!   '   5#      5$      �� 5'   
   $	��350��6   $	��100��5(      $��0,0,800,230q�5)      ��5*      �� 5+      $ 	#BB000000��6"   ._�5-      �� $��VOLUME��6   #��[  5.      $ Center=�5/      $��40(�50      
 �� Padding$
 27q�51      
�������$��Center=�53       54        55      $
 Blue��6'   $ 20Q�56   '   $ 0Q�57   '   $	 1Q�58   '   5:      5;      � 5=   
   $��760,250,810,480q�6   $	��100��6(   $	��350��65   ��5>      �� 5?      $ 	#BB000000��6"    � ColumnDefinitions 5B      �� 5C      $��75 �6#   �� 5D      $	��200 �6#   �� 5E      $��75 �6#   .+�5I      �� $��0��6   $��Center=�5J      $��Center=�5K      $��75��5L      $��0q�5M      $	��100��5N      $��24(�5O      ��5P      �� *SwitchVolumeSettingCommand+��5R      �� 5S      $ 	#00000000��6&   ��5V      �� 5W      $��<��6    #��[  5X      
�������$��40(�5Z       5\        5]      $
 Blue��6/   $ 20Q�5^   '   $ 0Q�5_   '   $	 1Q�5`   '   5b      5d      �� 5g      $��1��6   #��[  5i      $��40(�5j      
�������$ Center=�5l      $��Center=�5m      $��Center=�5n      $
��20,0q�5o      ��5h      �� *VolumeSetting+ 5q        5r      $
 Blue��6'   $ 20Q�5s      $ 0Q�5t      $	 1Q�5u      5w      �� 5z      $��2��6   $��Center=�5{      $��Center=�5|      $��75��5}      $��0q�5~      $	��100��5      $��24(�5�      ��5�      �� *SwitchVolumeSettingCommand+��5�      �� 5�      $ 	#00000000��6&   ��5�      �� 5�      $��>��6    #��[  5�      
�������$��40(�5�       5�        5�      $
 Blue��6/   $ 20Q�5�   '   $ 0Q�5�   '   $	 1Q�5�   '   5�      5�      5�      �� 5�   
   $	��350��6   $	��100��5�      $��0,0,800,-20q�5�      $��0q�5�      
���������5�      �� *GoBackCommand+��5�      �� 5�      $ 	#BB000000��6"   ��5�      �� 5�      $
��BACK��6   
�������#��[  5�      $��40(�5�       5�        5�      $ HotPink��6+   $ 20Q�5�   +   $ 0Q�5�   +   $	 1Q�5�   +   5�      5�      5�      5�      