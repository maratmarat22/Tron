   M S B A M L   `   `   `  ����R  NTron.Client.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:Tron.Client.Application.Views.Menu"Tron.Client.Application.Views.Menu  5      5    /Tron.Client.Application.Views.Menu.MainMenuPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Blocal0clr-namespace:Tron.Client.Application.Views.Menu     5        N� Title$  MainMenuPage��5	       N� 
Background 5      �� 5   
    �� ImageSource$+ %../../Resources/Images/Background.jpg��6   .��5      � $��Center=�6   $��Center=�6(   $
��1080��6C   $
��1920��6Q   .+�5   
   �� $	��500��6   $	��170��5      $��0,0,800,600q�5      ��5      �� 5       �� Color$ 	#BB000000��6"   ._�5      �� $
��TRON��6      Tiny#��[  5       �� TextAlignment$ Center=�5      $	��150(�5      
�������$��Center=�5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$ HotPink��6'      
BlurRadius$ 50Q�5   #      ShadowDepth$ 0Q�5    #   	   Opacity$	 1Q�5!   #   5#      5$      �� 5(   
   $	��350��6   $	��100��5)      $��0,0,800,230q�5*      $��0q�5+      
���������5.      �� 5/      $ 	#BB000000��6"   ��51      �� 52      $��SINGLEPLAYER��6   
�������#��[  54      $��40(�55       56        57      $
 Blue��6+   $ 20Q�58   '   $ 0Q�59   '   $	 1Q�5:   '   5<      5>      �� 5B   
   $	��350��6   $	��100��5C      $��0,0,800,-20q�5D      $��0q�5E      
���������5G      �� *NavToMpMenuCommand+��5I      �� 5J      $ 	#BB000000��6"   ��5L      �� 5M      $��MULTIPLAYER��6   #��[  5N      
�������$��40(�5P       5Q        5R      $ 
LightGreen��6+   $ 20Q�5S   '   $ 0Q�5T   '   $	 1Q�5U   '   5W      5Y      �� 5]   
   $	��350��6   $	��100��5^      $��0,0,800,-270q�5_      $��0q�5`      
���������5b      �� *NavToSettingsCommand+��5d      �� 5e      $ 	#BB000000��6"   ��5g      �� 5h      $��SETTINGS��6   #��[  5i      
�������$��40(�5k       5l        5m      $ LightYellow��6+   $ 20Q�5n   '   $ 0Q�5o   '   $	 1Q�5p   '   5r      5t      �� 5x   
   $	��350��6   $	��100��5y      $��0,0,800,-520q�5z      $��0q�5{      
���������5}      �� *ExitCommand+��5~      �� 5      $ 	#BB000000��6"   ��5�      �� 5�      $
��EXIT��6   #��[  5�      
�������$��40(�5�       5�        5�      $ HotPink��6+   $ 20Q�5�   '   $ 0Q�5�   '   $	 1Q�5�   '   5�      5�      5�      5�      