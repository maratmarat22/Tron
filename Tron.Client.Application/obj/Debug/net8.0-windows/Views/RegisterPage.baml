   M S B A M L   `   `   `  ����R  NTron.Client.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:Tron.Client.Application.ViewsTron.Client.Application.Views  5      0    *Tron.Client.Application.Views.RegisterPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:Tron.Client.Application.Views     5      -   5          N� Title$  RegisterPage��5	       N� 
Background 5      �� 5   
    �� ImageSource$( "../Resources/Images/Background.jpg��6   .��5      � .+�5   
   �� $	��800��6   $	��170��5      $��0,0,700,600q�5      ��5      �� 5       �� Color$ 	#BB000000��6"   ._�5      �� $��REGISTER��6      Tiny#��[  5       �� TextAlignment$ Center=�5      $	��150(�5      
�������$��Center=�5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$ HotPink��6'      
BlurRadius$ 50Q�5          ShadowDepth$ 0Q�5!      	   Opacity$	 1Q�5"      5$      5%      �� 5'   
   $��Center=�6   $��Center=�6.   $	��700��5(      $	��100��5)      $��300,0,0,230q�5*      ._�5+      �� $��ENTER YOUR USERNAME HERE��6   #��[  5,      
�������$��40(�5.      $��Center=�5/      $��Center=�50       51        52      $ HotPink��6'   $ 20Q�53   '   $ 0Q�54   '   $	 1Q�55   '   57      ��58      �� 59      $ 	#BB000000��6"   5<      �� 5>   
   -   5        
 ��Name$
 RegisterTextBox��5>      $	��350��5?   
   $	��100��5@   
   #��[  5A   
   $��30(�5B   
   $��Center=�5C   
   
������� �� TextAlignment$ Center=�5E   
   $��0,0,800,230q�5F   
   .��6   ��5G      �� 5H      $ 	#BB000000��6"   .��5I   "   5J      �� 5L   
   $	��300��5M   	   $	��100��5N   	   $��Center=�5O   	   $��Center=�5P   	   $��1050,700,0,0q�5Q   	   y�5L      �� *TryAgainVisibility+  BoolToVisibility �� 	Converter# [ ��5R      �� 5S      $ 	#BB000000��6"   ._�5V      �� $��ERROR��6   $��40(�5W      
�������#��[  5Y      $��Center=�5Z      $��Center=�5[      $ Center=�5\       5^        5_      $ HotPink��6'   $ 20Q�5`      $ 0Q�5a      $	 1Q�5b      5d      5e      �� 5g   
   $	��350��6   $	��100��5h   	   $��0,0,800,-20q�5i   	   $��0q�5j   	   
���������5l   	   �� *RegisterCommand+��5m      �� 5n      $ 	#BB000000��6"   ��5p      �� 5q      $��REGISTER��6   
�������#��[  5s      $��40(�5t       5u        5v      $ 
LightGreen��6+   $ 20Q�5w   #   $ 0Q�5x   #   $	 1Q�5y   #   5{      5}      �� 5   
   $	��350��6   $	��100��5�      $��0,0,800,-270q�5�      $��0q�5�      
���������5�      �� *GoBackCommand+��5�      �� 5�      $ 	#BB000000��6"   ��5�      �� 5�      $
��BACK��6   #��[  5�      
�������$��40(�5�       5�        5�      $ HotPink��6+   $ 20Q�5�      $ 0Q�5�      $	 1Q�5�      5�      5�      5�      5�      