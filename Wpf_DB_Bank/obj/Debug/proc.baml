   M S B A M L   `   `   `  ����  Wpf_DB_Bank    Wpf_DB_Bank.proc   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$
  proc��5   	   $	��321��6   $	��629��6#   .��5      � $��0,-2,0,2q�6   ���	#FFFFCF48.+�5   
   �� $��Choose client ID:��6   $
��Left=�6,   $��	20,69,0,0q�6G   $	��Top=�6Z   $��34��6r   $	��188��6~   �� 5   
   -   5         ��Name$ 	cid_combo��5      $
��Left=�6@   $��24��6[   $	��Top=�6h   $	��113��6�   $y�Visible=�6�   $��
136,71,0,0q�6�   ��6'   �� ��5      �� 5	       �� EndPoint$	 0,1&�6&    �� 
StartPoint$	 0,0&�65   .��5
      �  � Color$ 	#FFF0F0F0��6#    � Offset$ 0Q�65   � 5      $ 	#FF919DEC��6#   $ 1Q�65   5      5      �� 5   
   -   5        $��Make a pay raise��5      $
��Left=�6-   $��24��6H   $��
269,71,0,0q�6T   $	��Top=�6h   $	��150��6�   ��5      �� 5       �� Triggers 5      �� 5       �� Binding 6&   ��  �� ElementName$ 	cid_combo��	 �� Path$
	 Text�
 �� Value
  6[   .a�5      ��  �� Property � {� �� Value .  6A   5      5      5      �� 5   
   $W��QA procedure that will update the limit on the credit card to the selected client:��6   $
��Left=�6l   $��25��6�   $��	20,30,0,0q�6�   $	��Top=�6�   $	��465��6�   $
��Bold"�6�   �� 5   
   $����A function that will calculate the average salary at all branches of the Bank, 
and returns the branch code in which the average salary is the minimum of all:��6   $
��Left=�6�   $��45��6�   $��
20,163,0,0q�6�   $	��Top=�6�   $	��559��6  $
��Bold"�6!  �� 5   
   -   5         ��Name$	 ans��5      $��27��6!   $��232,213,0,0q�6-   $
��Wrap=�6B   $	��Top=�6W   $��50��6o    J� RenderTransformOrigin$ 0.906,0.704&�6z   
���   �$��14(�6�   $
��Bold"�6�   
�������$
��Left=�6�    �� Padding$ 5q�6�   �� 5   
   -   5        $"��Click Here to see the answer��5      $
��Left=�68   $��27��6S   $��
31,213,0,0q�6_   $	��Top=�6s   $	��177��6�   6�   5'      5(      