   M S B A M L   `   `   `  ����i  eMicrosoft.VisualStudio.Shell.15.0, Version=16.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3avUclr-namespace:Microsoft.VisualStudio.Shell;assembly=Microsoft.VisualStudio.Shell.15.0Microsoft.VisualStudio.Shell  5       	DeeHelper@   :DeeHelper.GetActionGroupWindow.GetActionGroupWindowControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ivsshellUclr-namespace:Microsoft.VisualStudio.Shell;assembly=Microsoft.VisualStudio.Shell.15.0     5      -   5        $��MyToolWindow��5      ,   &Microsoft.VisualStudio.Shell.VsBrushes    	WindowKey#���   5         WindowTextKey#���  5      .��5      � .+�5   
   ��  �� Orientation$ Vertical=�6   .+�5      � $��30��6    � ColumnDefinitions 5      �� 5      $
��0.1* �6'   66   �� 5      6)   �� 5      $
��0.2* �6'   66   .+�5      �� $
��URL:��6   $��10(�6(   $
��Bold"�66   $��Center=�6H   6e   �� 5      -   5         ��Name$ 
URLTextBox��5   ,   $��0,4,10,4q�6   $��1��6@   $��Center=�6P   6t   �� 5      -   5        $��2��5      $��21��6)   
���   �
���   �
�������$��Generate��6o   6�   5      � 5      $��30��6    5      �� 5      $��0.085* �6'   68   �� 5      6)   .+�5      �� $
��Pre:��6   $��10(�6(   $
��Bold"�66   $��Center=�6H   6e   �� 5      -   5        $ ActionGroupPreTextBox��5   <    �� 
IsReadOnly . $��2��6,   $	��0,4q�6[   $��Center=�6h   6�   5       � 5"      $��30��6    5#      �� 5$      $��0.085* �6'   68   �� 5%      6)   .+�5'      �� $��Post:��6   $��10(�6)   $
��Bold"�67   $��Center=�6I   6f   �� 5(      -   5        $ ActionGroupPostTextBox��5(   <    . $��2��6,   $	��0,4q�6\   $��Center=�6i   6�   5)      5*      5+      5,      