
![图片1](https://user-images.githubusercontent.com/15990821/114347426-80e83280-9b97-11eb-8a95-457ae862c463.png)

一、运行环境要求

 	1．Widows7/Windows10  64Bit
  
 	2．Windows10 SDK （所有）
  
 https://developer.microsoft.com/zh-cn/windows/downloads/sdk-archive/

 	3．DXSDK_Jun10.exe
  
  
 	4．适用于 Windows 7 和 .NET Framework 4 的 Windows SDK
  
 	5．vc_redist.x86.exe 与vc_redist.x64.exe
  
 	6．Nvidia 1060ti 以上的显卡
  
 	7．将所有的显示监视器都插到显卡上防止集成显卡性能不足拖累软件运行帧率
  
 	8．Unity2018.4.2f1 或以上
  
二、SDK导入

 1．新建Unity 3D项目（测试）
 
![图片1](https://user-images.githubusercontent.com/15990821/114347549-af660d80-9b97-11eb-9cc3-7098abf6c95c.png)

 2．导入SDK
 
![图片2](https://user-images.githubusercontent.com/15990821/114347561-b2f99480-9b97-11eb-84bc-0d673948d365.png)

 3．项目文件目录
 
![图片3](https://user-images.githubusercontent.com/15990821/114347585-bd1b9300-9b97-11eb-89d7-f273de5dcb74.png)




4．项目设置

 (1)运行平台 Window  x86_64
 
![图片4](https://user-images.githubusercontent.com/15990821/114347594-bf7ded00-9b97-11eb-80bf-af2efacf255a.png)

 (2)渲染颜色空间设置
 
![图片5](https://user-images.githubusercontent.com/15990821/114347600-c278dd80-9b97-11eb-9b2b-a3a529948abc.png)

 (3)打包后台运行设置
 
![图片6](https://user-images.githubusercontent.com/15990821/114347612-c99feb80-9b97-11eb-9883-ef5e18de7087.png)


三、APP授权

1．默认授权

（1）Appid:78a4ad68c98cb40f

（2）Appkey:622480c88ba3cb51

（3）Code:6051056611412581414505864631510613156500105456105954126

    

四、全息预制MicroLightManager


1．内核渲染器

（1）MicroLightRender  （默认）

    进程画面传输，生成模拟器窗口

（2）UnityRender （仅LR3D）

     线程内部画面传输，生成模拟器窗口

（3）UnityCoverRender （不需扩展屏时，仅LR3D）

    线程内部画面传输，不创建窗口 直接覆盖渲染

2．投影模式

（1）Surface

    面投影，可以带倾斜角度

（2）LTypeRoom

    L型体验室
 
（3）TrapezoidRoom

 	 梯形体验室	
  
3．渲染模式

（1）LR3D

    左右3D,适用于单桌面，单墙面

（2）Stereo

 	显卡主动3D渲染  适用于L型体验室 梯形体验室
  

4．空间追踪面

（1）Surface

    适用于单桌面，单墙面的空间追踪面Id

（2）LTypeRoomFront

    适用于L型体验室 正墙面的空间追踪面Id

（3）LTypeRoomFloor

    适用于L型体验室 地面的空间追踪面Id

（4）TrapezoidRoomLeft

    适用于梯形体验室 左面的空间追踪面Id

（5）TrapezoidRoomFront

    适用于梯形体验室 正前面的空间追踪面Id

（6）TrapezoidRoomRight

    适用于梯形体验室 右面的空间追踪面Id

（7）TrapezoidRoomFloor

    适用于梯形体验室 地面的空间追踪面Id


5．全屏与扩展

（1）AutoFullScreen

      模拟器是否自动全屏，选择UnityCoverRender 内核渲染器不起作用。
      
（2）ShowOnPRIMARY

    模拟器是否显示在主屏幕，打包时注意Display Resolution Dialog 设置与 Regedit 中UnitySelectMonitor的显示器选择。

6．空间追踪面

（1）MicroLightPlayAera

    每个追踪面都有一个固定的Id 标识，每个面都有4个顶点提供算法进行矩阵实时追踪计算；
    其比例大小与外部MicroLight.exe设置的区域大小一样，因此为了保证画面比例问题，在控制缩放时 xyz 缩放是等比的。
     
7．头盔对象

（1）MicroLightCameraRig

    其Id 是Hmd,其与外部眼镜运动是相对的，可以设置其更新位置与否，这里由于定位算法角度误差过大，不建议开启角度追踪更新。

（2）MicroLightTrackingSpace

    MicroLightTrackingSpace 是以MicroLightPlayAera相对应的追踪面左右相机的父体，负责管理当前追踪面的画面追踪。



（3）MicroLightCamera

     每个追踪面都有两个相对偏移的左右相机进行画面实时追踪，其通过实时变换ProjectionMatrix达到图像变换的目的;
    在非HDRP 渲染管线中 更新在OnPreRender函数中，如果在HDRP 渲染管线的时候 需要把 OnPreRender 修改为 Update;
    后期处理绑定PostProcessLayer时 需要自定义修改注释掉PostProcessLayer中 “//m_Camera.ResetProjectionMatrix();”的方法，防止OnPreRender/Update 更新矩阵后被复位掉；
    可视层级与远近才切面开发者可根据自身项目自己调整，SDK不做业务限定；



8．微光手柄

（1）MicroLightHand

    Id 是Touchpad1,其每帧进行输入状态查询，抛出相应事件，与其互相配合的组件有InputMode、MicroLightCanvas、SDKPointer3DRaycaster、PhysicsRaycastMethod、CanvasRaycastMethod。

五、控制

    1.SDK中 MicroLightManager 会初始化硬件驱动所以不能频繁销毁和再次生成，因此需在项目中配置为不销毁对象；
    2.SDK中 MicroLightManager在项目开发过程中可当作一个人称控制器进行 位置移动 选择 三轴等比例缩放，在业务逻辑控制中SDK 不做限定。
    3.输入设备如果采用Leapmotion 手势设备作为控制设备，相关设计相对的位置摆放，注意父节点设计需跟随MicroLightManager位置移动 选择 三轴等比例缩放之间的关系，其他业务功能设计SDK不做限定。
    4.使用Xbox360 或者其他输入设备SDK不做限定。

六、打包

    1.关闭XR
    2. 颜色空间Linear
    3. Display Resolution Dialog 设置 是否有启动画面
    4.当部署程序没有启动监视器选择窗口时显示窗口所在的位置不正确则需对相应App Regedit的注册表项UnitySelectMonitor 进行手动设置
