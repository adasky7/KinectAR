   M S B A M L   `   `   `  ����N  JLightBuzz.Vitruvius, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullhGclr-namespace:LightBuzz.Vitruvius.Controls;assembly=LightBuzz.VitruviusLightBuzz.Vitruvius.Controls  5   	   C ?KinectAR, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:KinectARKinectAR 5   	      KinectAR.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   \controlsGclr-namespace:LightBuzz.Vitruvius.Controls;assembly=LightBuzz.Vitruvius     5   	   (localclr-namespace:KinectAR     5   	     � Title$  AR Data collection��5	   	   $	��700��6$   $��1467.2��61   .��5      ]� ��.  $��Stretch=�6&   $��10q�6D   $��Stretch=�6P   .+�5   
   � 
�������.+�5      �� 
�������$��1q�6*   $
��Left=�6>   $	��585��6Y   $��	10,10,0,0q�6f   $	��Top=�6y   $	��774��6�   ._�5      � .+�5      /   )LightBuzz.Vitruvius.Controls.KinectViewer  -   5          Name$ viewer��5   ,   (� 5      $��0q�6   $	��753��6)   $	��563��65   .��5      �� $
��1080��6!   $
��1920��6/   .+�5      .   (LightBuzz.Vitruvius.Controls.KinectAngle  -   5          Name$ AngleLeftElbow��5   3    J� Opacity$	 0.5Q�6K      Fill
 � � �6Y     5      -   5        $ AngleLeftShoulder��5   3   $	 0.5Q�6N   
 ��  �6\     5      -   5        $ AngleRightElbow��5   3   $	 0.5Q�6L   
 �����6Z     5      -   5        $ AngleRightShoulder��5   3   $	 0.5Q�6O   
 �� ��6]     5      -   5        $ AngleLeftWrist��5   3   $	 0.5Q�6K   
 ��·�6Y     5      -   5        $ AngleRightWrist��5   3   $	 0.5Q�6L   
 ��·�6Z   5      5      5      5      �� 5      
�������$��1q�6*   $
��Left=�6>   $��40��6Y   $��
10,600,0,0q�6e   $	��Top=�6y   $	��774��6�   ���	#FFF6E3E4._�5      ]� .+�5       �� -   5         ��Name$ label��5       $��Kinect Indicator��6+   $��15,0,0,0q�6F   $
��Bold"�6X   $��14(�6j   $��Center=�6x   $
��Left=�6�   A� 5!      -	   5         A�Name$ Indicator_kinect��5!      
���  ��$��25��6C   
��� � �$��Center=�6^   $��25��6y   $
��Left=�6�   $��10,0,0,0q�6�   �� 5"      -
   5         ��Name$ button1��5"      $��Stop rec��6.   $��33��6A   $��Center=�6M   $��75��6h   $��Right=�6s   $
��Bold"�6�   $��14(�6�   $��Right=�6�   $��0,0,50,0q�6�   ���	#FFD02C2C6�   �� 5#      -   5        $ button��5#      $��Data rec��6-   $��33��6@   $��Center=�6L   $��75��6g   $��0,0,50,0q�6r   $
��Bold"�6�   $��14(�6�   ���	#FF446EE5$��Right=�6�   $��Right=�6�   6�   �� 5$      -   5         ��Name$ 
FrameCount��5$       $
��Wrap=�64   $��Frame count:��6H   $��88��6\   $��10,8,0,10.4q�6g   $��14(�6|   $
��Bold"�6�   $��Center=�6�   $
��Left=�6�   �� 5%      -   5        $ FrameCountValue��5%       $��17��69   $
��Wrap=�6E   $��Center=�6�   $��14(�6�   $
��Bold"�6�   $��47��6�   $
��Left=�6�   $��5,10,0,11.4q�6�   
���������6Y   �� *TotalFrameCount+	 �� Mode$	 OneWay=�
 �� FallbackValue$
 0��6  �� 5&      -   5        $	 FPS��5&       $
��Wrap=�6-   $
��FPS:��6A   $��31��6M   $��60,9,0,11.4q�6X   $��14(�6m   $
��Bold"�6{   $��Center=�6�   $
��Left=�6�   �� 5'      -   5        $ FPSValue��5'       $��17��62   $
��Wrap=�6>   $��Center=�6�   $��14(�6�   $
��Bold"�6�   $��47��6�   $
��Left=�6�   $��5,10,0,11.4q�6�   
���������6R   �� *Fps+$	 OneWay=�$
 0��6�   5)      5*      �� 5+      
�������$��1q�6*   $
��Left=�6>   $	��585��6Y   $��
789,10,0,0q�6f   $	��Top=�6z   $	��662��6�   ._�5,      � .+�5-        -   5        $ depthviewer��5-   ,   (� 5.      $
��Left=�6   $	��564��69   $��	10,10,0,0q�6F   $	��Top=�6Y   $	��641��6q   5/      50      51      52      53      