   M S B A M L   `   `   `  ����E  AFilter UID, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Filter_UID
Filter_UID  5   	       Filter_UID.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Filter_UID     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5   	    � Title$ 
Filter UID��5	   	   $	��467��6   $	��758��6)    � Icon$ icon.png��6L   .��5
      � $��	0,0,101,0q�6   $	��423��6   .+�5   
   �� $#��Chọn file UID hôm trước��6   $
��Left=�62   $��	48,30,0,0q�6M   $	��Top=�6`   �� 5   
   -   5         ��Name$ 	UIDBefore��5      $
��Left=�6%   $��
250,30,0,0q�6@    �� TextWrapping$
 Wrap=�6T   $	��Top=�6h   $	��356��6�   $��26��6�   6�   �� 5   
   $��Chọn file UID hôm sau��6   $
��Left=�60   $��	48,95,0,0q�6K   $	��Top=�6^   �� 5   
   -   5        $ UIDAfter��5      $
��Left=�6$   $��
250,95,0,0q�6?   $
 Wrap=�6S   $	��Top=�6g   $	��356��6   $��26��6�   6�   �� 5   
   $"��Chọn file UID đối thủ��6   $
��Left=�60   $��
51,146,0,0q�6K   $	��Top=�6_   �� 5   
   -   5        $ UIDOpponent��5      $
��Left=�6'   $��251,146,0,0q�6B   $
 Wrap=�6W   $	��Top=�6k   $	��356��6�   $��26��6�   6�   �� 5   
   $ ��Đặt tên file xuất ra��6   $
��Left=�6/   $��
67,272,0,0q�6J   $	��Top=�6^   �� 5   
   -   5        $ NameFileExport��5      $
��Left=�6*   $��253,272,0,0q�6E   $
 Wrap=�6Z   $	��Top=�6n   $	��356��6�   $��26��6�    J� RenderTransformOrigin$ 0.497,1.386&�6�   �� 5   
   -   5         ��Name$ checkUID��5      $	��UID��6%   $
��Left=�63   $��269,319,0,0q�6N   $	��Top=�6c   $ -0.028,0.566&�6{    d� 	IsChecked$
 TrueY�6�   �� 5   
   -   5        $ 
checkPhone��5      $��Điện thoại��6'   $
��Left=�6<   $��431,319,0,0q�6W   $	��Top=�6l   �� 5   
   -   5        $��
ExportFile��5      $��Xuất File��6#   $
��Left=�67   $��256,356,0,0q�6R   $	��Top=�6g   $ -0.189,-0.674&�6   $��29��6�   $��76��6�   ${�False��6�   �� 5   
   $"��Chọn fodler file xuất ra��6   $
��Left=�63   $��
55,209,0,0q�6N   $	��Top=�6b   �� 5   
   -	   5        $ NameFileExportFolder��5      $
��Left=�60   $��253,211,0,0q�6K   $
 Wrap=�6`   $	��Top=�6t   $	��356��6�   $��26��6�   $ 0.497,1.386&�6�   �� 5   
   -
   5        $��ResetButton��5      $��Reset��6$   $
��Left=�64   $��374,358,0,0q�6O   $	��Top=�6d   $��29��6|   $��56��6�   6�   5      5      